using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BedSnoop : Snoop
{
    public void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop() {
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Under the pillow, there are hundreds, perhaps even thousands, of teeth.",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "How long has your friend been living with devastating, dread scurvy?",
            GameManager.DEFAULT_DIALOG_DURATION
        );
        TalkingManager.instance.AddSpeechData(
            CharacterType.EVIDENCE,
            "Are they a shark? This seems extreme, even by shark standards.",
            GameManager.DEFAULT_DIALOG_DURATION
        );


        GameManager.learned.Add("How you've lost more teeth than I've even had in my life!");
        //GameManager.score += item.score;
        if (MyFriend.Instance.IsWatchful()) {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "So, you've found my little collection. I guess you have a lot of questions.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "I thought we were close! But you never told me about your life at sea.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            }
          
        GameManager.instance.ComposeLitany();

        bool isGood = GameManager.instance.RollFinalOutcome();

        if(isGood) {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "How did you know? Was it the shark-like quantity of teeth I've lost?",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "And the scurvy!",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "Yarr. It be a long, sad tale. Never had I had a mate to tell.",
                GameManager.DEFAULT_DIALOG_DURATION
            );

            GameManager.instance.EndGame(true);
        }
        else {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "In truth, I fear and hate the sea, ever since I had scurvy as a child.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "But I do like teeth. And you have teeth... for another minute or two.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            GameManager.instance.EndGame(false);
        }

        base.EndSnoop();
    }

    void Awake()
    {
        name = "bed";
        Duration = 2.0f + Random.Range(0.0f, 1.0f);
    }
}
