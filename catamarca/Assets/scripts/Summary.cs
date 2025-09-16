public class Summary : BaseScreen
{
    private void Start()
    {
    }
    private void OnDestroy()
    {
    }
    public override void OnShow()
    {
        Events.OnCharacterAnim(game.playerID, Character.anims.win);
        print("summary OnShow");
        Invoke("Ready", Data.Instance.gameData.data.gameOverDuration);
    }
    void Ready()
    {
        game.NextScreen();
    }
}
