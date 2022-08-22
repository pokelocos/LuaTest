using DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsScene : MonoBehaviour
{
    public Text labelNoLastGame;
    public Text lastGameText;
    public Text stadisticsText;

    public LayoutAdjuster adjuster;
    private void Start()
    {
        var data = DataManager.LoadData<Data>();
        var statistics = data.stats;
        var lastGame = data.LastGame;

        // last game
        if(lastGame == null)
        {
            labelNoLastGame.gameObject.SetActive(true);
            lastGameText.gameObject.SetActive(false);
        }
        else
        {
            labelNoLastGame.gameObject.SetActive(false);
            lastGameText.gameObject.SetActive(true);
            var msg = "";
            msg += "Game Mode: " + lastGame.basicStats.gameMode + "\n";
            msg += "Nodes: " + lastGame.nodes.Count() + "\n"; // esto deveria mostrar los iconos de los nodos u otra representacion visual

            lastGameText.text = msg;
        }

        var msg2 = "";

        // general stats
        //var timePlayed = // ¿Steam?
        msg2 += "Games played: " + statistics.gamesPlayed + "\n";
        msg2 += "Games wined: " + statistics.gamesWin + "\n";
        msg2 += "Total cycles: " + statistics.totalCiclesPlay + "\n";
        //msg2 += "Total money earned: " + "$" + extrastring.Doted(statistics.totalMoneyEarned) + "\n";
        msg2 += "Total money earned: " + "$" + extrastring.Doted(1000) + "\n";
        msg2 += "Total money earned: " + "$" + extrastring.Doted(10000000) + "\n";
        msg2 += "Total money spend: " + "$" + extrastring.Doted(statistics.totalMoneySpend) + "\n";

        // node amount
        msg2 += "Node Purchased: " + statistics.amountOfNodesPurchased.Count() + "\n";
        msg2 += "Product generated: " + statistics.amountOfProductGenerated.Count() + "\n";

        stadisticsText.text = msg2;

        adjuster.CalculateAdjust();
    }
}

public static class extrastring
{
    public static string Doted(int value)
    {
        // Gets a NumberFormatInfo associated with the en-US culture.
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        nfi.CurrencyDecimalSeparator = ",";
        nfi.CurrencyGroupSeparator = ".";
        nfi.CurrencySymbol = "";
        var r = Convert.ToDecimal(value).ToString("C0",nfi);
        return r;
    }
}