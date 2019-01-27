using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleUI : MonoBehaviour
{
    public Image image;
    public Text text;

    public void MoveUp()
    {

    }

    public void DisplayText(CharacterType type, string text)
    {
        this.text.text = text;
        
        if(type == CharacterType.AI)
        this.transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));

    }
}
