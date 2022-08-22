using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    [SerializeField] public BasicStats basicStats;
    //[SerializeField] private ExtraStats extraStats;
    [SerializeField] public EffectState[] effects;
    [SerializeField] public NodeState[] nodes;
    [SerializeField] public ConnectionState[] connections;
    [SerializeField] public string[] mods;

    public GameState(BasicStats basicStats, EffectState[] effects, NodeState[] nodes, ConnectionState[] connections, string[] mods)
    {
        this.basicStats = basicStats;
        this.effects = effects;
        this.nodes = nodes;
        this.connections = connections;
        this.mods = mods;
    }

    public GameState()
    {
        this.basicStats = new BasicStats(0,0,0,0,""); // el string tiene que ser el nombre del modo de juego basico
        this.nodes = new NodeState[0];
        this.effects = new EffectState[0];
        this.connections = new ConnectionState[0];
        this.mods = new string[0];
    }

    [Serializable]
    public struct BasicStats
    {
        public int money;
        public int cycle;
        public int currentTime;
        public int points;
        public string gameMode;

        public BasicStats(int money, int cycle, int currentTime, int points, string gameMode)
        {
            this.money = money;
            this.cycle = cycle;
            this.currentTime = currentTime;
            this.points = points;
            this.gameMode = gameMode;
        }
    }

    [Serializable]
    public struct ExtraStats
    {
        public List<Tuple<string, float>> floatStats;
        public List<Tuple<string, string>> stringStats;

        public ExtraStats(List<Tuple<string, float>> floatStats, List<Tuple<string, string>> stringStats)
        {
            this.floatStats = floatStats;
            this.stringStats = stringStats;
        }
    }

    [Serializable]
    public struct NodeState
    {
        public string name;
        [SerializeField] public Tuple<float, float> position;
        public float currentTime;

        public Vector2 Position
        {
            get
            {
                return new Vector2(position.Item1, position.Item2);
            }
        }

        public NodeState(string name, float x, float y, float currentTime)
        {
            this.name = name;
            this.position = new Tuple<float, float>(x, y);
            this.currentTime = currentTime;
        }
    }

    [Serializable]
    public struct EffectState
    {
        public string name;
        public float currentTime;

        public EffectState(string effectName, float currentTime)
        {
            this.name = effectName;
            this.currentTime = currentTime;
        }
    }

    [Serializable]
    public struct ConnectionState
    {
        public Tuple<int, int> nodeRelationIndex;
        public string ingredientName;
        public float currentTime;

        public ConnectionState(Tuple<int, int> nodeRelationIndex, string ingredientName, float currentTime)
        {
            this.nodeRelationIndex = nodeRelationIndex;
            this.ingredientName = ingredientName;
            this.currentTime = currentTime;
        }

        public ConnectionState(int n1, int n2, string ingredientName, float currentTime)
        {
            this.nodeRelationIndex = new Tuple<int, int>(n1, n2);
            this.ingredientName = ingredientName;
            this.currentTime = currentTime;
        }
    }

}
