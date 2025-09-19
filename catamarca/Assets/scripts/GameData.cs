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
        public float gameOverDuration;
        public int totalQuestions;
        public float delayForNextTrivia;
        public float delayResponseDone;
        public float questionDuration;

        public string intro;
        public string intro_button;
        public string choose;

        public string intro_title_p1;
        public string intro_desc_p1;

        public string intro_title_p2;
        public string intro_desc_p2;

        public string intro_title_p3;
        public string intro_desc_p3;

        public string end_win;
        public string end_lose;

        public string intro_en;
        public string intro_button_en;
        public string choose_en;

        public string intro_title_p1_en;
        public string intro_desc_p1_en;

        public string intro_title_p2_en;
        public string intro_desc_p2_en;

        public string intro_title_p3_en;
        public string intro_desc_p3_en;

        public string end_win_en;
        public string end_lose_en;

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
