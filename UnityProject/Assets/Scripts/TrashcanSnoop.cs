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

    public void StartSnoop() {
        base.StartSnoop();
    }

    public override void EndSnoop() {
        base.EndSnoop();
        TalkingManager.instance.AddSpeechData(
        CharacterType.EVIDENCE,
        "Hmm, an empty bottle of chloroform. They must be super into organic chemistry!",
        GameManager.DEFAULT_DIALOG_DURATION
        );

        GameManager.learned.Add("Your love of organic chemistry!");
        GameManager.score += 20;
    }


    public int OnAwardSoulPoints()
    {
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            return 20;
            // found a banana peel
        }
        else
        {
            return 50;
            // used condom or something
        }
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
