using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float DEFAULT_DIALOG_DURATION = 3.0f;

    public static int score = 0;

    public static List<string> learned = new List<string>();

    public static GameManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad();
    }

    public void EndGame()
    {

    }

    public void StartGame()
    {

    }

    public void ResetGame()
    {

    }

}
