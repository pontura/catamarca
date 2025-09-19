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

        if(score>= (Data.Instance.gameData.data.totalQuestions+(Data.Instance.gameData.data.totalQuestions/2) ) /2)
        {
            Events.OnCharacterAnim(game.playerID, Character.anims.win);

            if (Data.Instance.triviaData.GetLang(game.playerID) == Trivia.TriviaData.langs.es)
                field.text = Data.Instance.gameData.data.end_win;
            else
                field.text = Data.Instance.gameData.data.end_win_en;
            Events.OnWin(true);
        }
        else
        {
            Events.OnCharacterAnim(game.playerID, Character.anims.lose);

            if (Data.Instance.triviaData.GetLang(game.playerID) == Trivia.TriviaData.langs.es)
                field.text = Data.Instance.gameData.data.end_lose;
            else
                field.text = Data.Instance.gameData.data.end_lose_en;
            Events.OnWin(false);
        }

        game.ResetScore();
    }
    void Ready()
    {
        game.NextScreen();
    }
}
