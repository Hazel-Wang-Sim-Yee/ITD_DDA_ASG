/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the Launching state in the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Launching : GameStateHandler
{
    // Called when entering the Launching state
    public override void Setup(GameStates lastgameState)
    {
        //Load Main Menu Scene
        StateMachine.instance.SwitchGameState(GameStates.Menu);
    }

    // Called when exiting the Launching state
    public override void TearDown()
    {
    }
}
