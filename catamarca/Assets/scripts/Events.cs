using UnityEngine;
using YaguarLib.Audio;

public static class Events
{
    public static System.Action OnTimeOver = delegate { };
    public static System.Action<bool> OnResponse = delegate { };
}