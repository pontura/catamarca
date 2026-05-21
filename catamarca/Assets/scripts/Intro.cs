using UnityEngine;

public class Intro : BaseScreen
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text buttonField;

    public override void OnShow()
    {
        if (Data.Instance.triviaData.GetLang(game.playerID) == Trivia.TriviaData.langs.es)
        {
            field.text = Data.Instance.gameData.data.intro;
            buttonField.text = Data.Instance.gameData.data.intro_button;
        }
        else
        {
            field.text = Data.Instance.gameData.data.intro_en;
            buttonField.text = Data.Instance.gameData.data.intro_button_en;
        }
        Events.ResetApp(game.playerID);
    }
    public override void OnKey(int key)
    {
        OnClicked(0);
    }
    public void OnClicked(int langID)
    {
        game.PlaySfx("scroll");
        game.NextScreen();
        OnTapScreen();
    }
}
