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
        Invoke("Ready", Data.Instance.gameData.data.gameOverDuration);
        int score = game.score;

        if(score>=Data.Instance.gameData.data.totalQuestions/2)
        {
            Events.OnCharacterAnim(game.playerID, Character.anims.win);
            field.text = Data.Instance.gameData.data.end_win;
        }
        else
        {
            Events.OnCharacterAnim(game.playerID, Character.anims.lose);
            field.text = Data.Instance.gameData.data.end_lose;
        }

        game.ResetScore();
    }
    void Ready()
    {
        game.NextScreen();
    }
}
