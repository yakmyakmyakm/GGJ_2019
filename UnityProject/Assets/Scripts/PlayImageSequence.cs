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

    IEnumerator animation;

   public void StartAnimation(List<Sprite> sprites)
   {
       index = 0;
       this.sprites = sprites;
       image.sprite = sprites[index];
       animation = RunAnimation();
       StartCoroutine(animation);
   }

   public void StopAnimation()
   {
       StopAllCoroutines();
   }

   IEnumerator RunAnimation()
   {
       yield return new WaitForSeconds(speed);
       image.sprite = sprites[index];
       index++;
       if(index >= sprites.Count) index = 0;
       StartCoroutine(RunAnimation());
   }

}
