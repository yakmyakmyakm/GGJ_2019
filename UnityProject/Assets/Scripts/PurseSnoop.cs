using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PurseSnoop : Snoop
{
    public void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Inside, you find a very large absorbent pad, maybe the size of a queen bed. It's probably to soak up blood.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "The dread scurvy causes previously-healed wounds to re-open! They must be so sick",
            GameManager.DEFAULT_DIALOG_DURATION
        );

        GameManager.learned.Add("The painful re-opening of your wounds, due to scurvy!");

        GameManager.score += 20;

        if (MyFriend.Instance.IsWatchful())
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "Stop that! Why are you snooping around in my purse?",
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
                    "Yarr, it be true. Too long have I been a-sea, not a lime in sight.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "I want to be with you, even if your wounds don't completely heal due to dread scurvy.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Aye, matey. But stay out of me purse now.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );

                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
            else
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Those giant absorbent pads? Actually, really handy when you murder someone for their teeth.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "Wow! That's so interesting. You have so many hidden depths.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "I am going to need you to step onto the pad now. Say 'aaah!'",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
        }
        base.EndSnoop();
    }

    void Awake()
    {
        name = "purse";
        Duration = 2.0f + Random.Range(0.0f, 1.0f);
    }
}
