using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Data data;

    [Serializable]
    public class Data
    {
        public int totalQuestions;
        public float delayForNextTrivia;
        public float delayResponseDone;
        public float questionDuration;
        public string[] wrong_answers;
    }
    public void Load(string json, System.Action OnLoaded)
    {
        StartCoroutine(LoadJson(json, OnLoaded));
    }
    IEnumerator LoadJson(string url, System.Action OnLoaded)
    {
        string path = Path.Combine(Application.streamingAssetsPath, url);
        string json;
        if (path.Contains("://") || path.Contains(":///"))
        {
            using (WWW www = new WWW(path))
            {
                yield return www;
                json = www.text;
            }
        }
        else
        {
            json = File.ReadAllText(path);
        }
        data = JsonUtility.FromJson<Data>(json);
        OnLoaded();
    }
}
