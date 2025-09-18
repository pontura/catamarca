using UnityEngine;

public class PlayerSelector : BaseScreen
{
    int playerID;
    public override void OnShow()
    {
        base.OnShow();
        playerID = Random.Range(0, 3);
        Events.ChangeCharacter(game.playerID, playerID);

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
    }
    public void StartGame()
    {
        //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.UI_GENERIC, YaguarLib.Audio.AudioManager.channels.UI);
        game.PlaySfx("click");
        game.NextScreen();
    }
}
