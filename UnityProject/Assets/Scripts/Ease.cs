using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Ease
{
    public enum Type
    {
        Linear,
        Hermite,
        Sinerp,
        Coserp,
        Spring,
        Vec2,
    }

    private delegate float EaseHandler(float start, float end, float t);
    private static Dictionary<Type, EaseHandler> _types = new Dictionary<Type, EaseHandler>()
    {
        {Type.Linear, Mathf.Lerp},
        {Type.Hermite, Hermite},
        {Type.Sinerp, Sinerp},
        {Type.Coserp, Coserp},
        {Type.Spring, Spring}
        
    };

    public static void Go(MonoBehaviour o, float start, float end, float t, UnityAction<float> update, UnityAction complete, Type type)
    {
        //o.StopAllCoroutines();
        o.StartCoroutine(GoCoroutine(start, end, t, update, complete, type));
    }

    public static void GoDelay(MonoBehaviour o, float start, float end, float t, float delay, UnityAction<float> update, UnityAction complete, Type type)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoDelay(start, end, t, delay, update, complete, type));
    }

    public static void GoNoCallback(MonoBehaviour o, float start, float end, float time, UnityAction<float> update, Type type)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoNoCallback(start, end, time, update, type));
    }

    public static void GoVector2(MonoBehaviour o, Vector2 start, Vector2 end, float time, UnityAction<Vector2> update, UnityAction complete)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoVec2(start, end, time, update, complete));
    }

    public static void GoVector3(MonoBehaviour o, Vector3 start, Vector3 end, float time, UnityAction<Vector3> update, UnityAction complete)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoVec3(start, end, time, update, complete));
    }

    public static void EaseOutVector3(MonoBehaviour o, Vector3 start, Vector3 end, float time, UnityAction<Vector3> update, UnityAction complete)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoEaseOutVec3(start, end, time, update, complete));
    }

    public static void GoNoTimeScale(MonoBehaviour o, float start, float end, float time, UnityAction<float> update, UnityAction complete, Type type)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(GoTimeScaleZero(start, end, time, update, complete, type));
    }

    public static void WaitFor(MonoBehaviour o, float time, UnityAction complete)
    {
        o.StopAllCoroutines();
        o.StartCoroutine(Wait(time, complete));
    }

    private static IEnumerator Wait(float time, UnityAction complete)
    {
        yield return new WaitForSeconds(time);
        if (complete != null)
            complete();
    }

    private static IEnumerator GoCoroutine(float start, float end, float t, UnityAction<float> update, UnityAction complete, Type type)
    { 
        var i = 0f;
        while (i <= 1f)
        {
            i += Time.unscaledDeltaTime / t;
            if(update != null) update(_types[type](start, end, i));
            yield return null;
        }

        if (complete != null)
            complete();
    }

    private static IEnumerator GoNoCallback(float start, float end, float time, UnityAction<float> update, Type type)
    {
        var i = 0f;
        while (i <= 1f)
        {
            i += Time.deltaTime / time;
            update(_types[type](start, end, i));
            yield return null;
        }
    }

    private static IEnumerator GoVec2(Vector2 start, Vector2 end, float time, UnityAction<Vector2> update, UnityAction complete)
    {
        var i = 0f;
        while (i < 1f)
        {
            i += Time.deltaTime / time;
            update(Vector2.Lerp(start, end, i));
            yield return null;
        }

        if (complete != null)
        {
            complete();
        }
    }

    private static IEnumerator GoVec3(Vector3 start, Vector3 end, float time, UnityAction<Vector3> update, UnityAction complete)
    {
        var i = 0f;
        while (i < 1f)
        {
            i += Time.deltaTime / time;
            //update(Vector3.Lerp(start, end, i));
            update(Vector3.Lerp(start, end, i));
            yield return null;
        }

        if (complete != null)
        {
            complete();
        }
    }

    private static IEnumerator GoEaseOutVec3(Vector3 start, Vector3 end, float time, UnityAction<Vector3> update, UnityAction complete)
    {
        var i = 0f;
        while (i < 1f)
        {
            i += Time.deltaTime / time;
            //update(Vector3.Lerp(start, end, i));
            update(SinerpVector3(start, end, time));
            yield return null;
        }

        if (complete != null)
        {
            complete();
        }
    }

    private static IEnumerator GoDelay(float start, float end, float t, float delay, UnityAction<float> update, UnityAction complete, Type type)
    {
        yield return new WaitForSeconds(delay);
        var i = 0f;
        while (i <= 1f)
        {
            i += Time.deltaTime / t;
            update(_types[type](start, end, i));
            yield return null;
        }

        if (complete != null)
            complete();
    }

    private static IEnumerator GoTimeScaleZero(float start, float end, float time, UnityAction<float> update, UnityAction complete, Type type)
    {
        float delta = Time.unscaledDeltaTime;

        var i = 0f;

        while (i <= 1f)
        {
            i += delta / time;
            
            update(_types[type](start, end, i));
            yield return null;
        }

        if (complete != null)
            complete();
    }

    //ease in and out
    private static float Hermite(float start, float end, float t)
    {
        return Mathf.Lerp(start, end, t * t * (3f - 2f * t));
    }

    //ease out
    private static float Sinerp(float start, float end, float t)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(t * Mathf.PI * .5f));
    }

    //ease in
    private static float Coserp(float start, float end, float t)
    {
        return Mathf.Lerp(start, end, 1f - Mathf.Cos(t * Mathf.PI * .5f));
    }

    private static float Spring(float start, float end, float t)
    {
        t = Mathf.Clamp01(t);
        t = (Mathf.Sin(t * Mathf.PI * (.2f + 2.5f * t * t * t)) * Mathf.Pow(1f - t, 2.2f) + t) * (1f + (1.2f * (1f - t)));
        return start + (end - start) * t;
    }

    private static Vector3 SinerpVector3(Vector3 start, Vector3 end, float t)
    {
        return Vector3.Lerp(start, end, Mathf.Sin(t * Mathf.PI * .5f));
    }
}