using MoonSharp.Interpreter;
using RA;
using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MoonSharpUserData]
public class GameManager : MonoBehaviour
{

    public SpriteRenderer node_Pref;

    private void Awake()
    {
        LuaCore.script.Globals["MicroFactory"] = UserData.Create(this);
        LuaCore.script.Globals["Game"] = UserData.Create(this);
        LuaCore.script.Globals["MF"] = UserData.Create(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnNode(string infoName)
    {
        var infoNode = GameInfoData.GetInfoNodeByName(infoName);

        var node = Instantiate(node_Pref);
        node.color = Commons.StrToColor(infoNode.backgroundColor);
    }
    
}

