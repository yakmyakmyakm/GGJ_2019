using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DresserSnoop : Snoop
{
    public override void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "There's a big, weird bottle with a bleached label, but you can just barely read parts of it.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "They've got a lot of some kind of acid. Isn't Vitamin C ascorbic acid?",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "They must be struggling with the dread disease scurvy, which causes tooth loss.",
            GameManager.DEFAULT_DIALOG_DURATION
        );


        GameManager.learned.Add("The huge amount of ascorbic acid you're going through for your scurvy!");

        GameManager.score += 20;

        if (MyFriend.Instance.IsWatchful() || gameComplete)
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "What the hell? That's none of your business!",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "But I only did it because I care about you! And I've learned so much.",
                GameManager.DEFAULT_DIALOG_DURATION
            );

            GameManager.instance.ComposeLitany();

            bool isGood = GameManager.instance.RollFinalOutcome();
            if(gameComplete) isGood = true;

            if (isGood)
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "It's just... embarassing. What kind of landlubbber even gets scurvy?",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    "I'm here for you. I love you for so much more than your teeth.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Please stop it with the snooping, though. That's super creepy.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );

                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
            else
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "Oh, no. Not ascorbic acid. Concentrated hydrofluoric acid!",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "I use it to dispose of my victims, except for their teeth. Teeth are so pretty.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "And you have so many! Temporarily.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
        }
        base.EndSnoop();
    }

    void Awake()
    {
        name = "dresser";
        Duration = 2.0f + Random.Range(0.0f, 1.0f);
    }
}
