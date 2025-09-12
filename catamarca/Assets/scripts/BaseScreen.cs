using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    [HideInInspector] public GameManager game;
    public void Init(GameManager game)
    {
        this.game = game;
    }
    public void Show(bool isOn)
    {
        gameObject.SetActive(isOn);
        if (isOn) {  OnShow(); }else{ OnHide(); }    
    }
    public virtual void OnShow()  { }
    public virtual void OnHide()  { }
}
