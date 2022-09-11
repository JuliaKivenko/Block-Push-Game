using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        scoreText.text = "<sprite=0> 0";
    }
    private void OnEnable()
    {
        GameManager.onPointsNumberChange += UpdateScoreText;
    }

    private void OnApplicationQuit()
    {
        GameManager.onPointsNumberChange -= UpdateScoreText;
    }

    private void Update()
    {
        float timer = GameManager.Instance.currentRunTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void UpdateScoreText(int points) => scoreText.text = $"<sprite=0> {points}";

    public void ShowPauseMenu() => GameManager.Instance.ChangeState(GameState.Paused);
}
