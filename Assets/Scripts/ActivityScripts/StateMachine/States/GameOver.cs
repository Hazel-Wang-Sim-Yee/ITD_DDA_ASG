using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOver : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        ActivityManager.instance.HideCandies();
        HighScoreManager.instance.SetHighScore();
        GameOverUIManager.instance.Enable(true);
    }

    public override void TearDown()
    {
        GameOverUIManager.instance.Enable(false);
    }
}
