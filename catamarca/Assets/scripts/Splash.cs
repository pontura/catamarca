using UnityEngine;

public class Splash : BaseScreen
{
    [SerializeField] TMPro.TMP_Text field_en;
    [SerializeField] TMPro.TMP_Text field_es;

    private void Start()
    {
    }
    private void OnDestroy()
    {
    }
    public override void OnShow()
    {
        field_es.text = Data.Instance.gameData.data.intro_button;
        field_en.text = Data.Instance.gameData.data.intro_button_en;
        Events.ResetApp();
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
