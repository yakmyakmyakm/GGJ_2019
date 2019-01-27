using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudMusicDistraction : Distraction
{
    void Awake()
    {
        name = "loudmusic";
        Duration = 1.0f + Random.Range(0.0f, 2.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();

        TalkingManager.instance.AddSpeechData(
            CharacterType.AI,
            "Slayer! Perfect choice. Let me actually cue 'Dead Skin Mask'.",
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
            "It really does set the mood. But... Undisputed Attitude?",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        base.EndDistraction();

        Debug.Log("complete loudmusic distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
