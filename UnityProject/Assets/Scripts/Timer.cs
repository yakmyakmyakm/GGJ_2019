using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public static Timer RunTimer(float time, Action onComplete, bool noTimeScale = false)
    {
        GameObject o = new GameObject("Timer");
        Timer timer = o.AddComponent<Timer>();
        timer.Initalize(time, onComplete, noTimeScale);
        return timer;
    }

    public void Initalize(float time, Action onComplete, bool noTimeScale)
    {
        if (!noTimeScale)
        {
            StartCoroutine(Run(time, onComplete));
        }
        else
        {
            StartCoroutine(RunNoTimescale(time, onComplete));
        }
    }

    public void StopTimer()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Run(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        if (onComplete != null) onComplete();
        Destroy(this.gameObject);
    }

    IEnumerator RunNoTimescale(float time, Action onComplete)
    {
        yield return new WaitForSecondsRealtime(time);
        if (onComplete != null) onComplete();
        Destroy(this.gameObject);
    }
}
