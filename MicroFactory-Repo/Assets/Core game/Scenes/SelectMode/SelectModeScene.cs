using DataSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModeScene : MonoBehaviour
{
    private GameModeData modeSelected;

    [Header("Scene references")]
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.interactable = modeSelected != null;
    }

    public void SelectMode(GameModeData gameMode) // button function
    {
        modeSelected = gameMode;
        descriptionText.text = gameMode.description;
        playButton.interactable = true;
    }

    public void InitGameRun(GameModeData gameMode) //"GameMode.data" to "GameState"
    {
        Data data = DataManager.LoadData<Data>();
        var gameState = new GameState();

        gameState.basicStats.money = gameMode.startMoney;

        var nodes = new List<GameState.NodeState>();
        foreach (var starNode in gameMode.nodes)
        {
            var node = starNode.nodeData;
            var pos = starNode.position;
            nodes.Add(new GameState.NodeState(node.nodeName, pos.x, pos.y, 0));
        }
        gameState.nodes = nodes.ToArray();

        ResourcesLoader.allowModData = gameMode.allowMods;

        data.gameState = gameState;
        DataManager.SaveData<Data>(data);
    }

    public void InitGameRun()
    {
        InitGameRun(modeSelected);
    }
}
