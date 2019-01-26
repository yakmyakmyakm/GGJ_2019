using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;

    public Action onCompleteIncrease;

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Decrease(float time)
    {
        Ease.Go(this, 1, 0, time, SetFill, DoneDecreasing, Ease.Type.Linear);
    }

    void DoneDecreasing()
    {

    }

    public void Increase(float time)
    {
        Show();
        Ease.Go(this, 0, 1, time, SetFill, DoneIncreasing, Ease.Type.Linear);
    }

    void DoneIncreasing()
    {
        if (onCompleteIncrease != null) onCompleteIncrease();
        Hide();
    }

    public void SetFill(float value)
    {
        fillImage.fillAmount = value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Increase(5);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Decrease(5);
        }
    }
}
