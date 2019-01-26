using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speakable : MonoBehaviour
{
    public TextMeshProUGUI text;
    Timer timer;
    public void Speak(float time, string textToSay)
    {
        if(timer != null) timer.StopTimer();
        this.gameObject.SetActive(true);
        text.text = textToSay;
        timer = Timer.RunTimer(time, HideBubble);
    }

    public void HideBubble()
    {
        this.gameObject.SetActive(false);
    }
}
