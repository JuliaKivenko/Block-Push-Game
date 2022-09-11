using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HudController hud;
    [SerializeField] private PauseMenu pauseScreen;
    [SerializeField] private EndScreen endScreen;

    private void OnEnable()
    {
        GameManager.onGameStateChange += DisplayCorrectUI;
    }

    private void OnApplicationQuit()
    {
        GameManager.onGameStateChange -= DisplayCorrectUI;
    }

    private void DisplayCorrectUI()
    {
        GameState state = GameManager.Instance.gameState;
        switch (state)
        {
            case GameState.Running:
                hud.gameObject.SetActive(true);
                pauseScreen.gameObject.SetActive(false);
                endScreen.gameObject.SetActive(false);
                break;
            case GameState.Paused:
                hud.gameObject.SetActive(false);
                pauseScreen.gameObject.SetActive(true);
                endScreen.gameObject.SetActive(false);
                break;
            case GameState.Win:
                hud.gameObject.SetActive(false);
                pauseScreen.gameObject.SetActive(false);
                endScreen.gameObject.SetActive(true);
                break;
            case GameState.Lose:
                hud.gameObject.SetActive(false);
                pauseScreen.gameObject.SetActive(false);
                endScreen.gameObject.SetActive(true);
                break;
            case GameState.Starting:
                hud.gameObject.SetActive(true);
                pauseScreen.gameObject.SetActive(true);
                endScreen.gameObject.SetActive(true);
                break;
        }
    }

}
