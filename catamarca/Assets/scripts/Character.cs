using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int characterID;
    [SerializeField] Animator[] animators;
    [SerializeField] int playerID;

    public enum anims
    {
        idle,
        lose,
        right,
        win,
        wrong
    }
    void Start()
    {
        Events.ChangeCharacter += ChangeCharacter;
        Events.OnCharacterAnim += OnCharacterAnim;
        ChangeCharacter(Random.Range(0,3));
    }

    void OnDestroy()
    {
        Events.ChangeCharacter -= ChangeCharacter;
        Events.OnCharacterAnim -= OnCharacterAnim;
    }

    private void ChangeCharacter(int _chID)
    {
        this.characterID = _chID;
        SetCharacter();
    }
    private void SetCharacter()
    {
        foreach (Animator anim in animators)
            anim.gameObject.SetActive(false);
        animators[characterID].gameObject.SetActive(true);
    }

    void OnCharacterAnim(int _playerID, anims anim)
    {
        if (playerID == _playerID)
        {
            animators[characterID].Play(anim.ToString());
        }
    }
}
