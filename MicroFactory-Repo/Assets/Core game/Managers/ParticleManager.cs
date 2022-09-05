using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MoonSharpUserData]
public class ParticleManager : MonoBehaviour
{
    public List<particleObj> particles;

    private void Awake()
    {
        UserData.RegisterType<ParticleManager>();
        var pm = UserData.Create(this);
        LuaCore.Script.Globals.Set("Particler", pm);
        LuaCore.Script.Globals.Set("ParticleManager", pm);
    }

    private void Start()
    {

    }


    public void SpawnIconParticle(string name,float x, float y)
    {
        var p = particles.Find(p => p.name == name);
        Instantiate(p.obj, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void SpanwNumberParticle(string name, float x,float y, float value)
    {
        var p = particles.Find(p => p.name == name);
        var mf = Instantiate(p.obj, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoneyFeedback>();
        mf.Show(null,value);
    }

    internal void RemoveAll()
    {
        throw new NotImplementedException();
    }

    [System.Serializable]
    public struct particleObj
    {
        public string name;
        public GameObject obj;
    }
}
