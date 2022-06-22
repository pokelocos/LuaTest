using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    [SerializeField] private Gradient fadeGradient; // ??? esto es para cambiar el color de si el nodo recividor puede o no recivir o no ???
    [SerializeField] private float speed = 3;

    [Header("Pref references")]
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private IngredientController ingredient_Pref;

    private ConnectionView connectionView;
    private NodeController from, to;
    private IngredientData ingredientAllowed;

    private List<IngredientController> shippingIngredients = new List<IngredientController>();

    public NodeController GetInputNode() => from;
    public NodeController GetOutputNode() => to;
    public IngredientData GetIngredientAllowed() => ingredientAllowed;

    public void Awake()
    {
        connectionView = GetComponent<ConnectionView>();
    }

    public void InitEvent()
    {

    }

    public void SendProduct()
    {
        var ing = Instantiate(ingredient_Pref, this.transform);
        ing.Init(this, ingredientAllowed, speed, IngredientDelivered);
        shippingIngredients.Add(ing);
    }

    public void IngredientDelivered()
    {
        to.Addingredient(this, ingredientAllowed);
    }


    public void Connect(NodeController from,NodeController to,IngredientData ingredientAllowed)
    {
        this.from = from;
        this.to = to;
        this.ingredientAllowed = ingredientAllowed;
    }

    public void Disconnect()
    {
        from.RemoveConnection(this);
        to.RemoveConnection(this);

        Destroy(this.gameObject);
    }

    private void Update()
    {
        this.transform.position = from.transform.position;
        this.transform.right = to.transform.position - from.transform.position;
        var dis = Vector3.Distance(from.transform.position, to.transform.position);
        connectionView.SetSize(dis);
        UpdateBoxCollider(new Vector2( dis, collider.size.y)); // haciendo cosas raras
    }

    /// <summary>
    /// updates the position and rotation of the collision to fit 
    /// the size corresponding to the connection.
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="size"></param>
    private void UpdateBoxCollider(Vector2 size)
    {
        collider.size = size;
        collider.offset = new Vector2(size.x / 2, collider.offset.y);
    }

}
