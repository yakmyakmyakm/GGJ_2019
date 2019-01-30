using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillWaterDistraction : Distraction
{
    void Awake()
    {
        name = "spillwater";
        Duration = 5.0f + Random.Range(0.0f, 2.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

        TalkingManager.instance.AddSpeechData(
            CharacterType.PLAYER,
            "Oops! Should have turned off glass.useGravity.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "You can keep your game dev humor to yourself.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        //AI can begin moving
        //base.StartAIDistraction();
    }

    //AI calls when compelte fixing
    public override void EndDistraction()
    {

        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Luckily, I'm really quick at cleaning up puddles of fluid on my floor.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete spillwater distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
