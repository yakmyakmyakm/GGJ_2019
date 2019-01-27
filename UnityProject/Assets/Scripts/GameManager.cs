using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public const float DEFAULT_DIALOG_DURATION = 3.0f;

    public static int score = 0;

    public static int scoreThreshold = 80;
    // roll above this on 1dscore for good ending

    public static List<string> learned = new List<string>();

    public System.Action onEndGame;

    public static GameManager instance;

    [SerializeField]
    private GameEvent GameInit, GameStart, GameEnd;

    public bool RollFinalOutcome()
    {
        return Mathf.FloorToInt(Random.Range(0, score)) > scoreThreshold;
    }

    public void ComposeLitany()
    {
        if (learned.Count > 0)
        {
            foreach (string item in learned)
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    item,
                    GameManager.DEFAULT_DIALOG_DURATION
                );

            }
        }
        else
        {
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "...actually, I don't really know anything about you.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (GameInit)
                GameInit.Raise();
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    bool isGoodEnding;

    public void EndGame(bool isGood)
    {
        isGoodEnding = isGood;

        if (GameEnd)
            GameEnd.Raise();
        MyFriend.Instance.EndGame();
        if (onEndGame != null) onEndGame();
    }

    public void StartGame()
    {
        if (GameStart)
            GameStart.Raise();
        MyFriend.Instance.StartGame();

        Debug.Log("Starting GAME");
    }

    public static int snoopedCount = 0;

    public Vector3 playerStartPos;
    public Vector3 friendStartPos;

    public PlayerObject player;
    public MyFriend friend;

    GenerateItems generate;

    public void ResetGame()
    {
        if(generate == null) generate = GameObject.Find("GenerateSystem").GetComponent<GenerateItems>();
        generate.InitalizeWorld();
        player.transform.position = playerStartPos;
        friend.transform.position = friendStartPos;
        
        // if (GameStart)
        //     GameStart.Raise();
        string s = string.Empty;
        if (isGoodEnding)
        {
            s = "Your friend gave you a hug ";
        }
        else
        {
            s = "Your friend unfriended you on facebook ";
        }

        EndGameScore.instance.SetText(s + " " + score);
        score = 0;
        snoopedCount = 0;

        Debug.Log("RESETTING GAME!!!");
    }
}
