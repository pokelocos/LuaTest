using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPreview : MonoBehaviour
{
    [SerializeField] ConnectionView view;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetView(ConnectionData data)
    {
        view.SetColor(data.bgColor, data.borderColor);
    }

    public void SetPosition(Vector3 from, Vector3 to)
    {
        this.transform.position = from;
        this.transform.right = to - from;
        var dis = Vector3.Distance(from, to);
        view.SetSize(dis);
    }
}
