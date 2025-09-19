using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Trivia
{
    public class TriviaData : MonoBehaviour
    {
        public langs lang;
        public enum langs
        {
            es,
            en
        }
        [SerializeField] Data _data_es;
        [SerializeField] Data _data_en;

        public Data data  { 
            get { 
                if(lang == langs.es) 
                    return _data_es;
                else return _data_en;
            }
        }

        bool en_loaded;
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
            if(!en_loaded)
            {
                _data_es = JsonUtility.FromJson<Data>(json);
                en_loaded = true;
                Load("trivia_en.json", OnLoaded);
            }
            else
            {                
                _data_en = JsonUtility.FromJson<Data>(json);
                OnLoaded();
            }
        }
        public void SetLang(langs lang)
        {
            this.lang = lang;
        }
    }
}