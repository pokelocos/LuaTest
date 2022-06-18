using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeView))]
[RequireComponent(typeof(ClockTimer))]
public class NodeController : MonoBehaviour
{
    private NodeView nodeView;
    private ClockTimer timer;

    private NodeData data;

    //private List<ConnectionController> input = new List<ConnectionController>();
    //private List<ConnectionController> output = new List<ConnectionController>();

    public void Init(NodeData data)
    {
        this.data = data; 
    }

    private void Awake()
    {
        nodeView = GetComponent<NodeView>();
        timer = GetComponent<ClockTimer>();
    }
}
