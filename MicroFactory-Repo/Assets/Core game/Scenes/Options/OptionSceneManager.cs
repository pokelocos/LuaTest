using DataSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSceneManager : MonoBehaviour
{
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider effectVol;
    private Data data;

    private void Awake()
    {
      
    }

    private void Start()
    {
        data = DataManager.LoadData<Data>();
        masterVol.value = data.options.generalVolumen;
        musicVol.value = data.options.musicVolumen;
        effectVol.value = data.options.effectVolumen;
    }

    public void SetMasterVolumen(System.Single f)
    {
        Debug.Log(f);
        data.options.generalVolumen = f;

        var radio = Radio.mainRadio;
        radio.GetSource(0).volume = data.options.musicVolumen * data.options.generalVolumen;
        radio.GetSource(1).volume = data.options.effectVolumen * data.options.generalVolumen;
        DataManager.SaveData<Data>(data);
    }

    public void SetMusicVolumnen(System.Single f)
    {
        data.options.musicVolumen = f;
        var radio = Radio.mainRadio;
        radio.GetSource(0).volume = data.options.musicVolumen * data.options.generalVolumen;
        DataManager.SaveData<Data>(data);
    }

    public void SetEffectVolumen(System.Single f)
    {
        data.options.effectVolumen = f;
        var radio = Radio.mainRadio;
        radio.GetSource(1).volume = data.options.effectVolumen * data.options.generalVolumen;
        DataManager.SaveData<Data>(data);
    }
}
