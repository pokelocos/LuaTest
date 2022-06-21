using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    [SerializeField] private Gradient fadeGradient; // ??? esto es para cambiar el color de si el nodo recividor puede o no recivir o no ???

    [Header("Pref references")]
    [SerializeField] private new BoxCollider2D collider;
    //[SerializeField] private ingredeintView ingredientView;

    private ConnectionView connectionView;
    private ClockTimer timer;
    private NodeController from, to;
    private IngredientData ingredientAllowed;// <- esto podria ser plural private List<IngredientData> ingredientsAllowed;

    public NodeController GetInputNode() => from;
    public NodeController GetoutputNode() => to;
    public IngredientData GetIngredientAllowed() => ingredientAllowed;


    public void Awake()
    {
        
    }

    public void InitEvent()
    {

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

    /// <summary>
    /// updates the position and rotation of the collision to fit 
    /// the size corresponding to the connection.
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="size"></param>
    private void UpdateBoxCollider(BoxCollider2D collider, Vector2 size)
    {
        collider.size = size;
        collider.offset = new Vector2(size.x / 2, collider.offset.y);
    }

}
