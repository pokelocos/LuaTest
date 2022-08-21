using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    /// <summary>
    /// This is a generic class that is used by the rest of the solution to handle the data.
    /// Use this class as a base object to store all the information that you don't want to
    /// be lost from your program. Remember that all objects must be "Serializable" so that
    /// their value is saved correctly.
    /// </summary>
    [System.Serializable]
    public class Data
    {
        // <----
        // Fill, This area whit varibles and object you want to save.
        //DESCOMENTAR
        public StatisticsData stats = new StatisticsData();
        public GameState LastGame = null;
        public GameState gameState = null;
        public SettingsData options = new SettingsData();
        public ModsAllowedData modsAllowed = new ModsAllowedData();
        // <----

        /// <summary>
        /// Basic constructor, necesary to make a data file.
        /// </summary>
        public Data() { }

        public bool IsGameInProgress()
        {
            return gameState != null;
        }
    }


}
