using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    [Serializable]
    public class SettingsData
    {
        // Graphics
        public bool fullscrean = true;
        public Tuple<int, int> resolution = new Tuple<int, int>(1920, 1080);

        // Sound
        public float generalVolumen = 0.5f;
        public float effectVolumen = 1f;
        public float musicVolumen = 1f;
        public bool muteInBackGround = false;

        // Modes
        public bool TouchScreenMode = false;
        public bool enableController = false; // always false for this game

        // Misc
        public string lenguage = "En";
        public bool uploadGameplayData = false; // send info from gamplay and errors to balancing purposes and bug fix (not personal information).

        // Other
        public bool SnapMode = false;

        // Input Setting
        public KeyCode closeInfoPanel = KeyCode.Escape;
        public KeyCode hidePanel = KeyCode.Space;
        public KeyCode AcelerateTime = KeyCode.LeftControl;
        public KeyCode changeFilter = KeyCode.Tab;
    }

}