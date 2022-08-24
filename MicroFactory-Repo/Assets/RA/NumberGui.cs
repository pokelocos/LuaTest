using RA.UtilMonobehaviours;
using UnityEngine;

[RequireComponent(typeof(ClockTimer))]
public class NumberGui : TextGui
{
    private ClockTimer timer;
    private void Awake()
    {
        timer = GetComponent<ClockTimer>();
    }

    public void SetDelayedValue(float value, float delayTime, int steps)
    {
        timer.StopClock();
        timer.ClearEvent();
        var last = float.Parse(valueText.text);
        timer.Max = delayTime;

        var currentStep = 0;
        var timeStep = delayTime / (steps * 1f);

        timer.OnUpdate += (t) => {
            if (t.Current >= (currentStep * timeStep))
            {
                currentStep++;
                var v = last + (value - last) * (t.Current / t.Max);
                SetValue(((int)v).ToString());
            }
        };
        timer.OnEnd += (t) => SetValue(value.ToString());
        timer.StartClock();
    }
}


