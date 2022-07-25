using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    private Canvas dialogueCanvas;

    public void AddPostIt(GameObject postIt)
    {
        if (dialogueCanvas == null)
        {
            var canvaces = FindObjectsOfType<Canvas>();
            dialogueCanvas = canvaces.First(c => c.transform.parent.name.Equals("Dialogue Manager"));
        }

        Instantiate(postIt, dialogueCanvas.transform);
    }

    public void ClearPostIts()
    {
        var postIts = FindObjectsOfType<PostIt>();

        for (int i = 0; i < postIts.Length; i++)
        {
            Destroy(postIts[i].gameObject);
        }
    }
}
