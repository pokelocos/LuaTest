using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFeedback : ExtraFeedback
{
    public Color goodColor;
    public Color badColor;

    [SerializeField] private TextMesh text;
    [SerializeField] private TextMesh textShadow;
    [SerializeField] private Animator animator;

    public override void Show(Transform target,params object[] prms)
    {
        base.Show(target);

        string msg = prms[0].ToString();
        text.text = msg;
        textShadow.text = msg;
        text.color = int.Parse(msg) >= 0 ? goodColor : badColor;
    }
}
