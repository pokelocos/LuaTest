using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreditShower : MonoBehaviour
{
    [SerializeField] private RectTransform pivot;
    [SerializeField] private List<CreditData> creditsDatas;

    [SerializeField] private Font titleFont;
    [SerializeField] private Font contentFont;

    private void Awake()
    {
        LoadCredits("en"); // cabiar a que se seleccione con el idioma correspondiente
    }

    private void LoadCredits(string lenguage)
    {
        var credits = creditsDatas.First(c => c.lenguage.Equals(lenguage));

        var header = credits.header;
        InstantiateHeader(header);

        var infoGroups = credits.infoGroup;
        foreach (var group in infoGroups)
        {
            InstantiateGroup(group);
        }

        var footer = credits.footer;
        IntantiateFooter(footer);
    }

    private void IntantiateFooter(CreditData.Footer footer)
    {
        var inst = new GameObject("Footer");
        var trans = inst.AddComponent<RectTransform>();
        inst.transform.SetParent(pivot);

        var obj = InstantiateText("Text", trans, footer.text, contentFont);
    }

    private void InstantiateHeader(CreditData.Header header)
    {
        var inst = new GameObject("Header");
        var trans = inst.AddComponent<RectTransform>();
        var layout = inst.AddComponent<VerticalLayoutGroup>();
        inst.transform.SetParent(pivot);

        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childForceExpandWidth = false;

        var title = InstantiateText("Tilte", trans, header.title, titleFont);
        var image = InstantiateImage("Image", trans, header.image);
        //image.rectTransform.sizeDelta = image.rectTransform.sizeDelta/2;
        image.rectTransform.localScale /= 2f;
        var name = InstantiateText("Name", trans, header.name, contentFont);
    }

    private void InstantiateGroup(CreditData.InfoGroup group)
    {
        var title = InstantiateText("Title-" + group.title, pivot, group.title, titleFont);

        var obj = InstantiateLayout<HorizontalLayoutGroup>("Group-" + group.title, pivot);

        List<RectTransform> columns = new List<RectTransform>();
        for (int i = 0; i < group.colummns; i++)
        {
            var parent = obj.GetComponent<RectTransform>();
            var column = InstantiateLayout<VerticalLayoutGroup>("Column-" + (i + 1), parent);
            columns.Add(column.GetComponent<RectTransform>());
        }

        for (int i = 0; i < group.names.Count; i++)
        {
            var n = i % group.colummns;
            var col = columns[n];
            InstantiateText("name-" + n, col, group.names[i], contentFont);
        }
    }

    private T InstantiateLayout<T>(string name, RectTransform parent) where T : HorizontalOrVerticalLayoutGroup
    {
        var obj = new GameObject(name);
        obj.AddComponent<RectTransform>();
        var layout = obj.AddComponent<T>();
        obj.transform.SetParent(parent);

        return layout;
    }

    private Text InstantiateText(string name, RectTransform parent, string content, Font font)
    {
        var obj = new GameObject(name);
        obj.AddComponent<RectTransform>();
        var text = obj.AddComponent<Text>();
        obj.transform.SetParent(parent);

        text.text = content;
        text.font = font;
        text.alignment = TextAnchor.MiddleCenter;

        return text;
    }

    private Image InstantiateImage(string name, RectTransform parent, Sprite content)
    {
        var obj = new GameObject(name);
        obj.AddComponent<RectTransform>();
        var image = obj.AddComponent<Image>();
        obj.transform.SetParent(parent);

        image.sprite = content;

        return image;
    }
}
