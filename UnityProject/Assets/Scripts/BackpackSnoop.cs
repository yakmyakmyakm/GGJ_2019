using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BackpackSnoop : Snoop
{
    public override void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Inside, you find rubber gloves, a bottle of chloroform, some rags, and a pair of pliers.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "It makes sense that, if someone spilled chloroform in their lab, they'd want to clean it up!",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Not sure about the pliers, though.",
            GameManager.DEFAULT_DIALOG_DURATION
        );


        GameManager.learned.Add("How clumsy you are in the lab, spilling chloroform everywhere!");

        GameManager.score += 20;

        if (MyFriend.Instance.IsWatchful() || gameComplete)
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "Get out of my stuff! I can't stand it when people are in my stuff.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "But I only did it because I care about you! And I've learned so much.",
                GameManager.DEFAULT_DIALOG_DURATION
            );

            GameManager.instance.ComposeLitany();

            bool isGood = GameManager.instance.RollFinalOutcome();

            if (isGood)
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Organic chemistry gets me so excited, my hands do shake when I'm pouring reagents.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "What about those pliers, though?",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Seriously, that's none of your business. Accept it, and me.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );

                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
            else
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Silly! Let me put on those gloves... perfect. Did you know chloroform is an anaesthetic?",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "Not a very safe one, though. It fell out of favor in the 1930s.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "That's fine, for our purposes here. You sure do have a lot of beautiful teeth!",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
        }
        base.EndSnoop();
    }

    void Awake()
    {
        name = "backpack";
        Duration = 2.0f + Random.Range(0.0f, 1.0f);
    }
}
