using UnityEngine;
using static Character;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject winSummary;
    [SerializeField] GameObject damage;
    [SerializeField] Animator bg;
    [SerializeField] int characterID;
    [SerializeField] Animator[] animators;
    [SerializeField] int playerID;
    int damageNum = 0;
    public enum anims
    {
        idle,
        lose,
        right,
        win,
        wrong,
        scroll
    }
    void Start()
    {
        Events.ResetApp += ResetApp;
        Events.ChangeCharacter += ChangeCharacter;
        Events.OnWin += OnWin;
        ResetApp();
    }

    void OnDestroy()
    {
        Events.ResetApp -= ResetApp;
        Events.ChangeCharacter += ChangeCharacter;
        Events.ChangeCharacter -= ChangeCharacter;
        Events.OnWin -= OnWin;
    }
    void OnWin(bool won)
    {
        winSummary.SetActive(won);
    }
    private void ChangeCharacter(int _playerID, int _chID)
    {
        if (playerID == _playerID)
        {
            this.characterID = _chID;
            SetCharacter();
            animators[characterID].Play("scroll");
        }
    }
    private void SetCharacter()
    {
        foreach (Animator anim in animators)
            anim.gameObject.SetActive(false);
        animators[characterID].gameObject.SetActive(true);
    }
    void ResetApp()
    {
        winSummary.SetActive(false);
        damage.SetActive(false);
        damageNum = 0;
        gameObject.SetActive(false);
        Invoke("Reseted", 0.1f);
    }
    void Reseted()
    {
        gameObject.SetActive(true);
    }
    void OnCharacterAnim(int _playerID, anims anim)
    {
        if (playerID == _playerID)
        {
            animators[characterID].Play(anim.ToString());
        }
        if(anim == anims.wrong)
        {
            damage.SetActive(true);
            CancelInvoke();
            damageNum++;
            bg.Play("damage_" + damageNum);
            Invoke("DamageDone", 2.5f);
        }
    }
    void DamageDone()
    {
        damage.SetActive(false);
        gameObject.SetActive(true);
    }
}
