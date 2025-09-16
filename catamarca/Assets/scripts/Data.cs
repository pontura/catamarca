using System;
using Trivia;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance;

    public TriviaData triviaData;
    public GameData gameData;

    [SerializeField] GameManager game1;
    [SerializeField] GameManager game2;

    int screenID;

    private void Awake()
    {
        Instance = this;

        game1.Initialize();
        game2.Initialize();

        triviaData = GetComponent<TriviaData>();
        gameData = GetComponent<GameData>();
    }
    void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
        gameData.Load("data.json", OnDataDone);
    }
    void OnDataDone()
    {
        triviaData.Load("trivia.json", Init);
    }
    void Init()
    {
        game1.Init();
        game2.Init();
    }

}
