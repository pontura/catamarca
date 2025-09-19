using Trivia;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TriviaUI trivia;
    [SerializeField] BaseScreen[] screens;
    [SerializeField] int screenID;
    public int playerID = 0;
    public int score;
    [SerializeField] YaguarLib.Audio.IngameAudio ingameAudio;

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
    public void ResetScore()
    {
        this.score = 0;
    }
    public void AddScore(bool isCorrect)
    {
        if(isCorrect)
            this.score += 1;
    }

    public void PlaySfx(string key, bool loop=false) {
        if (loop) {
            YaguarLib.Audio.AudioManager.channels channel = playerID == 0 ? YaguarLib.Audio.AudioManager.channels.GAME1 : YaguarLib.Audio.AudioManager.channels.GAME2;
            float pitch = playerID == 0 ? 0.95f : 1.05f;
            YaguarLib.Audio.AudioManager.Instance.ChangePitch(channel, pitch);
            ingameAudio.Play(key, channel, loop: loop);
        } else {
            ingameAudio.Play(key);
        }
    }

    public void StopLoopSfx() {
        YaguarLib.Audio.AudioManager.channels channel = playerID == 0 ? YaguarLib.Audio.AudioManager.channels.GAME1 : YaguarLib.Audio.AudioManager.channels.GAME2;
        YaguarLib.Audio.AudioManager.Instance.StopChannel(channel);
        YaguarLib.Audio.AudioManager.Instance.ChangePitch(channel, 1f);
    }

}
