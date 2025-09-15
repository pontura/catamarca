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
        if (p == 1) playerID++;
        else playerID--;
        if (playerID > 2) playerID = 0;
        else if(playerID < 0) playerID = 2;
            Events.ChangeCharacter(game.playerID, playerID);
    }
    public void StartGame()
    {
        game.NextScreen();
    }
}
