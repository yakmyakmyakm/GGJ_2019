using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    PLAYER,
    AI,
    CAT
}

public class TalkingManager : MonoBehaviour
{
    public Speakable player;
    public Speakable ai;

    public static TalkingManager instance;

    void Awake() 
    {
        TalkingManager.instance = this;
        player.HideBubble();
        ai.HideBubble();
    }

    public void Speak(CharacterType character, string textToSay, float time)
    {
        switch(character)
        {
            case CharacterType.PLAYER: player.Speak(time, textToSay); break;
            case CharacterType.AI: ai.Speak(time, textToSay); break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Speak(CharacterType.PLAYER, "HELLO!", 3);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Speak(CharacterType.PLAYER, "HaaaaaHa!", 3);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Speak(CharacterType.AI, "SUP!", 3);
        }
    }
}
