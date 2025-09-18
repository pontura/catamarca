using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class TriviaButton : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text field;
        [SerializeField] Animator anim;
        [SerializeField] Animator feedback;
        public int id;
        TriviaUI triviaUI;
        public TriviaData.Result result;
        bool selected;

        System.Action<bool> OnDone;

        void Start()
        {
            anim = GetComponent<Animator>();          
        }
        public void Init(TriviaUI triviaUI, int id, TriviaData.Result result, System.Action<bool> onDone)
        {
            this.result = result;
            this.triviaUI = triviaUI;
            this.id = id;
            field.text = result.response;
            GetComponent<Button>().onClick.AddListener(() => {
                selected = true;
                this.triviaUI.OnSelect(this);
            });
            OnDone = onDone;
        }
        public void SetInteraction(bool isOn)
        {
            GetComponent<Button>().interactable = isOn;
        }
        public void OnSelected(bool hasBeenClicked)
        {
            //if (hasBeenClicked)
            //    anim.Play("SelectedDone");
            //else 
                anim.Play("Disabled");
        }
        public void SetResult(bool isCorrect)
        {
            if (isCorrect)
            {
                if (selected) {
                    //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.REWARD, YaguarLib.Audio.AudioManager.channels.UI);
                    OnDone(true);
                    anim.Play("CorrectSelected");
                } else
                    anim.Play("CorrectUnselected");
            }
            else
            {
                if (selected) {
                    //YaguarLib.Events.Events.OnPlaySoundInChannel(YaguarLib.Audio.AudioManager.types.WRONG, YaguarLib.Audio.AudioManager.channels.UI);
                    OnDone(false);
                    anim.Play("IncorrectSelected");
                } else
                    anim.Play("Disabled");
            }
        }
    }
}
