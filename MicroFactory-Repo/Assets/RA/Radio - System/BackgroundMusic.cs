using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Radio))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music = new List<AudioClip>();

    void Start()
    {
        var source = Radio.mainRadio.GetSource(0);
        source.loop = true;
        source.clip = music[Random.Range(0, music.Count)];
        source.Play();
    }
}
