using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();

    public void PlayRandomSound()
    {
        var source = Radio.mainRadio.GetSource("SFX");
        source.PlayOneShot(sounds[Random.Range(0,sounds.Count)]);
    }

    public void PlaySoundByIndex(int n)
    {
        var source = Radio.mainRadio.GetSource("SFX");
        source.PlayOneShot(sounds[n]);
    }
}
