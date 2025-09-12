using Trivia;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TriviaUI trivia;
    [SerializeField] BaseScreen[] screens;
    [SerializeField] int screenID;

    public void Initialize()
    {
        HideScreens();
    }
    public void Init()
    {
        foreach (BaseScreen s in screens)
            s.Init(this);
        ShowScreen();
    }
    void ShowScreen()
    {
        HideScreens();
        screens[screenID].Show(true);
        print("ShowScreen " + screenID);
    }
    public void NextScreen()
    {
        screenID++;
        if (screenID > screens.Length) screenID = 0;
        ShowScreen();
    }
    void HideScreens()
    {
        foreach (BaseScreen s in screens)
            s.Show(false);
    }




   

}
