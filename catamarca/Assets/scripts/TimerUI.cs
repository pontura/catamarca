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
    }
    public void SetOff()
    {
        isOn = false;
    }
    void Update()
    {
        if (!isOn) return;
        timer += Time.deltaTime;
        float v = (timer / duration);
        if (v >= 0.99f)
        {
            v = 1; 
            SetOff();
            Events.OnTimeOver();
        }
        progress.fillAmount = v;
    }
}
