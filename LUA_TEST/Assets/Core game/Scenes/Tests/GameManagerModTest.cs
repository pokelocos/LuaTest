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

    [Header("Basics prefs")]
    [SerializeField] private NodeController node_Pref;
    //[SerializeField] private EffectController effect_Pref;
    [SerializeField] private ConnectionController connetion_Pref;


    private void Awake()
    {
        LuaCore.Script.Globals["MicroFactory"] = UserData.Create(this);
        LuaCore.Script.Globals["Game"] = UserData.Create(this);
        LuaCore.Script.Globals["MF"] = UserData.Create(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResourcesLoader.allowModData = false;
        ResourcesLoader.LoadDataGame(); // esto tiene que iniciarse al momento de iniciar la partida SUPER IMPORTANTE
    }

    public void CreateNodeByIndex(int i)
    {
        var data = ResourcesLoader.GetNode(i);
        var node = Instantiate(node_Pref, Vector2.zero, Quaternion.identity);
        node.Init(data, 0);
    }

    public void CreateNodeByName(string name)
    {
        var data = ResourcesLoader.GetNode(name);
        var node = Instantiate(node_Pref, Vector2.zero, Quaternion.identity);
        node.Init(data, 0);
    }

    public void CreateNodeByTag(string s)
    {
        var nodes = ResourcesLoader.GetNodesByTag(s);
        var node = nodes[Random.Range(0, nodes.Length)];
        CreateNodeByName(node.name);
    }

    public void CreateNodeRandom()
    {
        var randIndex = Random.Range(0, ResourcesLoader.NodeAmount());
        CreateNodeByIndex(randIndex);
    }
}

