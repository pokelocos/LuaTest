using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFeedback : MonoBehaviour
{
    public bool loop;
    public bool followTarget;

    [SerializeField] private Vector3 offset;

    private Transform target;

    public virtual void Show(Transform target,params object[] prms)
    {
        this.target = target;
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        target = null;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(followTarget)
        {
            this.transform.position = target.position + offset;
        }
    }

}
