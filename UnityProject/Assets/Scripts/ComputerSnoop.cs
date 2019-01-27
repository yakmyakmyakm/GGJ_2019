using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ComputerSnoop : Snoop
{
    public override void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Their browser tabs are all open to a wide variety of online chainsaw stores.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Is she buying one for that jerk, Trevor? He's always leaving logs around.",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        GameManager.learned.Add("How you prefer that jerk, Trevor, to me!");

        GameManager.score += 20;

        if (MyFriend.Instance.IsWatchful() || gameComplete)
        {
            Debug.Log("CAUGHT SNOOPING COMPUTER");

            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "What are you doing on my computer! That's so rude.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "I thought you loved me! But you're buying a chainsaw for that jerk, Trevor.",
                GameManager.DEFAULT_DIALOG_DURATION
            );

            bool isGood = GameManager.instance.RollFinalOutcome();

            if (isGood)
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Oh, silly, no! I was getting a chainsaw for you. It was going to be a surprise.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "So... can we be chainsaw friends?",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Forever and ever.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
            }
            else
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Oh, silly, no! I was getting a chainsaw for YOU.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "To dismember you, in case this is not entirely clear.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
            }

            TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
        }

        base.EndSnoop();
    }

    void Awake()
    {
        name = "computer";
        Duration = 2.0f + Random.Range(0.0f, 6.0f);
    }
}
