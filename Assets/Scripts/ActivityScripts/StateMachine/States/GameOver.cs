/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the Game Over state in the game's state machine.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOver : GameStateHandler
{
    // Called when entering the Game Over state
    public override void Setup(GameStates lastgameState)
    {
        //Hide Candies
        ActivityManager.instance.HideCandies();
        //Set High Score
        HighScoreManager.instance.SetHighScore();
        //Enable Game Over UI
        GameOverUIManager.instance.Enable(true);
    }

    // Called when exiting the Game Over state
    public override void TearDown()
    {
        //Disable Game Over UI
        GameOverUIManager.instance.Enable(false);
    }
}
