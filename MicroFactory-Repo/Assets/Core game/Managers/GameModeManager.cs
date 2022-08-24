using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    private int deltaReward;

    public bool WinCondition()
    {
        return false;
    }

    public bool LoseCondition()
    {
        return false;
    }

    public bool LoseWarning()
    {
        return false;
    }
}
