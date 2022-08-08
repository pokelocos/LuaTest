using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPreview : MonoBehaviour
{
    [Header("Varibles")]
    [SerializeField] private Vector3 offset = new Vector3(0.5f, 1, 0);
    [SerializeField] private Sprite invalidIngredient;

    [Header("Pref references")]
    [SerializeField] private Image icon;
    [SerializeField] private Image background;
    [SerializeField] private Image border;


    private void LateUpdate()
    {
        this.transform.position = Input.mousePosition + offset;
    }

    public void SetView(IngredientData data)
    {
        this.gameObject.SetActive(true);
        if (data != null)
        {
            icon.sprite = data.icon;
            SetColors(data.primaryColor, data.secondaryColor);
        }
        else
        {
            icon.sprite = invalidIngredient;
        }
    }

    public void HidenInformation()
    {
        this.gameObject.SetActive(false);
    }

    private void SetColors(Color c1, Color c2)
    {
        background.color = c1;
        border.color = c2;
        icon.color = c2;
    }
}
