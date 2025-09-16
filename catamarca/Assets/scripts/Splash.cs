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
    public void OnClcked()
    {
        game.NextScreen();
    }
}
