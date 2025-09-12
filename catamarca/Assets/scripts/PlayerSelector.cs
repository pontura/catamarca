public class PlayerSelector : BaseScreen
{
    public override void OnShow()
    {
        base.OnShow();
    }
    public void OnSelect(int playerID)
    {
        game.NextScreen();
    }
}
