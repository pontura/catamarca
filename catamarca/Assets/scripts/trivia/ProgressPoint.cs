using UnityEngine;

namespace Trivia
{
    public class ProgressPoint : MonoBehaviour
    {
        public enum states
        {
            done_ok,
            done_wrong,
            on,
            off
        }
        public void SetState(states state)
        {
            GetComponent<Animator>().Play(state.ToString());
        }
    }

}