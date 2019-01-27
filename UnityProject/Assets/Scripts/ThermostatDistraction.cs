using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermostatDistraction : Distraction
{
    void Awake()
    {
        name = "thermostat";
        Duration = 1.0f + Random.Range(0.0f, 5.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

        TalkingManager.instance.AddSpeechData(
            CharacterType.PLAYER,
            "Is it warm in here, or is it just me?",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Oh, no, I spent hours getting the heat just right.",
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
            "I'm practically cold-blooded.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete thermostat distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
