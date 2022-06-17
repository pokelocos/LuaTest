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

    public SpriteRenderer node_Pref;

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

        
    }

    public void SpawnNode(string infoName)
    {
        var infoNode = GameInfoData.GetInfoNodeByName(infoName);

        var node = Instantiate(node_Pref);
        node.color = Commons.StrToColor(infoNode.backgroundColor);
    }
    
}

