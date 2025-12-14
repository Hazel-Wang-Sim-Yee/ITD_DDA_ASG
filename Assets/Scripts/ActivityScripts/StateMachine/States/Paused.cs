/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the Paused state in the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paused : GameStateHandler
{
    // Called when entering the Paused state
    public override void Setup(GameStates lastgameState)
    {
        //Enable Pause Menu UI
        PauseMenuUIManager.instance.Enable(true);
        //Pause Score Timer
        ActivityScoreManager.instance.PauseScoreTimer(true);
    }

    // Called when exiting the Paused state
    public override void TearDown()
    {
        //Disable Pause Menu UI
        PauseMenuUIManager.instance.Enable(false);
        //Resume Score Timer
        ActivityScoreManager.instance.PauseScoreTimer(false);
    }
}
