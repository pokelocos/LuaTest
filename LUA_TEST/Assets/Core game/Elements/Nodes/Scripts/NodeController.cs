using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// un nodo no puede tenr mas de una recipe con el mismo input

[RequireComponent(typeof(NodeView))]
[RequireComponent(typeof(ClockTimer))]
public class NodeController : MonoBehaviour //, ISelectableObject
{
    private NodeView nodeView;
    private ClockTimer timer;

    private NodeData data;

    private List<ConnectionController> input = new List<ConnectionController>();
    private List<ConnectionController> output = new List<ConnectionController>();
    private Inventory inventory = new Inventory();
    private RecipeData currentRecipe; // << esta varible puede ser problematica por posible NULL

    public delegate void ConnectionEvent(ConnectionController connection, NodeController node);
    public event ConnectionEvent OnConnect;
    public event ConnectionEvent OnDisconnect;
    public event ConnectionEvent OnReceiveProduct;

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

        SelectCurrentRecipe();
        TryStartProduction();
    }

    public void Init(NodeData data, float startTime)
    {
        this.data = data;
        timer.Current = startTime;
        nodeView.SetView(data);
    }

    public void RemoveConnection(ConnectionController connection)
    {
        input.Remove(connection); // esto podria estar con un if els y ais sacar eventos UwU
        output.Remove(connection);

        OnDisconnect?.Invoke(connection,this);
    }

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
    /// Revisa las recipes que tiene el nodo y se queda con la que pueda hacer dependiendo delos inputsa que tenga
    /// </summary>
    /// <returns></returns>
    private void SelectCurrentRecipe()
    {
        data.recipes.OrderByDescending(r => r.inputIngredients.Count());
        currentRecipe = null;

        var ings = input.Select(x => x.GetIngredientAllowed());
        var recipes = data.recipes;
        for (int i = 0; i < recipes.Count(); i++)
        {
            if (recipes[i].inputIngredients.Count() == 0)
            {
                CurrentRecipe = recipes[i];
                break;
            }

            bool hasMatch = input.Select(x => x.GetIngredientAllowed())
                          .Intersect(ings).Any();
            if (hasMatch)
            {
                CurrentRecipe = recipes[i];
                break;
            }
        }
    }

    private bool TryStartProduction()
    {
        if (timer.IsActive()) // ya esta producionedo
            return false;
        if (currentRecipe == null)
            return false;
        var ingredients = currentRecipe.inputIngredients;
        if (!inventory.HaveIngredients(ingredients)) // no tengo los ingredientes
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