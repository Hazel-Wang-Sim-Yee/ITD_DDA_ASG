using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    public static StateMachine instance;

    [SerializeField]
    private List<GameStateHandler> gameStateHandlers;

    public GameStates currentGameState { get; private set;}

    private GameStateHandler  currentStateHandler;

    private void Awake()
    {
        SingletonPattern();
        SetStateHandler(GameStates.Launching);
        currentStateHandler.Setup(GameStates.Launching);
    }

    public void SwitchGameState(GameStates stateToSwitch)
    {
        if(stateToSwitch == currentGameState) {Debug.LogWarning("Trying to switch to the same game state: " + stateToSwitch.ToString()); return;}
        GameStates lastGameState = currentGameState;

        currentStateHandler.TearDown();
        currentGameState = stateToSwitch;

        SetStateHandler(stateToSwitch);

        currentStateHandler.Setup(lastGameState);
    }

    private void SetStateHandler(GameStates stateToSwitch)
    {
        currentStateHandler = gameStateHandlers.Find(x => x.state == stateToSwitch);
    }

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
