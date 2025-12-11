using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paused : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        PauseMenuUIManager.instance.Enable(true);
        ActivityScoreManager.instance.PauseScoreTimer(true);
    }

    public override void TearDown()
    {
        PauseMenuUIManager.instance.Enable(false);
        ActivityScoreManager.instance.PauseScoreTimer(false);
    }
}
