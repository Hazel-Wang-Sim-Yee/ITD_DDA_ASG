using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Launching : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        StateMachine.instance.SwitchGameState(GameStates.Menu);
    }

    public override void TearDown()
    {
        
    }
}
