using DataSystem;
using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public NodeManager nodeManager;
    public EffectManager effectmanager;
    public GameModeManager gameModeManager;
    public RewardManager rewardManager;
    public ConnectionManager connectionManager;
    public ParticleManager particleManager;


    [Header("Handlers")]
    public TimeHandler timeHandler;
    public CameraHandler cameraHandler;
    public DragHandler dragHandler;

    private int cycle = 0;
    private GameState state;

    // Start is called before the first frame update
    void Start()
    {
        var data = DataManager.LoadData<Data>();
        state = data.gameState;
       
        // ver que hacer con los mods de state.Mods (!!!)
        

        LoadCommnads();
        timeHandler.OnEndCycle += () => OnEndCycle();
        rewardManager.OnSelectedReward += (r) =>
        {
            var nodes = r.nodes.ToList();
            var effects = r.effects.ToList();
            nodes.ForEach(n => {
                nodeManager.CreateNodeByName(n.nodeName);
                });
            effects.ForEach(e => effectmanager.CreateEffectByName(e.effectName));
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEndCycle()
    {
        cycle++;
        if((cycle % 3 == 0) || cycle <= 3)
        {
            timeHandler.SetTimeScale(0);
            timeHandler.ActualizeToggles();
            var nodeDatas = ResourcesLoader.GetNodes();
            var effectDatas = ResourcesLoader.GetEffects();
            var rewards = rewardManager.GenerateRewards(nodeDatas.ToArray(), effectDatas.ToArray(), 3, UnityEngine.Random.Range(2, 4), 3, cycle); // nodeDatas y effectDatas podria ser estatica y global en otra clase que guarde datas
            rewardManager.ShowRewards(rewards);
            Debug.Log("A");
        }

        // calcular costo ede mantecion de nodos
        var cost = nodeManager.GetMaintainCost();

        var gm = gameModeManager;
        if(gm.WinCondition())
        {

        }

        if(gm.LoseWarning())
        {

        }

        if(gm.LoseCondition())
        {

        }

    }


    public void LoadCommnads()
    {
        var destroyAll = new DebugCommand("DestroyAll", "Remove all items in play.", "destroy all", () => {
            connectionManager.RemoveAll();
            nodeManager.RemoveAll();
            effectmanager.RemoveAll();
            particleManager.RemoveAll();
        });
        Commands.commandList.Add(destroyAll);

        var addMoney = new DebugCommand<string>("AddMoney", "Add the indicated amount as money.", "add money", (x) => {
            //Debug.Log("a");
        });
        Commands.commandList.Add(addMoney);

        var setCycle = new DebugCommand<string>("SetCycle", "---", "set_cycle", (x) => {
            //Debug.Log("a");
        });
        Commands.commandList.Add(setCycle);

    }
}
