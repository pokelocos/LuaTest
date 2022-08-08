using RA.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private NodeController node_Pref;

    //[SerializeField] private InfoPanel infoPanel;


    private List<NodeController> nodes = new List<NodeController>();

    // Start is called before the first frame update
    void Start()
    {
        
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
