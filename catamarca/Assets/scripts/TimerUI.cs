using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Image progress;
    bool isOn;
    float timer;
    float duration;
    int playerID;

    public void Init(float duration, int playerID)
    {
        this.playerID = playerID;
        isOn = true;
        timer = 0;
        this.duration = duration;
        progress.fillAmount = 1;
    }
    public void SetOff()
    {
        isOn = false;
    }
    void Update()
    {
        if (!isOn) return;
        timer += Time.deltaTime;
        float v = 1-(timer / duration);
        if (v <= 0)
        {
            v = 0; 
            SetOff();
            Events.OnTimeOver(playerID);
        }
        progress.fillAmount = v;
    }
}
