using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    private ConnectionController path;

    private IngredientView view;
    private ClockTimer timer;

    private void Awake()
    {
        view = GetComponent<IngredientView>();
        timer = GetComponent<ClockTimer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    public void FollowPath()
    {
        var from = path.GetInputNode().transform.position;
        var to = path.GetOutputNode().transform.position;
        this.transform.position = Vector3.Lerp(from, to, timer.Current / timer.Max);
    }

    public void Init(ConnectionController connection, IngredientData data, float time, Action onEnd)
    {
        path = connection;
        view.SetView(data);
        timer.Max = time;
        timer.StartClock();

        timer.OnEnd += (c) => onEnd();
        timer.OnEnd += (c) => Destroy(this.gameObject);
    }
}
