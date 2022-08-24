using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectControler effect_Pref;

    private List<EffectControler> effects = new List<EffectControler>();

    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads();
    }

    public void CreateEffectByIndex(int i)
    {
        var data = ResourcesLoader.GetEffect(i);
        var effect = Instantiate(effect_Pref, Vector2.zero, Quaternion.identity);
        effect.Init(data, 0);
        effects.Add(effect);
    }

    public void CreateEffectByName(string name)
    {
        var data = ResourcesLoader.GetEffect(name);
        var effect = Instantiate(effect_Pref, Vector2.zero, Quaternion.identity);
        effect.Init(data, 0);
        effects.Add(effect);
    }

    public void CreateEffectByTag(string s)
    {
        //var effects = ResourcesLoader.GetEffect(s);
        //var effect = effects[Random.Range(0, effects.Length)];
        //CreateEffectByName(effect.name);
    }

    internal void RemoveAll()
    {
        throw new System.NotImplementedException();
    }

    public void CreateEffectRandom()
    {
        var randIndex = Random.Range(0, ResourcesLoader.EffectAmount());
        CreateEffectByIndex(randIndex);
    }

    public void RemoveEffectByName(string name)
    {
        var effect = effects.Find(e => e.name.Equals(name));
        effects.Remove(effect);
    }

    public void RemoveNodeByIndex(int n)
    {
        effects.RemoveAt(n);
    }

    private void LoadCommnads()
    {
        { 
            var spawnNode = new DebugCommand<string>(
                "SpawnEffect",
                "Spawn a effect by name.",
                "Spawn effect <name>", (x) =>
                {
                    CreateEffectByName(x);
                });
            Commands.commandList.Add(spawnNode);
        }

        {
            var removeNode = new DebugCommand<string>(
                "RemoveEffect",
                "Remove the first effect it finds with the corresponding name.",
                "Remove effect <name>", (x) =>
                {
                    RemoveEffectByName(x);
                });
            Commands.commandList.Add(removeNode);
        }
    }
}
