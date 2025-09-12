using Trivia;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TriviaUI trivia;
    [SerializeField] BaseScreen[] screens;

    int screenID;
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
    }
    void NextScreen()
    {
        screenID++;
        if (screenID > screens.Length) screenID = 0;
    }
    void HideScreens()
    {
        foreach (BaseScreen s in screens)
            s.Show(false);
    }




   

}
