using Trivia;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TriviaUI trivia;
    [SerializeField] BaseScreen[] screens;
    [SerializeField] int screenID;
    public int playerID = 0;

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
        print("ShowScreen " + screenID);
        HideScreens();
        screens[screenID].Show(true);
    }
    public void NextScreen()
    {
        screenID++;
        if (screenID >= screens.Length) screenID = 0;
        ShowScreen();
    }
    void HideScreens()
    {
        foreach (BaseScreen s in screens)
            s.Show(false);
    }




   

}
