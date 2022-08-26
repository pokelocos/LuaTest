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
        timer.OnEnd += (t) => { OnEndCycle?.Invoke(); };
        timer.OnUpdate += (t) => {
            dayBar.fillAmount = ((t.Current / t.Max)); 
        };

        SetTimeScale(0);
    }



    public void SetTimeScale(float value)
    {
        if (value == Time.timeScale)
            return;

        Time.timeScale = value;
    }
}
