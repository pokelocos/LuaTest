using DataSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMods : MonoBehaviour
{
    public ModView modView_Pref;
    public RectTransform pivot;

    public ModView main;
    public Text extraInfo;

    void Start()
    {
        if(ModLoader.ModsCount <= 0)
        {
            main.gameObject.SetActive(false);
            extraInfo.text = "No mods found.";
        }
        else
        {
            SetModPanels();
            var mod = ModLoader.GetMod(0);
            main.SetInfo(mod);
            ShowContent(mod);
        }
    }

    private void SetModPanels()
    {
        for (int i = 0; i < ModLoader.ModsCount; i++)
        {
            // set basic info
            var mod = ModLoader.GetMod(i);
            var basicinfo = mod.basicInfo;
            var view = Instantiate(modView_Pref, pivot.transform);
            view.SetInfo(mod);

            // set toggle status
            var data = StaticData.Data;
            var toggle = view.GetComponentInChildren<ToggleMark>();
            var value = data.modsAllowed.TryGetAllowed(basicinfo.name);
            toggle.SetValue(value);
            toggle.OnChange = (v) =>
            {
                data.modsAllowed.TrySetAllowed(basicinfo.name, v);
                DataManager.SaveData<Data>(data);
            };

            //set panel action (set in main view)
            var general = view.GetComponent<Button>();
            general.onClick.AddListener(() =>
            {
                main.SetInfo(mod);
                ShowContent(mod);
            });
        }
        var h = modView_Pref.GetComponent<RectTransform>();
        var size = (h.sizeDelta.y + 15) * ModLoader.ModsCount;
        pivot.sizeDelta = new Vector2(pivot.sizeDelta.x, size);
    }

    private void ShowContent(ModLoader.Mod mod)
    {
        var msg = "";
        msg += "Game modes: " + mod.gameModesInfo.Count + "\n";
        msg += "Nodes: " + mod.nodesInfo.Count + "\n";
        msg += "Effects: " + mod.effectsInfo.Count + "\n";
        msg += "Recipes: " + mod.recipesInfo.Count + "\n";
        msg += "Ingredients: " + mod.ingredientsInfo.Count + "\n";
        //msg += "LuaCode: " + mod.luaCode.Count + "\n";
        //msg += "Images: " + mod.images.Count + "\n";
        //msg += "Sounds: " + mod.audios.Count + "\n";

        extraInfo.text = msg;
    }
}
