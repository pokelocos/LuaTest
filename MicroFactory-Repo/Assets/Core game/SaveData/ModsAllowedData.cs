using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataSystem
{
    [System.Serializable]
    public class ModsAllowedData
    {
        public List<Tuple<string, bool>> allowedMods = new List<Tuple<string, bool>>();

        public ModsAllowedData() { }

        public bool TryGetAllowed(string name)
        {
            try
            {
                var mod = allowedMods.First(m => m.Item1.Equals(name));
                return mod.Item2;
            }
            catch
            {
                allowedMods.Add(new Tuple<string, bool>(name, false));
                return false;
            }
        }

        public bool TrySetAllowed(string name, bool allow)
        {
            try
            {
                var mod = allowedMods.First(m => m.Item1.Equals(name));

                allowedMods.Remove(mod);
                allowedMods.Add(new Tuple<string, bool>(name, allow));
                return allow;
            }
            catch
            {
                allowedMods.Add(new Tuple<string, bool>(name, false));
                return false;
            }
           
        }
    }
}