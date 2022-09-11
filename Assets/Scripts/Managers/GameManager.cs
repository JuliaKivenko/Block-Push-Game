using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Default,
    Starting,
    Running,
    Paused,
    Win,
    Lose,
}

public class GameManager : Singleton<GameManager>
{
    public delegate void GameStateChange();
    public static event GameStateChange onGameStateChange;

    public delegate void PointsNumberChange(int currentPoints);
    public static event PointsNumberChange onPointsNumberChange;


    public GameState gameState { get; private set; }
    public int points { get; private set; }
    public float currentRunTime { get; private set; }
    public float timePlayed { get; private set; }

    [SerializeField] private float timePerLevel;

    private void Start()
    {
        ChangeState(GameState.Starting);
    }

    //handle changing game state
    public void ChangeState(GameState newState)
    {
        if (gameState == newState) return;

        gameState = newState;

        Debug.Log(newState);

        switch (newState)
        {
            case GameState.Starting:
                SetUpNewRun();
                break;

            case GameState.Running:
                HandleUnpause();
                break;

            case GameState.Paused:
                HandlePause();
                break;

            case GameState.Win:
                HandleGameEnd();
                break;

            case GameState.Lose:
                HandleGameEnd();
                break;

            case GameState.Default:
                break;
        }

        onGameStateChange?.Invoke();
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
        if (gameState == GameState.Running)
        {
            if (currentRunTime > 0) currentRunTime -= Time.deltaTime;
            else ChangeState(GameState.Lose);
        }
    }

    //Used when resetting or starting new run. Reset everything to default values.
    public void SetUpNewRun()
    {
        points = 0;
        onPointsNumberChange?.Invoke(points);
        currentRunTime = timePerLevel;
    }

    public void AddPoints()
    {
        points += 1;
        onPointsNumberChange?.Invoke(points);
    }

    private void HandlePause()
    {
        Time.timeScale = 0;
    }
    private void HandleUnpause()
    {
        Time.timeScale = 1;
    }

    private void HandleGameEnd()
    {
        HandlePause();
        SaveManager.Save();
    }

}
