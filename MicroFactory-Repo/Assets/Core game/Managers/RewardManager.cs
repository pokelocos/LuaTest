using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads(); // me gustaria no tener que acordarme de cargar este script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCommnads()
    {
        var getReward = new DebugCommand("GetReward", "Generate a random prize for the player to choose.", "get reward", () => {
           // Debug.Log("a");
        });
        Commands.commandList.Add(getReward);
    }
}
