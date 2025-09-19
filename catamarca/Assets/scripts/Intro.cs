using UnityEngine;

public class Intro : BaseScreen
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text buttonField;

    private void Start()
    {
    }
    private void OnDestroy()
    {
    }
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
        Events.ResetApp();
    }
    public void OnClicked(int langID)
    {
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_GENERIC, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("click");
        game.NextScreen();
    }
}
