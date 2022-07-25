using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music = new List<AudioClip>();

    void Start()
    {
        var source = Radio.mainRadio.GetSource(0);
        source.loop = true;
        source.PlayOneShot(music[Random.Range(0,music.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
