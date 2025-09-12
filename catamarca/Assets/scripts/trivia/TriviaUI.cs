using System.Collections.Generic;
using UnityEngine;

namespace Trivia
{
    public class TriviaUI : BaseScreen
    {
        [SerializeField] TMPro.TMP_Text field;
        [SerializeField] Transform container;
        [SerializeField] TriviaButton button;
        [SerializeField] List<TriviaButton> buttons;

        TriviaData.Result resultDone;
        [SerializeField] TimerUI timerUI;

        int triviaID;

        string okResponse;

        public override void OnShow()
        {
            InitTrivia(Data.Instance.triviaData.data.questions[triviaID]);        
        }
        public void InitTrivia(TriviaData.Question question)
        {
            timerUI.Init(Data.Instance.gameData.data.questionDuration);
            okResponse = Data.Instance.triviaData.data.questions[triviaID].results[0].response;
            YaguarLib.Xtras.Utils.Shuffle(Data.Instance.triviaData.data.questions[triviaID].results);

            field.text = question.title;
            YaguarLib.Xtras.Utils.RemoveAllChildsIn(container);

            int buttonId = 0;
            buttons = new List<TriviaButton>();
            foreach (TriviaData.Result result in question.results)
            {
                TriviaButton b = Instantiate(button, container);
                b.Init(this, buttonId, result);
                buttonId++;
                buttons.Add(b);
            }
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
            Events.OnResponse(isCorrect);
            Invoke("Next", Data.Instance.gameData.data.delayForNextTrivia);
        }
        void Next()
        {
            triviaID++;
            if (triviaID >= Data.Instance.gameData.data.totalQuestions)
                TriviaDone();
            //else
            InitTrivia(Data.Instance.triviaData.data.questions[triviaID]);
        }
        void TriviaDone()
        {
            triviaID = 0;
        }
    }
}
