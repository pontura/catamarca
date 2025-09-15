using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Image progress;
    bool isOn;
    float timer;
    float duration;

    public void Init(float duration)
    {
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
            Events.OnTimeOver();
        }
        progress.fillAmount = v;
    }
}
