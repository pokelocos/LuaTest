using RA.CommandConsole;
using RA.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private NodeController node_Pref;

    private List<NodeController> nodes = new List<NodeController>();

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

    internal void RemoveAll()
    {
        throw new System.NotImplementedException();
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

    public void RemoveNodeByName(string name)
    {
        var node = nodes.Find(n => n.name.Equals(name));
        nodes.Remove(node);
    }

    public void RemoveNodeByindex(int n)
    {
        nodes.RemoveAt(n);
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads();
    }

    private void LoadCommnads()
    {
        var spawnNode = new DebugCommand<string>("SpawnNode", "Spawn a node by name.", "Spawn node <name>", (x) => {
            CreateNodeByName(x);
        });
        Commands.commandList.Add(spawnNode);


        var removeNode = new DebugCommand<string>(
            "RemoveNode",
            "Remove the first node it finds with the corresponding name.",
            "Remove node <name>", (x) => {
            RemoveNodeByName(x);
        });
        Commands.commandList.Add(removeNode);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        var input = 0;

        if(InputUtils.MouseDoubleCLick(input, this))
        {

        }
    }
}
