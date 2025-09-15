using System.Collections.Generic;
using UnityEngine;
using YaguarLib.UI;

namespace Trivia
{
    public class TriviaUI : BaseScreen
    {
        [SerializeField] TMPro.TMP_Text field;
        [SerializeField] Transform container;
        [SerializeField] TriviaButton button;
        [SerializeField] List<TriviaButton> buttons;

        [SerializeField] List<ProgressPoint> progressPoints;
        [SerializeField] ProgressPoint progressPoint;
        [SerializeField] Transform progressPointsContainer;

        TriviaData.Result resultDone;
        [SerializeField] TimerUI timerUI;
        List <TriviaData.Result> results;
        int triviaID;

        string okResponse;

        public override void OnShow()
        {
            print("Trivia OnShow");
            triviaID = 0;
            progressPoints = new List<ProgressPoint>();
            YaguarLib.Xtras.Utils.RemoveAllChildsIn(progressPointsContainer);
            for (int a = 0; a < Data.Instance.gameData.data.totalQuestions; a++)
            {
                ProgressPoint p = Instantiate(progressPoint, progressPointsContainer);
                progressPoints.Add(p);
                p.SetState(ProgressPoint.states.off);
            }
          

            InitTrivia(Data.Instance.triviaData.data.questions[triviaID]);           
        }
        public void InitTrivia(TriviaData.Question question)
        {
            timerUI.Init(Data.Instance.gameData.data.questionDuration);
            okResponse = Data.Instance.triviaData.data.questions[triviaID].results[0].response;          

            field.text = question.title;

            YaguarLib.Xtras.Utils.RemoveAllChildsIn(container);

            int buttonId = 0;
            buttons = new List<TriviaButton>();

            results = new List<TriviaData.Result>();
            foreach (TriviaData.Result result in Data.Instance.triviaData.data.questions[triviaID].results)
            {
                results.Add(result);
            }
            YaguarLib.Xtras.Utils.Shuffle(results);
            foreach (TriviaData.Result result in results)
            {
                TriviaButton b = Instantiate(button, container);
                b.Init(this, buttonId, result);
                buttonId++;
                buttons.Add(b);
            }
            print("InitTrivia" + triviaID);
            Invoke("Delayed", 0.1f);
        }
        void Delayed()
        {
            progressPoints[triviaID].SetState(ProgressPoint.states.on);
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
        public bool CheckResult()
        {
            bool isCorrect = okResponse == resultDone.response;
            foreach (TriviaButton b in buttons)
            {
                b.SetResult(b.result.response == okResponse);
            }
            return isCorrect;
        }

        void StopGame()
        {
            timerUI.SetOff();
            Invoke("CheckResultsDone", Data.Instance.gameData.data.delayResponseDone);
        }
        void CheckResultsDone()
        {
            bool isCorrect = CheckResult();

            if(isCorrect)
            {
                Events.OnCharacterAnim(game.playerID, Character.anims.right);
                progressPoints[triviaID].SetState(ProgressPoint.states.done_ok);
            }
            else
            {
                Events.OnCharacterAnim(game.playerID, Character.anims.wrong);
                progressPoints[triviaID].SetState(ProgressPoint.states.done_wrong);
            }

            Events.OnResponse(isCorrect);
            Invoke("Next", Data.Instance.gameData.data.delayForNextTrivia);
        }
        void Next()
        {
            triviaID++;
            if (triviaID >= Data.Instance.gameData.data.totalQuestions)
            {
                triviaID = 0;
                TriviaComplete();
            } else
                InitTrivia(Data.Instance.triviaData.data.questions[triviaID]);
        }
        void TriviaComplete()
        {
            game.NextScreen();
        }
    }
}
