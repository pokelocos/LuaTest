using MoonSharp.Interpreter;
using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle pauseToggle;
    [SerializeField] private Toggle playToggle;
    [SerializeField] private Toggle speedPlayToggle;

    [Header("bar value")]
    [SerializeField] private Image dayBar;

    [SerializeField] private ClockTimer timer;

    public Action OnEndCycle;

    // Start is called before the first frame update
    void Start()
    {
        LuaCore.Script.Globals["Timer"] = UserData.Create(this);
        LuaCore.Script.Globals["TimeManager"] = UserData.Create(this);
        timer.OnEnd += (t) => { 
            OnEndCycle?.Invoke();
            LuaCore.DoFunction("OnCycleEnd");
            Time.timeScale = 1;
            ActualizeToggles();
        };
        timer.OnUpdate += (t) => {
            dayBar.fillAmount = ((t.Current / t.Max)); 
        };

        Time.timeScale = 0;
        ActualizeToggles();
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int v = 0;
            if (Time.timeScale == 0)
                v = 1;
            else if (Time.timeScale == 1)
                v = 4;
            else if (Time.timeScale == 4)
                v = 0;

            SetTimeScale(v);
            ActualizeToggles();
        }
    }

    public void SetTimeScale(float value)
    {
        if (value == Time.timeScale)
            return;

        Time.timeScale = value;
        LuaCore.DoFunction("OnChangeTimeSpeed");
    }

    public void ActualizeToggles()
    {
        var value = (int)Time.timeScale;
        pauseToggle.isOn = (value == 0);
        playToggle.isOn = (value == 1);
        speedPlayToggle.isOn = (value == 4);
    }
}
