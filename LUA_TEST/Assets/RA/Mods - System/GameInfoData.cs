using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfoData // este es un nombre de mierda correguir cuando sea necesario
{
    public static NodeInfo GetInfoNodeByName(string infoName, bool vanillaInfo = true, bool modsInfo = true) // probablemnte en vez de node info regrese elscriptable data
    {
        // revisa la lista de la info cargada y debuelve la que corresponde
        if(vanillaInfo)
        {
            // implementar
        }
        if(modsInfo)
        {
            for (int i = 0; i < ModLoader.ModsCount; i++)
            {
                var mod = ModLoader.GetMod(i);
                for (int j = 0; j < mod.nodesInfo.Count; j++)
                {
                    var info = mod.nodesInfo[j];

                    if (info.name.Equals(infoName))
                    {
                        return info;
                    }
                }
            }
        }
        return default;
    }
}
