using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public static Timer RunTimer(float time, Action onComplete)
    {
        GameObject o = new GameObject("Timer");
        Timer timer = o.AddComponent<Timer>();
        timer.Initalize(time, onComplete);
        return timer;
    }

    public void Initalize(float time, Action onComplete)
    {
        StartCoroutine(Run(time, onComplete));
    }

    public void StopTimer()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Run(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        if(onComplete != null) onComplete();
        Destroy(this.gameObject);
    }
}
