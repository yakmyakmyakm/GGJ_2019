using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Distraction : Interactable
{
    public Vector3 Destination;
    public string name;

    public List<Sprite> animSprite;

    public Image image;

    public Sprite canDistract;
    public Sprite isDistracting;

    public int distractedCount = 0;

    public bool isActiveForPlayer = true;

    public override Interactable.Type GetInteractableType()
    {
        return Type.Distraction;
    }

    public virtual void StartPlayerDistraction()
    {
        if (isActiveForPlayer)
        {
            isActiveForPlayer = false;
            this.OnStart();
            MyFriend.Instance.EnqueueDistraction(this);

            // if(animSprite.Count > 0)
            // {
            //     playImageSequence.StartAnimationOnce(animSprite);
            // }else
            // {
            //     playImageSequence.SetImage(isDistracting);
            // }
            image.overrideSprite = isDistracting;
        }

    }

    public virtual void EndDistraction()
    {
        isActiveForPlayer = true;
        distractedCount++;
        image.overrideSprite = canDistract;
    }

    //call this once AI has arrived
    public virtual void StartAIDistraction()
    {
        //playImageSequence.StartAnimation(active);
        //progressBar.onCompleteDecrease = EndAIDistraction;
    }

    public virtual void EndAIDistraction()
    {
        //playImageSequence.StartAnimation(inactive);
    }

    void Start()
    {
        Destination = this.transform.position;
        if (playImageSequence == null)
            playImageSequence = this.transform.GetComponentInChildren<PlayImageSequence>();

        image.sprite = canDistract;

    }
}
