using RA.Inputs;
using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// un nodo no puede tenr mas de una recipe con el mismo input

[RequireComponent(typeof(NodeView))]
[RequireComponent(typeof(ClockTimer))]
public class NodeController : MonoBehaviour
{
    private NodeView nodeView;
    private ClockTimer timer;

    private NodeData data;

    private List<ConnectionController> inputs = new List<ConnectionController>();
    private List<ConnectionController> outputs = new List<ConnectionController>();
    private Inventory inventory = new Inventory();
    private RecipeData currentRecipe; // << esta varible puede ser problematica por posible NULL

    public delegate void ConnectionEvent(ConnectionController connection, NodeController node);
    public event ConnectionEvent OnConnect;
    public event ConnectionEvent OnDisconnect;
    public event ConnectionEvent OnReceiveProduct;

    public NodeData Data => data;
    public int InputCount => inputs.Count();
    public int OutputCount => outputs.Count();


    public RecipeData CurrentRecipe
    {
        get => currentRecipe;
        private set
        {
            if(value != null)
            {
                currentRecipe = value;
                timer.Max = value.time;
            }
        }
    }

    private void Awake()
    {
        nodeView = GetComponent<NodeView>();
        timer = GetComponent<ClockTimer>();
    }

    private void Start()
    {
        this.InitEvents();
         
        if(ResourcesLoader.allowModData)
            this.InitLuaEvents();

        SelectCurrentRecipe();
        TryStartProduction();
    }

   
    private void OnMouseOver()
    {
        var leftInput = 0;
        var rightInput = 1;

        if (InputUtils.MouseDoubleCLick(leftInput))
        {
            Debug.Log("Double left");
        }

        if (InputUtils.MouseDoubleCLick(rightInput))
        {
            Debug.Log("Double Right");
        }

    }

    public void Init(NodeData data, float startTime)
    {
        this.data = data;
        timer.Current = startTime;
        nodeView.SetView(data);
    }

    public void RemoveConnection(ConnectionController connection)
    {
        inputs.Remove(connection); // esto podria estar con un if els y ais sacar eventos UwU
        outputs.Remove(connection);

        OnDisconnect?.Invoke(connection,this);
    }

    /// <summary>
    /// Inicializa los evnetos importantes relacionados al 
    /// funcionamiento interno del nodo y sus componentes.
    /// </summary>
    private void InitEvents()
    {
        // Node Events
        //timer.OnStart += (clock) => { };
        timer.OnEnd += (clock) => { TryStartProduction(); };
        timer.OnUpdate += (clock) => { nodeView.SetBarAmount((clock.Current / currentRecipe.time)); };

        // Connection Event
        OnReceiveProduct += (c, n) => { TryStartProduction(); };
        OnConnect += (c, n) => { SelectCurrentRecipe(); };
        OnDisconnect += (c, n) => { SelectCurrentRecipe(); };

        // Inventory event
        //inventory.OnSuccessReceived += (c) => { };
        //inventory.OnFailReceived += (c) => { c. }; // set red connection;
    }

    /// <summary>
    /// Inicializa los evnetos importantes relacionados al 
    /// las funciones añadidas atravez de LUA.
    /// </summary>
    private void InitLuaEvents()
    {

    }

    /// <summary>
    /// Revisa las recipes que tiene el nodo y se queda con
    /// la que pueda hacer dependiendo de los inputs que tenga
    /// </summary>
    /// <returns></returns>
    private void SelectCurrentRecipe()
    {
        data.recipes.OrderByDescending(r => r.inputIngredients.Count());
        currentRecipe = null;

        var ings = inputs.Select(x => x.GetIngredientAllowed());
        var recipes = data.recipes;
        for (int i = 0; i < recipes.Count(); i++)
        {
            if (recipes[i].inputIngredients.Count() == 0)
            {
                CurrentRecipe = recipes[i];
                break;
            }

            bool hasMatch = inputs.Select(x => x.GetIngredientAllowed())
                          .Intersect(ings).Any();
            if (hasMatch)
            {
                CurrentRecipe = recipes[i];
                break;
            }
        }
    }

    /// <summary>
    /// Intenta producir, retornara verdadero si lo logra, 
    /// falso si ya esta produciendo o no tiene la capacidad 
    /// de producir la receta con los materiles que tiene
    /// </summary>
    /// <returns></returns>
    private bool TryStartProduction()
    {
        // si ya esta producionedo
        if (timer.IsActive()) 
            return false;

        if (currentRecipe == null)
            return false;

        var ingredients = currentRecipe.inputIngredients;
        // si no tengo los ingredientes
        if (!inventory.HaveIngredients(ingredients)) 
            return false;

        inventory.RemoveIngedients(ingredients);
        timer.StartClock();
        return true;
    }

    private class Inventory
    {
        private List<Report> receivedIngredient = new List<Report>();

        public delegate void InventoryEvent(ConnectionController connection);
        public event InventoryEvent OnSuccessReceived;
        public event InventoryEvent OnFailReceived;

        protected void TryAddIngredient(ConnectionController from, IngredientData ingredient) // change name to "TryAddReport" (??)
        {
            var reports = (from r in receivedIngredient 
                     where r.@from == @from 
                     select r.@from).ToList();

            if(reports.Count <= 0) 
            {
                receivedIngredient.Add(new Report(from, ingredient));
                OnSuccessReceived?.Invoke(from);
            }
            else
            {
                OnFailReceived?.Invoke(from);
            }
        }

        public bool HaveIngredients(List<IngredientData> ingredients)
        {
            var received = new List<Report>(receivedIngredient);
            for (int i = 0; i < ingredients.Count; i++)
            {
                if(received.Exists(x=> x.ingredient == ingredients[i]))
                {
                    received.Remove(received.First(x => x.ingredient == ingredients[i]));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveIngedient(IngredientData ingredient)
        {
            var report = receivedIngredient.First(x => x.ingredient == ingredient);
            receivedIngredient.Remove(report);
        }

        public void RemoveIngedients(List<IngredientData> ingredients)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                RemoveIngedient(ingredients[i]);
            }
        }
        
        private struct Report
        {
            public ConnectionController from;
            public IngredientData ingredient;

            public Report(ConnectionController from, IngredientData ingredient)
            {
                this.from = from;
                this.ingredient = ingredient;
            }
        }
    }
}