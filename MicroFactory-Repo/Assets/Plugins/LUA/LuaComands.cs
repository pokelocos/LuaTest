using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LuaComands 
{
    [LuaCommand("Debug")]
    public static void DebugLog(string s)
    {
        Debug.Log(s);
    }

    [LuaCommand("Test")]
    public static float Test(int value)
    {
        return value;
    }

    public static class Math
    {
        
    }
}
