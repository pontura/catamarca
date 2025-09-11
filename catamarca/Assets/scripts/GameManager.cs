using System;
using Trivia;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TriviaData triviaData;
    GameData gameData;

    [SerializeField] TimerUI timerUI;
    [SerializeField] TriviaUI p1;
    [SerializeField] TriviaUI p2;

    int triviaID;

    private void Awake()
    {
        triviaData = GetComponent<TriviaData>();
        gameData = GetComponent<GameData>();
    }
    void Start()
    {
        gameData.Load("data.json", OnDataDone);
    }
    void OnDataDone()
    {
        triviaData.Load("trivia.json", InitTrivia);
    }
    string okResponse;
    void InitTrivia()
    {
        timerUI.Init(gameData.data.questionDuration);
        okResponse = triviaData.data.questions[triviaID].results[0].response;
        YaguarLib.Xtras.Utils.Shuffle(triviaData.data.questions[triviaID].results);
        p1.Init(this, triviaData.data.questions[triviaID]);
        p2.Init(this, triviaData.data.questions[triviaID]);
    }
    int results;
    public void CheckResults()
    {
        results++;
        if (results >= 2)
            StopGame();
    }
    void StopGame()
    {
        timerUI.SetOff();
        Invoke("CheckResultsDone", gameData.data.delayResponseDone);
    }
    void CheckResultsDone()
    {
        p1.CheckResult(okResponse);
        p2.CheckResult(okResponse);

        Invoke("Next", gameData.data.delayForNextTrivia);
    }
    void Next()
    {
        results = 0;
        triviaID++;
        if (triviaID >= gameData.data.totalQuestions)
            Done();
        else
            InitTrivia();
    }
    void Done()
    {
        results = 0;
    }
}
