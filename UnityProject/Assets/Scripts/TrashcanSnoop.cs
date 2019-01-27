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
        TrashCanOptionsData item = data[Random.next(0, data.Count)];

        TalkingManager.instance.AddSpeechData(
        CharacterType.EVIDENCE,
        item.description,
        GameManager.DEFAULT_DIALOG_DURATION,
        item.s
        );

        GameManager.learned.Add(item.learned);
        GameManager.score += item.score;

        base.EndSnoop();
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
