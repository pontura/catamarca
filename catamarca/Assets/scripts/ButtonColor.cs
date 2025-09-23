using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Color[] colors;
    [SerializeField] Image image;

    private void OnEnable()
    {
        if (id>0)
            Init(id);
    }
    public void Init(int id)
    {
        image.color = colors[id-1];
    }
}
