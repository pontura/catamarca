using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Trivia
{
    public class TriviaData : MonoBehaviour
    {
        [SerializeField] List<Sprite> sprites;
        public langs lang_p1;
        public langs lang_p2;

        public enum langs
        {
            es,
            en
        }
        [SerializeField] Data _data_es;
        [SerializeField] Data _data_en;

        public Sprite GetSprite(int id) {
            int arrID = id;
            if (arrID >= sprites.Count) arrID = 0;
            return sprites[arrID];
        }
        public langs GetLang(int p)
        {
            if (p == 1) 
                return lang_p1;
            return lang_p2;
        }
        public Data GetData(int p)  { 
            if(p == 1)
            { 
                if(lang_p1 == langs.es) 
                    return _data_es;
                else return _data_en;
            } else
            {
                if (lang_p2 == langs.es)
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
            public int id;
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
        public void LoadSprites(System.Action OnLoaded) {
            sprites = new();
            StartCoroutine(LoadSpritesFromFiles(OnLoaded));
        }

        IEnumerator LoadSpritesFromFiles(System.Action OnLoaded) {
            for (int i = 0; i < _data_es.questions.Length; i++) {
                string path = Path.Combine(Application.streamingAssetsPath + "/Images", i + ".png");
                Texture2D tex = null;

                if (path.Contains("://") || path.Contains(":///")) {
                    using (WWW www = new WWW(path)) {
                        yield return www;
                        tex = www.texture;
                    }
                } else if(File.Exists(path)) {
                    tex = new Texture2D(2, 2);
                    tex.LoadImage(File.ReadAllBytes(path));
                }
                if (tex != null) {
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
                    sprites.Add(sprite);
                }
            }
            OnLoaded();
        }

        public void SetLang(int p, langs lang)
        {
            if(p == 1)
                this.lang_p1 = lang;
            else
                this.lang_p2 = lang;
        }
    }
}