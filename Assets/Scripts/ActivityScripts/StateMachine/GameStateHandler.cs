/*
* Author: Hazel
* Date: 2025-12-08
* Description: Abstract base class for handling game states.
*/
using UnityEngine;

[System.Serializable]
public abstract class GameStateHandler : MonoBehaviour
{
    public GameStates state;
    public abstract void Setup(GameStates lastgameState);
    public abstract void TearDown();
}
