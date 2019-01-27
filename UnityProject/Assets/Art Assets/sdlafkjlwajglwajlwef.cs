using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sdlafkjlwajglwajlwef : MonoBehaviour
{
    public Image img;
    public Sprite one;
    public Sprite two;

    void Start()
    {
        img = GetComponent<Image>();
    }

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            if (img.sprite == one)
            {
                img.sprite = two;
            }
            else
            {
                img.sprite = one;
            }
            timer = 0;
        }
    }
}
