using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingUI : MonoBehaviour
{
    public SpeechBubbleUI speechBubble;
    List<SpeechBubbleUI> bubbles = new List<SpeechBubbleUI>();

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        Hide();
        Initalize();
    }

    public void Initalize()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject o = Instantiate(speechBubble.gameObject) as GameObject;
            bubbles.Add(o.GetComponent<SpeechBubbleUI>());
        }
    }
}
