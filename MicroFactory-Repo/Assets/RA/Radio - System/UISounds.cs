using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clickSounds = new List<AudioClip>();

    public void PlayRandomClickSound()
    {
        var source = Radio.mainRadio.GetSource("SFX");
        source.PlayOneShot(clickSounds[Random.Range(0,clickSounds.Count)]);
    }
}
