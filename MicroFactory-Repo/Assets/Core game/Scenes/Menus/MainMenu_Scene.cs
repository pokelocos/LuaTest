using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataSystem;
using UnityEngine.SceneManagement;

public class MainMenu_Scene : MonoBehaviour
{
    public Button continueButton;

    public DisplayDialog areYouSurePanel;

    private void Awake()
    {
        //areYouSurePanel.SetActive(false);

        var data = StaticData.Data;

        if(!data.IsGameInProgress())
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TryPlayButton()
    {
        var data = StaticData.Data;

        if (data.IsGameInProgress())
        {
            areYouSurePanel.Display("Are you sure to start a new game?", NewGame, "If you start a new game, the progress of the current game will be lost.");
        }
        else
        {
            SceneManager.LoadScene("Select Mode Scene");
        }
    }

    public void NewGame()
    {
        var data = DataManager.LoadData<Data>();
        data.gameState = null;
        SceneManager.LoadScene("Select Mode Scene");
    }

}
