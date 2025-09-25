using UnityEngine;

public class Splash : BaseScreen
{
    [SerializeField] TMPro.TMP_Text field_en;
    [SerializeField] TMPro.TMP_Text field_es;

    public override void OnShow()
    {
        game.PlaySfx("splash");
        field_es.text = Data.Instance.gameData.data.intro_button;
        field_en.text = Data.Instance.gameData.data.intro_button_en;
        Events.ResetApp(game.playerID);
    }
    public override void OnKey(int key)
    {
        if (key == 3)
            OnClicked(2);
        else if (key == 1)
            OnClicked(1);
    }
    public void OnClicked(int langID)
    {
        if (langID == 1)
            Data.Instance.triviaData.SetLang(game.playerID, Trivia.TriviaData.langs.es);
        else
            Data.Instance.triviaData.SetLang(game.playerID, Trivia.TriviaData.langs.en);
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_GENERIC, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("click");
        game.NextScreen();
    }
}
