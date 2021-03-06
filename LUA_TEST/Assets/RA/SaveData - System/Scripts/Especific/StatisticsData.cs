using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    [Serializable]
    public class StatisticsData
    {
        //public List<Tuple<string, int>> IntStats;
        public int gamesPlayed = 0;
        public int gamesWin = 0;
        public int totalMoneyEarned = 0;
        public int totalMoneySpend = 0;
        public int totalCiclesPlay = 0;
        public Dictionary<string, int> amountOfNodesPurchased = new Dictionary<string, int>();
        public Dictionary<string, int> amountOfProductGenerated = new Dictionary<string, int>();

    }
}
