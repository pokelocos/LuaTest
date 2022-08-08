using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMods : MonoBehaviour
{
    // esto deberia ir en una clase especial
    public ModView modView_Pref;
    public Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ModLoader.ModsCount; i++)
        {
            var basicinfo = ModLoader.GetMod(i).basicInfo;
            var view = Instantiate(modView_Pref,pivot);
            view.SetInfo(basicinfo.thumnail,basicinfo.name,basicinfo.author,basicinfo.version,basicinfo.description);
        }
    }
}
