using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingUI : MonoBehaviour
{
    public SpeechBubbleUI speechBubble;
    List<SpeechBubbleUI> bubbles = new List<SpeechBubbleUI>();

    List<SpeechBubbleUI> displayedBubbles = new List<SpeechBubbleUI>();

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
        //Hide();
        Initalize();
    }


    int index;
    Vector2 startPos = new Vector2(0,0);

    public void Initalize()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject o = Instantiate(speechBubble.gameObject, this.transform) as GameObject;
            o.GetComponent<RectTransform>().position = startPos;
            bubbles.Add(o.GetComponent<SpeechBubbleUI>());
        }

        
        
        // ShowBubble(CharacterType.PLAYER, "3");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ShowBubble(CharacterType.AI, "1");
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            ShowBubble(CharacterType.PLAYER, "2");
        }
    }



    public void ShowBubble(CharacterType type, string text)
    {
        SpeechBubbleUI bubble = bubbles[index];
        bubble.DisplayText(type, text);
        
        index++;
        if(index > 20)
        {
            index = 0;
        }

        //move rest up
        foreach(SpeechBubbleUI displayed in displayedBubbles)
        {
            displayed.MoveUp();
        }
        bubble.Show();
        displayedBubbles.Add(bubble);

    }


}
