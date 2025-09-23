using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    [SerializeField] ButtonColor[] buttonColors;

    protected void OnEnable()
    {
        Events.OnKeyPressed += OnKeyPressed;
    }
    protected void OnDisable()
    {
        Events.OnKeyPressed -= OnKeyPressed;
    }
    void OnKeyPressed(int _playerID, int keyID)
    {
        if(game.playerID == _playerID)
        OnKey(keyID);
    }
    public virtual void OnKey(int key) { }

    [HideInInspector] public GameManager game;
    public void Init(GameManager game)
    {
        this.game = game;
    }
    public void Show(bool isOn)
    {
        gameObject.SetActive(isOn);
        if (isOn)
            OnShow();
        else
            OnHide();
    }
    public virtual void OnShow()  { }
    public virtual void OnHide()  { }
}
