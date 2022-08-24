using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private ConnectionController connection_Pref;

    [SerializeField] private ConnectionData normalConnetion;
    [SerializeField] private ConnectionData errorConnetion;
    [SerializeField] private ConnectionData unmatchConnection;

    [Header("Scene References")]
    [SerializeField] private ConnectionPreview connectionPreview;
    [SerializeField] private IngredientPreview ingredientPreview;

    private NodeController currentNode;
    private NodeController otherNode;
    private IngredientData currentIngredient;
    private ConnectionData _fromConnectionData;
    private List<ConnectionController> connections = new List<ConnectionController>();

    public delegate void ConnectionEvent(ConnectionController c);
    public ConnectionEvent OnCreateConnection;

    public int TotalConnectionAmount => connections.Count();

    internal void RemoveAll()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        connectionPreview.gameObject.SetActive(false);
        ingredientPreview.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        var input = 1;// Right click
        if (Input.GetMouseButtonDown(input))
        {
            currentNode = GetNodeUnderPointer(mousePosition);
            if(currentNode != null)
                StartDragConnection(currentNode);
        }
        else if (Input.GetMouseButton(input))
        {
            otherNode = GetNodeUnderPointer(mousePosition);
            if (currentNode != null)
                DragConnection(currentNode, otherNode, mousePosition);
        }
        else if (Input.GetMouseButtonUp(input))
        {
            if (currentNode != null && otherNode != null)
                EndDragConnection(currentNode, otherNode);

            ClearReferences();
        }
    }

    private NodeController GetNodeUnderPointer(Vector3 position)
    {
        Collider2D target = Physics2D.OverlapPoint(position);
        if (target == null)
            return null;

        return target.gameObject.GetComponent<NodeController>();
    }

    private void StartDragConnection(NodeController startNode)
    {
        if(startNode.CurrentRecipe == null || startNode.CurrentRecipe.outputIngredients.Count <= 0)
        {
            return; // esto podria ser su propia data tipo "noPosibleConnection".
            //connectionPreview.SetView(noPosibleConnection); 
            //_fromConnectionData = noPosibleConnection;
        }

        if (startNode.OutputCount < startNode.MaxOutput)
        {
            //connectionPreview.SetView(normalConnetion);
            _fromConnectionData = normalConnetion;
            currentIngredient = startNode.CurrentRecipe.outputIngredients[currentNode.OutputCount]; //<< tryGetNextProduct() (???)
            ingredientPreview.SetView(currentIngredient);
        }
        else
        {
            connectionPreview.SetView(errorConnetion);
            _fromConnectionData = errorConnetion;
        }

        connectionPreview.gameObject.SetActive(true);
    }

    private void DragConnection(NodeController starNode, NodeController otherNode,Vector3 mousePosition)
    {
        Vector3 from = currentNode.transform.position + Vector3.zero;
        Vector3 to = mousePosition + Vector3.zero;
        connectionPreview.SetPosition(from, to);

        if(_fromConnectionData != normalConnetion) // si la conexion ya fue marcada como error
            return;

        if (otherNode == null || otherNode == currentNode)
        {
            connectionPreview.SetView(_fromConnectionData);
            return;
        }

        if (otherNode.InputCount + 1 > otherNode.MaxInput)
        {
            connectionPreview.SetView(errorConnetion);
            return;
        }

        if(!otherNode.CannConnect(currentIngredient))
        {
            connectionPreview.SetView(unmatchConnection);
        }
        else 
        {
            connectionPreview.SetView(_fromConnectionData);
        }
    }

    private void EndDragConnection(NodeController starNode, NodeController otherNode)
    {
        if (otherNode.InputCount + 1 > otherNode.MaxInput)
            return;

        if(!otherNode.CannConnect(currentIngredient))
            return;

        CreateConnection(starNode, otherNode, currentIngredient);
    }

    private void ClearReferences()
    {
        connectionPreview.gameObject.SetActive(false);
        ingredientPreview.gameObject.SetActive(false);
        currentNode = null;
        otherNode = null;
        currentIngredient = null;
    }

    private void CreateConnection(NodeController from, NodeController to,IngredientData ingredient)
    {
        var connection = Instantiate(connection_Pref, from.transform);
        connections.Add(connection);
        connection.Connect(from, to, ingredient);

        from.AddOutput(connection);
        to.AddInput(connection);
        
        OnCreateConnection?.Invoke(connection);
    }

}
