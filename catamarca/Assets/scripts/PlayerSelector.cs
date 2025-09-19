using UnityEngine;

public class PlayerSelector : BaseScreen
{
    int playerID;

    [SerializeField] TMPro.TMP_Text title;
    [SerializeField] TMPro.TMP_Text field;

    public override void OnShow()
    {
        base.OnShow();
        playerID = Random.Range(0, 3);
        Events.ChangeCharacter(game.playerID, playerID);
        SetTexts();
    }
    void SetTexts()
    {
        switch (playerID)
        {
            case 0:
                title.text = Data.Instance.gameData.data.intro_title_p1;
                field.text = Data.Instance.gameData.data.intro_desc_p1;
                break;
            case 1:
                title.text = Data.Instance.gameData.data.intro_title_p2;
                field.text = Data.Instance.gameData.data.intro_desc_p2;
                break;
            case 2:
                title.text = Data.Instance.gameData.data.intro_title_p3;
                field.text = Data.Instance.gameData.data.intro_desc_p3;
                break;
        }
    }
    public void OnSelect(int p)
    {
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_SWIPE, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("scroll");
        if (p == 1) playerID++;
        else playerID--;
        if (playerID > 2) playerID = 0;
        else if(playerID < 0) playerID = 2;
            Events.ChangeCharacter(game.playerID, playerID);
        SetTexts();
    }
    public void StartGame()
    {
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_GENERIC, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("click");
        game.NextScreen();
    }
}
