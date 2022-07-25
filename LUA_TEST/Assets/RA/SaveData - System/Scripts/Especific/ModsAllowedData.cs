using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataSystem
{
    [Serializable]
    public class ModsAllowedData
    {
        public List<Tuple<string, bool>> allowedMods;

        public bool TrySetAllowed(string name, bool allow)
        {
            try
            {
                var mod = allowedMods.First(m => m.Item1.Equals(name));

                allowedMods.Remove(mod);
                allowedMods.Add(new Tuple<string, bool>(name, allow));
                return true;
            }
            catch
            {
                return false;
            }
           
        }
    }
}