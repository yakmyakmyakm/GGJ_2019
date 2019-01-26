using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayImageSequence : MonoBehaviour
{
    public string id;
    public float speed = 0.2f;

    public List<Sprite> sprites = new List<Sprite>();

    public Image image;
    int index;

    IEnumerator anim;

    public void SetImage(Sprite s)
    {
        image.sprite = s;
    }

    public void StartAnimation(List<Sprite> sprites, bool isLeft = false)
    {
        image.transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (isLeft) image.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

        index = 0;
        this.sprites = sprites;
        image.sprite = sprites[index];
        anim = RunAnimation();
        StartCoroutine(anim);
    }

    public void StartAnimationOnce(List<Sprite> sprites)
    {
        index = 0;
        this.sprites = sprites;
        image.sprite = sprites[index];
        StartCoroutine(RunAnimationOnce());
    }

    public void StopAnimation()
    {
        if (sprites.Count > 0) image.sprite = sprites[0];
        StopAllCoroutines();
    }

    IEnumerator RunAnimation()
    {
        yield return new WaitForSeconds(speed);
        image.sprite = sprites[index];
        index++;
        if (index >= sprites.Count) index = 0;
        StartCoroutine(RunAnimation());
    }

    IEnumerator RunAnimationOnce()
    {
        yield return new WaitForSeconds(speed);
        image.sprite = sprites[index];
        index++;
        if (index < sprites.Count) 
        {
            StartCoroutine(RunAnimationOnce());
        }

        Debug.Log(index + " " + sprites.Count);
    }
}
