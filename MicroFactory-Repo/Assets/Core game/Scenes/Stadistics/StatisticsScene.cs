using DataSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsScene : MonoBehaviour
{

    public Text text;
    private void Start()
    {
        var data = DataManager.LoadData<Data>();
        var statistics = data.stats;
        var lastGame = data.LastGame;

        // last game
        if(lastGame != null)
        {

        }
        else
        {

        }
        

        // general stats
        //var timePlayed = // ¿Steam?
        var gp = statistics.gamesPlayed;
        var gw = statistics.gamesWin;
        var tcp = statistics.totalCiclesPlay;
        var tme = statistics.totalMoneyEarned;
        var tm = statistics.totalMoneySpend;

        // node amount
        var anp = statistics.amountOfNodesPurchased.Count();
        var a = statistics.amountOfProductGenerated.Count();

    }
}
