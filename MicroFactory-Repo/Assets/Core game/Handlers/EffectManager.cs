using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectControler effect_Pref;

    private List<EffectControler> effect = new List<EffectControler>();

    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads();
    }

    private void LoadCommnads()
    {
        var spawnNode = new DebugCommand<string>(
            "SpawnEffect",
            "Spawn a effect by name.",
            "Spawn effect <name>", (x) => {
            //CreateNodeByName(x);
        });
        Commands.commandList.Add(spawnNode);


        var removeNode = new DebugCommand<string>(
            "RemoveEffect",
            "Remove the first effect it finds with the corresponding name.",
            "Remove effect <name>", (x) => {
                //RemoveNodeByName(x);
            });
        Commands.commandList.Add(removeNode);
    }
}
