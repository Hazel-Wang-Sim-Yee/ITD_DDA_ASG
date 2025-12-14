/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the Menu state in the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : GameStateHandler
{
    // Called when entering the Menu state
    public override void Setup(GameStates lastgameState)
    {
        //Enable Start Menu UI
        StartMenuUIManager.instance.Enable(true);
    }

    // Called when exiting the Menu state
    public override void TearDown()
    {
        //Disable Start Menu UI
        StartMenuUIManager.instance.Enable(false);
    }
}
