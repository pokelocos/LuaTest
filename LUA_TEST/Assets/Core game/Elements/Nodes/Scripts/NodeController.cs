using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private RecipeData current;

    public delegate void ConnectionEvent(ConnectionController connection, NodeController node);
    public event ConnectionEvent OnConnect;
    public event ConnectionEvent OnDisconnect;
    public event ConnectionEvent OnReceiveProduct;

    private void Awake()
    {
        nodeView = GetComponent<NodeView>();
        timer = GetComponent<ClockTimer>();

        this.InitEvents();
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
    }

    private void InitEvents()
    {
        // Node Events
        //timer.OnStart += (n) => { };
        timer.OnEnd += (n) => { TryStartProduction(); };

        // Connection Event
        OnReceiveProduct += (c, n) => { TryStartProduction(); };
    }

    private bool TryStartProduction()
    {
        if (timer.IsActive()) // ya esta producionedo
            return false;

        var ingredients = current.inputIngredients;
        if (!inventory.HaveIngredients(ingredients)) // no tengo los ingredientes
            return false;

        inventory.RemoveIngedients(ingredients);
        timer.StartClock();
        return true;
    }

    private class Inventory
    {
        private List<Report> receivedIngredient = new List<Report>();

        protected void TryAddIngredient(ConnectionController from, IngredientData ingredient) // change name to "TryAddReport" (??)
        {
            var reports = (from r in receivedIngredient 
                     where r.@from == @from 
                     select r.@from).ToList();

            if(reports.Count <= 0) 
            {
                receivedIngredient.Add(new Report(from, ingredient));
            }
            else
            {
                // ya exite asi que no hago nada UwU // Evento (???)
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