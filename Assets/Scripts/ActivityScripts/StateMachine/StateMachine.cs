/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    public static StateMachine instance; // Singleton instance

    [SerializeField]
    private List<GameStateHandler> gameStateHandlers; // List of all game state handlers

    public GameStates currentGameState { get; private set;} // Current game state

    private GameStateHandler  currentStateHandler; // Current state handler

    // Initialize the state machine
    private void Awake()
    {
        SingletonPattern();
        SetStateHandler(GameStates.Launching);
        currentStateHandler.Setup(GameStates.Launching);
    }

    // Switch to a new game state
    public void SwitchGameState(GameStates stateToSwitch)
    {
        if(stateToSwitch == currentGameState) {Debug.LogWarning("Trying to switch to the same game state: " + stateToSwitch.ToString()); return;}
        GameStates lastGameState = currentGameState;

        currentStateHandler.TearDown();
        currentGameState = stateToSwitch;

        SetStateHandler(stateToSwitch);

        currentStateHandler.Setup(lastGameState);
    }

    // Set the current state handler based on the game state
    private void SetStateHandler(GameStates stateToSwitch)
    {
        currentStateHandler = gameStateHandlers.Find(x => x.state == stateToSwitch);
    }

    // Singleton pattern implementation
    private void SingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
