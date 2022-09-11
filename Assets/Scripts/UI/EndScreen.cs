using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void OnEnable()
    {
        GameManager.onGameStateChange += DisplayTexts;
    }
    private void OnApplicationQuit()
    {
        GameManager.onGameStateChange -= DisplayTexts;
    }

    private void DisplayTexts()
    {
        if (GameManager.Instance.gameState == GameState.Win) titleText.text = $"You win!";
        if (GameManager.Instance.gameState == GameState.Lose) titleText.text = $"You lose!";

        finalScoreText.text = $"Score: <sprite=0> {GameManager.Instance.points}";
    }

    public void TryAgain() => GameManager.Instance.ChangeState(GameState.Starting);
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
