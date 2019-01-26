using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenDistraction : Distraction
{
    void Awake()
    {
        // this.kitten = new Distraction();
        // this.kitten.active = this.active;
        // this.kitten.inactive = this.inactive;
        // this.kitten
        // this.kitten
        //Vector3 pos = GameObject.transform.position;
        //this.kitten.location = new Vector2(pos.x, pos.z);

        name = "kitten";
        Duration =  4.0f + Random.Range(0.0f, 4.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();
        //MyFriend.Instance.EnqueueDistraction(this);

        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Seriously, kicking a kitten?",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "That is messed up! Poor kitty.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        //AI can begin moving
        //base.StartAIDistraction();
    }

    //AI calls when compelte fixing
     public override void EndDistraction()
    {

        TalkingManager.instance.AddSpeechData(
            CharacterType.PLAYER,
            "I'm sorry I kicked the cat.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Why are we even friends?",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete kitten distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
