using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialog : MonoBehaviour //mejorar nombre
{
    [Header("Properties")]
    public bool ShowToggle = false;

    [Header("Text")]
    [SerializeField] private Text main;
    [SerializeField] private Text secondary;

    [Header("Buttons")]
    [SerializeField] private DiagButton confirm;
    [SerializeField] private DiagButton decline;

    //[Header("Don't show again toggle")]
    //[SerializeField] private Toggle ShowAgain;
    //[SerializeField] private Text label;

    private void OnEnable()
    {
        //if(!ShowToggle)
        //{
        //    ShowAgain.gameObject.SetActive(false);
        //}
    }

    public void Display(string main, Action confirmAction, string secondary = "", Action declineAction = null)
    {
        Clear();
        this.gameObject.SetActive(true);

        this.main.text = main;
        this.secondary.text = secondary;

        confirm.button.onClick.AddListener(() => { confirmAction?.Invoke(); });
        confirm.button.onClick.AddListener(() => { this.gameObject.SetActive(false); });
        decline.button.onClick.AddListener(() => { declineAction?.Invoke(); });
        decline.button.onClick.AddListener(() => { this.gameObject.SetActive(false); });
    }

    public void Clear()
    {
        this.main.text = "";
        this.secondary.text = "";
        confirm.button.onClick.RemoveAllListeners();
        decline.button.onClick.RemoveAllListeners();
    }

    [Serializable]
    public struct DiagButton
    {
        public Button button;
        public Text text;
        public Image image;
    }
}
