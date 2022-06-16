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

    public static class Math
    {
        
    }
}
