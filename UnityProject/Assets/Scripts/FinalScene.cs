using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    public static FinalScene instance;
    Image img;
    public List<Sprite> ends = new List<Sprite>();

    void Awake()
    {
        instance = this;
        img = GetComponent<Image>();
    }

    public void ShowEnding(int num)
    {
        if (num >= ends.Count)
        {
            num = ends.Count - 1;
        }
        img.sprite = ends[num];
        this.gameObject.SetActive(true);
        Timer.RunTimer(2, Done, true);
    }

    void Done()
    {
        this.gameObject.SetActive(false);
    }

}
