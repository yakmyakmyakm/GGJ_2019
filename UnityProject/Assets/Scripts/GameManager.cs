using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float DEFAULT_DIALOG_DURATION = 3.0f;

    public static int score = 0;

    public static int scoreThreshold = 80; 
    // roll above this on 1dscore for good ending
    
    public static List<string> learned = new List<string>();

    public static GameManager instance;

    [SerializeField]
    private GameObject startUI, endUI;

    public bool RollFinalOutcome()
    {
        return Mathf.FloorToInt(Random.Range(0, score)) > scoreThreshold;
    }

    public void ComposeLitany()
    {
        if (learned.Count > 0) {
            foreach (string item in learned)
            {
                TalkingManager.instance.AddSpeechData(
                    CharacterType.PLAYER,
                    item,
                    GameManager.DEFAULT_DIALOG_DURATION
                );

            }
        }
        else {
            TalkingManager.instance.AddSpeechData(
                CharacterType.PLAYER,
                "...actually, I don't really know anything about you.",
                GameManager.DEFAULT_DIALOG_DURATION
            );
        }
    }

    void Awake()
    {
        instance = this;
        startUI.SetActive(true);
        endUI.SetActive(false);
    }

    public void EndGame(bool isGood)
    {
        endUI.SetActive(true);
        MyFriend.Instance.EndGame();
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        MyFriend.Instance.StartGame();
    }

    public void ResetGame()
    {
        endUI.SetActive(false);
    }
}
