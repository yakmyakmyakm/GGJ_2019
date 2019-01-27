using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    CanvasGroup cg;
    public Image fillImage;
    public Action onCompleteIncrease;
    public Action onCompleteDecrease;

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
        Show();
        Ease.Go(this, 1, 0, time, SetFill, DoneDecreasing, Ease.Type.Linear);
    }

    public void Decrease(FloatVariable time)
    {
        Show();
        Ease.Go(this, 1, 0, time.Value, SetFill, DoneDecreasing, Ease.Type.Linear);
    }

    void DoneDecreasing()
    {
        if (onCompleteDecrease != null) onCompleteDecrease();
        Hide();
    }

    public void Increase(float time)
    {
        Show();
        Ease.Go(this, 0, 1, time, SetFill, DoneIncreasing, Ease.Type.Linear);
        if(cg == null) cg = this.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    void DoneIncreasing()
    {
        if (onCompleteIncrease != null) onCompleteIncrease();
        Hide();
    }

    void SetFill(float value)
    {
        fillImage.fillAmount = value;
    }

    public void StopProgress()
    {
        this.StopAllCoroutines();
        Hide();
    }
}
