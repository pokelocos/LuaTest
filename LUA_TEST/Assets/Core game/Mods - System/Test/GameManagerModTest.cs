using MoonSharp.Interpreter;
using RA;
using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[MoonSharpUserData]
public class GameManagerModTest : MonoBehaviour
{
    public Text show;

    private void Awake()
    {
        LuaCore.Script.Globals["MicroFactory"] = UserData.Create(this);
        LuaCore.Script.Globals["Game"] = UserData.Create(this);
        LuaCore.Script.Globals["MF"] = UserData.Create(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResourcesLoader.alloModData = true;
        ResourcesLoader.LoadDataGame();

        var a = 2;
    }

}

