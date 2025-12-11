using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playing : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        ActivityManager.instance.SpawnStartCandies();
        if (lastgameState != GameStates.Paused)
        {
            ActivityScoreManager.instance.LevelStart();
            
            ActivityScoreManager.instance.ResetScore();
        }
    }

    public override void TearDown()
    {

    }
}
