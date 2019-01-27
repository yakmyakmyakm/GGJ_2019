using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceManager : MonoBehaviour
{
    public Text text;
    public Image image;

    private void Awake()
    {
        if(!gameObject.activeInHierarchy) HideEvidence(); 
    }

    public void SetText(string s)
    {
        text.text = s;
    }

    public void SetSprite(Sprite s)
    {
        image.sprite = s;
    }

    Timer timer;

    public void ShowEvidence(string s, float time)
    {
        timer = Timer.RunTimer(time, TimerComplete);
        text.text = s;
        this.gameObject.SetActive(true);
    }

    void TimerComplete()
    {
        HideEvidence();
    }

    public void HideEvidence()
    {
        if (timer != null) timer.StopTimer();
        this.gameObject.SetActive(false);
    }
}
