using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenDistraction : Distraction
{
    static int count = 0; //how many times this has happened

    void Awake()
    {
        name = "kitten";
        Duration = 4.0f + Random.Range(0.0f, 4.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

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

        KittenDistraction.count++;

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

        if(count > 2)
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "You know, I routinely murder people for their teeth. But even I don't kick kittens.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "That's seriously messed up. I am going to add your teeth to my collection now.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, false.ToString(), 0);

        }

        base.EndDistraction();

        Debug.Log("complete kitten distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
