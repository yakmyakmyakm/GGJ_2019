using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class TrashCanOptionsData
{
    public Sprite s;
    public string name;
    public string description;
    public int score;
    public string learned;
}

public class TrashcanSnoop : Snoop
{
    public List<TrashCanOptionsData> data = new List<TrashCanOptionsData>();

    public override void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        int i = Mathf.FloorToInt(Random.Range(0, data.Count));
        TrashCanOptionsData item = data[i];

        TalkingManager.instance.AddSpeechData(
        CharacterType.EVIDENCE,
        item.description,
        GameManager.DEFAULT_DIALOG_DURATION,
        item.s
        );

        GameManager.learned.Add(item.learned);
        GameManager.score += item.score;

        if (MyFriend.Instance.IsWatchful() || gameComplete)
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.AI,
                "AAAA! I can't believe you were digging through my trash can!",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "But I feel like we've gotten so close today!",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "I've learned so much about you...",
                GameManager.DEFAULT_DIALOG_DURATION
            );
            GameManager.instance.ComposeLitany();

            bool isGood = GameManager.instance.RollFinalOutcome();
            if(gameComplete) isGood = true;

            if (isGood)
            {
                string reason = "";
                string outcome = "";

                if (item.name == "tooth" || item.name == "apple" ||
                item.name == "syringes" || item.name == "toothbrush")
                {
                    reason = "I've been too ashamed to tell anyone about my scurvy.";
                    outcome = "It's such a relief that you just... know. Weird, but true.";
                }

                if (item.name == "wpwrapper" || item.name == "banana" ||
                item.name == "todo")
                {
                    reason = "That jerk, Trevor. He, and his stupid logging, broke my heart.";
                    outcome = "It's too hard to talk about, but now you know why I'm hurt.";
                }

                else
                {
                    reason = "Organic chemistry is the best! Do you like carbon?";
                    outcome = "I have this A-MAZING nucleophilic reagent to show you!";
                }

                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    reason,
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    outcome,
                    GameManager.DEFAULT_DIALOG_DURATION
                );

                //GameManager.instance.EndGame(true);

            }
            else
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "You know what you call someone who knows that much?",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "A witness. You sure have a lot of teeth, for a witness...",
                    GameManager.DEFAULT_DIALOG_DURATION
                );
                TalkingManager.instance.AddSpeechData(
                    CharacterType.AI,
                    "In case this is not clear, I habitually murder people for their teeth.",
                    GameManager.DEFAULT_DIALOG_DURATION
                );

                //GameManager.instance.EndGame(false);

                TalkingManager.instance.AddSpeechData(CharacterType.ENDGAME, isGood.ToString(), 0);
            }
        }
        base.EndSnoop();
    }

    void Awake()
    {
        name = "trashcan";
        Duration = 2.0f + Random.Range(0.0f, 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
