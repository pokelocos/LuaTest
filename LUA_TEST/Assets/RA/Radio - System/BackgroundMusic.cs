using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music = new List<AudioClip>();

    void Start()
    {
        Debug.Log(Radio.mainRadio);
        Debug.Log(Radio.mainRadio.GetSource(0));
        var source = Radio.mainRadio.GetSource(0);
        source.loop = true;
        source.clip = music[Random.Range(0, music.Count)];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
