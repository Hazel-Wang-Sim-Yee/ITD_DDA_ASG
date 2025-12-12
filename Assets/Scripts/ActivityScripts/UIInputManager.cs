using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIInputManager : MonoBehaviour
{
    public void PauseTheGame()
    {
        if(StateMachine.instance.currentGameState == GameStates.Playing)
        {
            StateMachine.instance.SwitchGameState(GameStates.Paused);
        }
    }

    public void RestartTheGame()
    {
        StateMachine.instance.SwitchGameState(GameStates.Playing);
    }

    public void ReturnToMainGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
