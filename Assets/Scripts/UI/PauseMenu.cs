using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Return() => GameManager.Instance.ChangeState(GameState.Running);
    public void TryAgain() => GameManager.Instance.ChangeState(GameState.Starting);
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
