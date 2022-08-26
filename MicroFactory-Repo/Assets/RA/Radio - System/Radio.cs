using DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public static Radio mainRadio;

    [SerializeField]
    private List<string> channels = new List<string>() { "Background", "SFX"};

    private List<Channel> _channels = new List<Channel>() ;

    private void Awake()
    {
        if(this.tag.Equals("MainRadio"))
            CheckSingleton();

        foreach (var channel in channels)
        {
            var source = this.gameObject.AddComponent<AudioSource>();
            _channels.Add(new Channel(channel, source));
        }
    }

    private void Start()
    {
        var data = DataManager.LoadData<Data>();
        GetSource(0).volume = data.options.musicVolumen * data.options.generalVolumen;
        GetSource(1).volume = data.options.effectVolumen * data.options.generalVolumen;
    }

    public AudioSource GetSource(int channelID)
    {
        return _channels[channelID].source;
    }

    public AudioSource GetSource(string channelName)
    {
        try
        {
            return _channels.First(c => c.name.Equals(channelName)).source;
        }
        catch
        {
            Debug.LogWarning("The channel name '" + channelName + "' don't exist.'");
            return null;
        }
    }

    public void PlaySound(AudioClip clip, int channelID)
    {
        _channels[channelID].source.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, string channelName)
    {
        try
        {
            var channel = _channels.First(c => c.name.Equals(channelName));
            channel.source.PlayOneShot(clip);
        }
        catch
        {
            Debug.LogWarning("The channel name '" + channelName + "' don't exist.'");
        }
       
    }

    private void CheckSingleton()
    {
        if (mainRadio == null)
        {
            mainRadio = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Serializable]
    private struct Channel
    {
        public string name;
        public AudioSource source;

        public Channel(string name,AudioSource source)
        {
            this.name = name;
            this.source = source;
        }
    }
}
