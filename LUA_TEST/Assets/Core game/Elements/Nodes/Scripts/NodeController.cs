using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeView))]
[RequireComponent(typeof(ClockTimer))]
public class NodeController : MonoBehaviour
{
    [SerializeField] private NodeView nodeView;
    [SerializeField] private ClockTimer timer;

    private NodeData data;

    //private List<ConnectionController> input = new List<ConnectionController>();
    //private List<ConnectionController> output = new List<ConnectionController>();

    private void Awake()
    {
        nodeView = GetComponent<NodeView>();
    }
}
