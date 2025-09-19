using UnityEngine;
using YaguarLib.Audio;

public static class Events
{
    public static System.Action ResetApp = delegate { };
    public static System.Action<int> OnTimeOver = delegate { };
    public static System.Action<bool> OnResponse = delegate { };
    public static System.Action<int, Character.anims> OnCharacterAnim = delegate { };
    public static System.Action<int, int> ChangeCharacter = delegate { };
    public static System.Action<bool> OnWin = delegate { };
}