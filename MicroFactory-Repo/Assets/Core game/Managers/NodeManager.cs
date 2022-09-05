using RA.CommandConsole;
using RA.Inputs;
using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private NodeController node_Pref;
    [SerializeField] private ParticleManager particleManager;

    private List<NodeController> nodes = new List<NodeController>();

    public List<NodeController> GetNodes()
    {
        return new List<NodeController>(nodes);
    }

    public float GetMaintainCost()
    {
        var total = 0f;
        nodes.ForEach(n => total += n.GetMaintainCost()); // aqui  podria calcularse un multiplicador e coste global (!!!)
        return total;
    }

    public NodeController CreateNodeByIndex(int i, float startTime = 0f)
    {
        var data = ResourcesLoader.GetNode(i);
        var node = Instantiate(node_Pref, Vector2.zero, Quaternion.identity);
        node.Init(data, startTime);
        nodes.Add(node);
        return node;
    }

    internal NodeController GetNode(int index)
    {
        return nodes[index];
    }

    internal NodeController GetNode(string name)
    {
        return nodes.Find(n => n.Data.nodeName == name);
    }

    public NodeController CreateNodeByName(string name, float startTime = 0f)
    {
        var data = ResourcesLoader.GetNode(name);
        var node = Instantiate(node_Pref, Vector2.zero, Quaternion.identity);
        node.Init(data, startTime);
        node.OnEndRecipe += (value, pos) => {
            particleManager.SpanwNumberParticle("money", pos.x, pos.y, value);
            };
        nodes.Add(node);
        return node;
    }

    internal void RemoveAll()
    {
        nodes.ForEach(n => Destroy(n.gameObject));
        nodes = new List<NodeController>();
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
        Destroy(node.gameObject);
    }

    public void RemoveNodeByindex(int n)
    {
        var node = nodes[n];
        nodes.RemoveAt(n);
        Destroy(node.gameObject);

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
