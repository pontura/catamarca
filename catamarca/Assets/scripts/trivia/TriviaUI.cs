using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Trivia.TriviaData;

namespace Trivia
{
    public class TriviaUI : BaseScreen
    {
        [SerializeField] Image image;
        [SerializeField] TMPro.TMP_Text field;
        [SerializeField] Transform container;
        [SerializeField] TriviaButton button;
        [SerializeField] List<TriviaButton> buttons;

        [SerializeField] List<ProgressPoint> progressPoints;
        [SerializeField] ProgressPoint progressPoint;
        [SerializeField] Transform progressPointsContainer;
        [SerializeField] List<Question> questions;
        TriviaData.Result resultDone;
        [SerializeField] TimerUI timerUI;
        List <TriviaData.Result> results;
        int triviaIndex;

        string okResponse;
        private void Start()
        {
            Events.OnTimeOver += OnTimeOver;
        }
        private void OnDestroy()
        {
            Events.OnTimeOver += OnTimeOver;
        }
        private void OnTimeOver(int _playerID)
        {
            if (_playerID != game.playerID) return;            
            game.PlaySfx("timeout");
            foreach (TriviaButton b in buttons)
            {
                b.SetInteraction(false);
            }
            StopGame();

        }
        public override void OnShow()
        {
            print("Trivia OnShow");
            triviaIndex = 0;
            progressPoints = new List<ProgressPoint>();
            YaguarLib.Xtras.Utils.RemoveAllChildsIn(progressPointsContainer);
            questions = new List<Question>();
            foreach (Question q in Data.Instance.triviaData.GetData(game.playerID).questions)
            {
                questions.Add(q);
            }
            YaguarLib.Xtras.Utils.Shuffle(questions);

            for (int a = 0; a < Data.Instance.gameData.data.totalQuestions; a++)
            {
                ProgressPoint p = Instantiate(progressPoint, progressPointsContainer);
                progressPoints.Add(p);
                if(a == 0)
                    p.SetState(ProgressPoint.states.on);
                else
                    p.SetState(ProgressPoint.states.off);
            }

            game.PlaySfx("trivia_entry");
            InitTrivia(questions[triviaIndex]);           
        }
        public void InitTrivia(TriviaData.Question question)
        {
            game.PlaySfx("timer", true);
            timerUI.Init(Data.Instance.gameData.data.questionDuration, game.playerID);
            okResponse = questions[triviaIndex].results[0].response;          

            field.text = question.title;

            YaguarLib.Xtras.Utils.RemoveAllChildsIn(container);

            int buttonId = 0;
            buttons = new List<TriviaButton>();

            results = new List<TriviaData.Result>();
            foreach (TriviaData.Result result in questions[triviaIndex].results)
            {
                results.Add(result);
            }
            YaguarLib.Xtras.Utils.Shuffle(results);
            foreach (TriviaData.Result result in results)
            {
                TriviaButton b = Instantiate(button, container);
                b.Init(this, buttonId, result, ResponseSfx);
                buttonId++;
                buttons.Add(b);
            }
            print("InitTrivia" + triviaIndex);
            progressPoints[triviaIndex].SetState(ProgressPoint.states.on);
            Events.OnCharacterAnim(game.playerID, Character.anims.idle);
        }
        public void OnSelect(TriviaButton button)
        {
            resultDone = button.result;
            foreach (TriviaButton b in buttons)
            {
                b.SetInteraction(false);
                b.OnSelected(b == button);
            }
            StopGame(); 
        }

        void ResponseSfx(bool correct) {
            string key = correct ? "right" : "wrong";
            game.PlaySfx(key);
        }
        public bool CheckResult()
        {
            bool isCorrect = false;
            if (resultDone != null)
            {
                isCorrect = okResponse == resultDone.response;
                resultDone = null;
            }
            foreach (TriviaButton b in buttons)
            {
                b.SetResult(b.result.response == okResponse);
            }
            return isCorrect;
        }

        void StopGame()
        {
            timerUI.SetOff();
            game.StopLoopSfx();
            Invoke(nameof(CheckResultsDone), Data.Instance.gameData.data.delayResponseDone);
        }
        void CheckResultsDone()
        {
            bool isCorrect = CheckResult();
            game.AddScore(isCorrect);
            if(isCorrect)
            {
                GetComponent<Animator>().Play("right");
                image.sprite = Data.Instance.triviaData.GetSprite(questions[triviaIndex].id);
                Events.OnCharacterAnim(game.playerID, Character.anims.right);
                progressPoints[triviaIndex].SetState(ProgressPoint.states.done_ok);
            }
            else
            {
                GetComponent<Animator>().Play("wrong");
                Events.OnCharacterAnim(game.playerID, Character.anims.wrong);
                progressPoints[triviaIndex].SetState(ProgressPoint.states.done_wrong);
            }

            Events.OnResponse(isCorrect);
            Invoke(nameof(Next), Data.Instance.gameData.data.delayForNextTrivia);
        }
        void Next()
        {
            triviaIndex++;
            if (triviaIndex >= Data.Instance.gameData.data.totalQuestions)
            {
                triviaIndex = 0;
                TriviaComplete();
            } else
                InitTrivia(questions[triviaIndex]);
        }
        void TriviaComplete()
        {
            game.NextScreen();
        }
    }
}
