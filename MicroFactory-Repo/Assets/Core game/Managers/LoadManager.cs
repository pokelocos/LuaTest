using DataSystem;
using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private EffectManager effectManager;

    private GameState loadState;
    private GameState saveState;

    private void Awake()
    {
        ResourcesLoader.LoadDataGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Load()
    {
        var data = DataManager.LoadData<Data>();
        loadState = data.gameState;

        if(loadState == null)
            return;

        LoadNodes();
        LoadConnections();
        LoadEffect();
        // Load Mods (??)
        // Load Basic stats (??)
    }
    public void Save()
    {
        saveState = new GameState();
        saveState.nodes = GenerateNodesState();
        saveState.connections = GenerateConnectionsState();
        //saveState.effects = GenerateEffectsState();
        // save mods (??)
        // save basic stats (??)

        var d = DataManager.LoadData<Data>();
        d.gameState = saveState;
        DataManager.SaveData<Data>(d);
    }

    private void LoadEffect()
    {
        foreach (var state in loadState.effects)
        {
            var effect = effectManager.CreateEffectByName(state.name, state.currentTime);
        }
    }

    
    private GameState.EffectState[] GenerateEffectsState()
    {
        var toReturn = new List<GameState.EffectState>();
        foreach (var effect in effectManager.GetEffects())
        {
            var timer = effect.GetComponent<ClockTimer>();
            var name = effect.Data.effectName;
            var state = new GameState.EffectState(name, timer.Current);
            toReturn.Add(state);
        }
        Debug.Log("<color=#FFC300>[Node Engine, saveSys]</color> <b>" + toReturn.Count + "</b> effects saved.");
        return toReturn.ToArray();
    }
    

    public void LoadNodes()
    {
        foreach (var state in loadState.nodes)
        {
            var node = nodeManager.CreateNodeByName(state.name, state.currentTime);
            node.transform.position = state.Position;
        }
    }

    public GameState.NodeState[] GenerateNodesState()
    {
        var toReturn = new List<GameState.NodeState>();
        foreach (var node in nodeManager.GetNodes())
        {
            var timer = node.GetComponent<ClockTimer>();
            var state = new GameState.NodeState(node.Data.nodeName, node.transform.position.x, node.transform.position.y, timer.Current);
            toReturn.Add(state);
        }
        Debug.Log("<color=#FFC300>[Node Engine, saveSys]</color> <b>" + toReturn.Count + "</b> nodes saved.");
        return toReturn.ToArray();
    }

    internal void LoadConnections()
    {
        var nodesState = loadState.nodes;
        var connectionsState = loadState.connections;
        foreach (var state in connectionsState)
        {
            var ns1 = nodesState[state.nodeRelationIndex.Item1];
            var ns2 = nodesState[state.nodeRelationIndex.Item2];
            var n1 = nodeManager.GetNode(ns1.name);
            var n2 = nodeManager.GetNode(ns2.name);
            var ing = ResourcesLoader.GetIngredient(state.ingredientName);
            var connection = connectionManager.CreateConnection(n1, n2, ing);
        }
    }

    public GameState.ConnectionState[] GenerateConnectionsState()
    {
        var toReturn = new List<GameState.ConnectionState>();
        
        foreach (var connection in connectionManager.GetConnections)
        {
            var timer = connection.GetComponent<ClockTimer>();
            var n1 = nodeManager.GetNodes().IndexOf(connection.GetInputNode());
            var n2 = nodeManager.GetNodes().IndexOf(connection.GetOutputNode());
            var state  = new GameState.ConnectionState(n1,n2,connection.GetIngredientAllowed().ingredientName,timer.Current);
            toReturn.Add(state);
        }
        Debug.Log("<color=#FFC300>[Node Engine, saveSys]</color> <b>" + toReturn.Count + "</b> connections saved.");
        return toReturn.ToArray();
    }
}
