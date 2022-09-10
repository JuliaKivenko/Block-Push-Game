using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Running,
    Paused,
    Win,
    Lose,
}

public class GameManager : Singleton<GameManager>
{
    public delegate void GameStateChange();
    public static event GameStateChange onGameStateChange;


    public GameState gameState { get; private set; }
    public float points { get; private set; }
    public float currentTime { get; private set; }
    public float lastRunScore { get; private set; }

    [SerializeField] private float timePerLevel;

    private void Start()
    {
        SetUpNewRun();
    }

    //handle changing game state
    public void ChangeState(GameState newState)
    {
        if (gameState == newState) return;

        gameState = newState;

        Debug.Log(newState);

        switch (newState)
        {
            case GameState.Running:
                break;

            case GameState.Paused:
                break;

            case GameState.Win:
                lastRunScore = points;
                SetUpNewRun();
                break;

            case GameState.Lose:
                lastRunScore = points;
                SetUpNewRun();
                break;
        }

        onGameStateChange?.Invoke();
    }

    private void Update()
    {
        if (gameState == GameState.Running)
        {
            if (currentTime > 0) currentTime -= Time.deltaTime;
            else ChangeState(GameState.Lose);
        }
    }

    //Used when resetting or starting new run. Reset everything to default values.
    public void SetUpNewRun()
    {
        points = 0;
        currentTime = timePerLevel;
    }

    public void AddPoints() => points += 1;

}
