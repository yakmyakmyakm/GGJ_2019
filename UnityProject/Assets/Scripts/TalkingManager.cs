﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    PLAYER,
    AI,
    EVIDENCE
}

public class TalkingManager : MonoBehaviour
{
    public Speakable player;
    public Speakable ai;

    public static TalkingManager instance;

    public InputManager inputManager;
    public EvidenceManager evidenceManager;

    [System.Serializable]
    public class SpeechData
    {
        public SpeechData(CharacterType type, string text, float time)
        {
            this.characterType = type;
            this.text = text;
            this.time = time;
        }

        public SpeechData() { }

        public string text;
        public float time;
        public CharacterType characterType;
    }

    public List<SpeechData> speeches = new List<SpeechData>();

    void Awake()
    {
        TalkingManager.instance = this;
        player.HideBubble();
        ai.HideBubble();
        inputManager.onEvidenceMouseClick = EvidenceMouseClick;
    }

    public int conversationIndex;
    IEnumerator conversation;
    IEnumerator Conversation()
    {
        SpeechData data = speeches[conversationIndex];
        Speak(data.characterType, data.text, data.time);
        yield return new WaitForSeconds(data.time);
        conversationIndex++;
        if (conversationIndex >= speeches.Count)
        {
            inputManager.SetState(InputManager.State.MOVEMENT);
            conversation = null;
        }
        else
        {
            conversation = Conversation();
            StartCoroutine(conversation);
        }

        //pass to next string on mouseclick
    }

    void Speak(CharacterType character, string textToSay, float time, Sprite evidenceSprite = null)
    {
        switch (character)
        {
            case CharacterType.PLAYER: player.Speak(time, textToSay); break;
            case CharacterType.AI: ai.Speak(time, textToSay); break;
            case CharacterType.EVIDENCE:
                evidenceManager.ShowEvidence(textToSay, time);
                inputManager.SetState(InputManager.State.EVIDENCE);
                break;
        }
    }

    public void AddSpeechData(CharacterType character, string textToSay, float time)
    {
        inputManager.SetState(InputManager.State.CONVERSATION);
        speeches.Add(new SpeechData(character, textToSay, time));

        if (conversation == null)
        {
            conversation = Conversation();
            StartCoroutine(conversation);
        }
    }

    void EvidenceMouseClick()
    {
        evidenceManager.HideEvidence();
        inputManager.SetState(InputManager.State.MOVEMENT);
    }

    public void EnterConvoMode()
    {

    }

    public void ExitConvoMode()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddSpeechData(CharacterType.PLAYER, "HELLO!", 3);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AddSpeechData(CharacterType.PLAYER, "HaaaaaHa!", 3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddSpeechData(CharacterType.AI, "SUP!", 3);
        }
    }
}
