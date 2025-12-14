/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages UI input for game state transitions.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIInputManager : MonoBehaviour
{
    // Called when the Pause button is pressed
    public void PauseTheGame()
    {
        // Only pause if the game is currently in the Playing state
        if(StateMachine.instance.currentGameState == GameStates.Playing)
        {
            StateMachine.instance.SwitchGameState(GameStates.Paused);
        }
    }

    // Called when the Restart button is pressed
    public void RestartTheGame()
    {
        StateMachine.instance.SwitchGameState(GameStates.Playing);
    }

    // Called when the Return to Main Game button is pressed
    public void ReturnToMainGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
