using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFireDistraction : Distraction
{
    void Awake()
    {
        name = "startfire";
        Duration = 4.0f + Random.Range(0.0f, 4.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "I bet that jerk Trevor left those logs there. He's always doing that.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "What the heck! I don't even have matches in my house. Not anymore.",
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
            "Why would you even do that? Those logs were decorative.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete log distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
