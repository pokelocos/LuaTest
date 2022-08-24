using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads();

        timeHandler.OnEndCycle += () => OnEndCycle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEndCycle()
    {
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
