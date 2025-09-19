using UnityEngine;

public class Splash : BaseScreen
{
    [SerializeField] TMPro.TMP_Text field;

    private void Start()
    {
    }
    private void OnDestroy()
    {
    }
    public override void OnShow()
    {
        Events.ResetApp();
    }
    public void OnClicked(int langID)
    {
        if (langID == 1)
            Data.Instance.triviaData.SetLang(Trivia.TriviaData.langs.es);
        else
            Data.Instance.triviaData.SetLang(Trivia.TriviaData.langs.en);
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_GENERIC, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("click");
        game.NextScreen();
    }
}
