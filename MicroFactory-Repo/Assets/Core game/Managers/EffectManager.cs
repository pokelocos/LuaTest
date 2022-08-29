using MicroFactory;
using RA.CommandConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectController effect_Pref;
    [SerializeField] private Transform pivot;

    private List<EffectController> effects = new List<EffectController>();

    // Start is called before the first frame update
    void Start()
    {
        LoadCommnads();
    }

    public List<EffectController> GetEffects() => new List<EffectController>(effects);

    public EffectController CreateEffectByIndex(int i, float startTime = 0)
    {
        var data = ResourcesLoader.GetEffect(i);
        var effect = Instantiate(effect_Pref, pivot);
        effect.Init(data, startTime);
        effects.Add(effect);
        return effect;
    }

    public EffectController CreateEffectByName(string name, float startTime = 0)
    {
        var data = ResourcesLoader.GetEffect(name);
        var effect = Instantiate(effect_Pref, pivot);
        effect.Init(data, startTime);
        effects.Add(effect);
        return effect;
    }

    public void CreateEffectByTag(string s)
    {
        //var effects = ResourcesLoader.GetEffect(s);
        //var effect = effects[Random.Range(0, effects.Length)];
        //CreateEffectByName(effect.name);
    }

    internal void RemoveAll()
    {
        effects.ForEach(e => Destroy(e.gameObject));
        effects = new List<EffectController>();
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
