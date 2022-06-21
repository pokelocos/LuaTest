using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionHandler : MonoBehaviour
{
    [SerializeField] private ConnectionController connection_Pref;

    [SerializeField] private ConnectionData normalConnetion;
    [SerializeField] private ConnectionData errorConnetion;
    [SerializeField] private ConnectionData unmatchConnection;

    [Header("Scene References")]
    [SerializeField] private ConnectionView connectionPreview;
    //[SerializeField] private IngredientPreview ingredientPreview;

    private NodeController currentNode;
    private NodeController otherNode;
    private IngredientData currentIngredient;

    public delegate void ConnectionEvent(ConnectionController c);
    public ConnectionEvent OnCreateConnection;

    private void Awake()
    {
        connectionPreview.gameObject.SetActive(false);
        //ingredientPreview.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        var input = 1;// Right click
        if (Input.GetMouseButtonDown(input))
        {
            StartDragConnection(mousePosition);
        }
        else if (Input.GetMouseButton(input))
        {
            DragConnection(mousePosition);
        }
        else if (Input.GetMouseButtonUp(input))
        {
            EndDragConnection(mousePosition);
            currentNode = null;
            otherNode = null;
            currentIngredient = null;
        }
    }

    private void StartDragConnection(Vector3 position)
    {
        Collider2D target = Physics2D.OverlapPoint(position);

        if (target == null)
            return;

        var node = target.gameObject.GetComponent<NodeController>();
        if (node == null)
            return;

        if (node.OutputCount < node.Data.outputMax)
        {
            connectionPreview.SetView(normalConnetion);
            currentIngredient = node.CurrentRecipe.otuputingredients[node.OutputCount];
            //IngredientPreview.SetView(ingredient);
        }
        else
        {
            connectionPreview.SetView(errorConnetion);
        }

        currentNode = node;
        connectionPreview.gameObject.SetActive(true);
    }

    private void DragConnection(Vector3 mousePosition)
    {
        if (currentNode == null)
            return;

        Vector3 from = currentNode.transform.position + Vector3.zero;
        Vector3 to = mousePosition + Vector3.zero;

        connectionPreview.SetPosition(from, to);

        Collider2D target = Physics2D.OverlapPoint(mousePosition);
        if (target == null)
            return;

        var node = target.gameObject.GetComponent<NodeController>();
        if (node == null || node == currentNode)
            return;

        if(node.InputCount + 1 >= node.Data.inputMax)
        {
            connectionPreview.SetView(errorConnetion);
        }
        //else if(node.matchIngredient()) // pregunar si exite una recipe que contega el elemnto
        else 
        {
            connectionPreview.SetView(normalConnetion);
        }
        otherNode = node;
    }

    private void EndDragConnection(Vector3 position)
    {
        if (otherNode == null)
            return;

        if (otherNode.InputCount + 1 >= otherNode.Data.inputMax)
            return;

        //if(node.matchIngredient()) // pregunar si exite una recipe que contega el elemnto
        //return;

        CreateConnection(currentNode,otherNode,currentIngredient);
    }

    private void CreateConnection(NodeController from, NodeController to,IngredientData ingredient) // no se si esto deba estar aqui pero weno
    {
        var connection = Instantiate(connection_Pref, from.transform);
        connection.Connect(from, to, ingredient);
        OnCreateConnection?.Invoke(connection);
    }
}
