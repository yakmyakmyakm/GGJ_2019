using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseplantDistraction : Distraction
{
    void Awake()
    {
        name = "houseplant";
        Duration = 6.0f + Random.Range(0.0f, 2.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Did you just push that plant over?",
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
            "This carpet is very stain-resistant. I spill all kinds of things on it.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete houseplant distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
