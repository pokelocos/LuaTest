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

    public override void Show(Transform target = null,params object[] prms)
    {
        if (prms.Length <= 0)
            return;

        var value = int.Parse(prms[0].ToString());

        base.Show(target);

        text.text = textShadow.text = "$" + value;
        text.color = value >= 0 ? goodColor : badColor;
    }

    public void OnEndAnimation()
    {
        Destroy(this.gameObject);
    }
}
