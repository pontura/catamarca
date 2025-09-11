using System.Collections.Generic;
using UnityEngine;

namespace Trivia
{
    public class TriviaUI : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text field;
        [SerializeField] Transform container;
        [SerializeField] TriviaButton button;
        [SerializeField] List<TriviaButton> buttons;

        TriviaData.Result resultDone;
        GameManager game;

        public void Init(GameManager game, TriviaData.Question question)
        {
            this.game = game;
            field.text = question.title;
            YaguarLib.Xtras.Utils.RemoveAllChildsIn(container);

            int buttonId = 0;
            buttons = new List<TriviaButton>();
            foreach (TriviaData.Result result in question.results)
            {
                TriviaButton b =  Instantiate(button, container);
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
            game.CheckResults();
        }
        public bool CheckResult(string okResponse)
        {
            bool isCorrect = okResponse == resultDone.response;

            foreach (TriviaButton b in buttons)
            {
                b.SetResult(b.result.response == okResponse);
            }

            return isCorrect;
        }
    }
}
