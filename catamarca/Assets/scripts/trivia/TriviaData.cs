using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Trivia
{
    public class TriviaData : MonoBehaviour
    {
        public Data data;

        [Serializable]
        public class Data
        {
            public Question[] questions;
        }
        [Serializable]
        public class Question
        {
            public string title;
            public Result[] results;
        }
        [Serializable]
        public class Result
        {
            public string response;
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
}