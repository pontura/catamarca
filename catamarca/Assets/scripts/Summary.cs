using System;
using UnityEngine;

public class Summary : BaseScreen
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
        print("summary OnShow");
        Invoke("Ready", Data.Instance.gameData.data.gameOverDuration);
        int score = game.score;

        if(score>=3)
            Events.OnCharacterAnim(game.playerID, Character.anims.win);
        else
            Events.OnCharacterAnim(game.playerID, Character.anims.lose);

        game.ResetScore();
    }
    void Ready()
    {
        game.NextScreen();
    }
}
