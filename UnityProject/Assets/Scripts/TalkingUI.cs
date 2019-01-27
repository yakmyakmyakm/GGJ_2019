using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TalkingUI : MonoBehaviour
{
    public SpeechBubbleUI speechBubble;
    List<SpeechBubbleUI> bubbles = new List<SpeechBubbleUI>();

    public List<SpeechBubbleUI> displayedBubbles = new List<SpeechBubbleUI>();

    int index;
    Vector2 startPos = new Vector2(0, 0);

    int totalItems = 20;

    public static TalkingUI instance;

    public GameObject parent;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);

        Hide();
    }

    bool isShowing;

    public void Show()
    {
        Initalize();
        parent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        parent.gameObject.SetActive(false);
        isShowing = false;
    }

    public void Initalize()
    {
        foreach (SpeechBubbleUI b in bubbles)
        {
            DestroyImmediate(b.gameObject);
        }

        bubbles.Clear();
        displayedBubbles.Clear();
        index = 0;

        for (int i = 0; i < totalItems; i++)
        {
            GameObject o = Instantiate(speechBubble.gameObject, parent.transform) as GameObject;
            o.GetComponent<RectTransform>().localPosition = startPos;
            bubbles.Add(o.GetComponent<SpeechBubbleUI>());
        }
    }

    public void ShowBubble(CharacterType type, string text)
    {
        if(!isShowing)
        {
            isShowing = true;
            Show();
        }

        SpeechBubbleUI bubble = bubbles[index];
        bubble.DisplayText(type, text);

        index++;
        if (index >= totalItems)
        {
            index = 0;
        }

        int displayCount = 0;

        //move rest up
        foreach (SpeechBubbleUI displayed in displayedBubbles)
        {
            displayed.MoveUp();
            displayCount++;
        }

        bubble.Show();
        displayedBubbles.Add(bubble);

        if (displayCount > 4)
        {
            //begin fade out of top
            displayedBubbles[0].GetComponent<RectTransform>().localPosition = Vector3.zero;
            //Debug.Log(displayedBubbles[0].GetComponent<RectTransform>().localPosition);
            displayedBubbles[0].SetAlpha(0);
            displayedBubbles.RemoveAt(0);
        }
    }
}
