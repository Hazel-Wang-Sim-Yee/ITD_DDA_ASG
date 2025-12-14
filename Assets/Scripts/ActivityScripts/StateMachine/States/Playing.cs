/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the Playing state in the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playing : GameStateHandler
{
    // Called when entering the Playing state
    public override void Setup(GameStates lastgameState)
    {
        //Spawn Starting Candies
        ActivityManager.instance.SpawnStartCandies();
        //Start Score Timer and Reset Score if not coming from Paused state
        if (lastgameState != GameStates.Paused)
        {
            ActivityScoreManager.instance.LevelStart();
            
            ActivityScoreManager.instance.ResetScore();
        }
    }

    // Called when exiting the Playing state
    public override void TearDown()
    {
    }
}
