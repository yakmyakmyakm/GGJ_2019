using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    PLAYER,
    AI,
    EVIDENCE,
    ENDGAME,
}

public class TalkingManager : MonoBehaviour
{
    public Speakable player;
    public Speakable ai;

    public static TalkingManager instance;

    public InputManager inputManager;
    public EvidenceManager evidenceManager;
    public System.Action onConversationComplete;

    [System.Serializable]
    public class SpeechData
    {
        public SpeechData(CharacterType type, string text, float time, Sprite s)
        {
            this.characterType = type;
            this.text = text;
            this.time = time;
            this.s = s;
        }

        public SpeechData(CharacterType type, string text, float time)
        {
            this.characterType = type;
            this.text = text;
            this.time = time;
        }

        public SpeechData() { }

        public Sprite s;
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
        inputManager.onConversationClick = ConversationMouseClick;
    }

    public int conversationIndex;
    IEnumerator conversation;
    IEnumerator Conversation()
    {
        SpeechData data = speeches[conversationIndex];
        Speak(data.characterType, data.text, data.time);
        yield return new WaitForSecondsRealtime(data.time);
        NextConversation();
    }

    void NextConversation()
    {
        conversationIndex++;
        if (conversationIndex >= speeches.Count)
        {
            inputManager.SetState(InputManager.State.MOVEMENT);
            conversation = null;
            TalkingUI.instance.Hide();
        }
        else
        {
            conversation = Conversation();
            StartCoroutine(conversation);
        }
    }

    void Speak(CharacterType character, string textToSay, float time, Sprite evidenceSprite = null)
    {
        evidenceManager.HideEvidence();

        switch (character)
        {
            case CharacterType.PLAYER:
            case CharacterType.AI:
                TalkingUI.instance.ShowBubble(character, textToSay);
                inputManager.SetState(InputManager.State.CONVERSATION);
                break;

            case CharacterType.EVIDENCE:
                evidenceManager.ShowEvidence(textToSay, time);
                inputManager.SetState(InputManager.State.EVIDENCE);
                break;

            case CharacterType.ENDGAME:
                Debug.Log("END GAME NOW!");
                GameManager.instance.EndGame(bool.Parse(textToSay));
                break;
        }
    }

    public void AddSpeechData(CharacterType character, string textToSay, float time, Sprite evidenceSprite = null)
    {
        //todo -- first of convo == delay
        inputManager.SetState(InputManager.State.CONVERSATION);
        speeches.Add(new SpeechData(character, textToSay, time));

        if (conversation == null)
        {
            conversation = Conversation();
            StartCoroutine(conversation);
        }
    }

    void ConversationMouseClick()
    {
        //next speech skip time
        if (conversation != null) StopCoroutine(conversation);
        NextConversation();
    }

    void EvidenceMouseClick()
    {
        if (conversationIndex >= speeches.Count-1)
        {
            evidenceManager.HideEvidence();
            inputManager.SetState(InputManager.State.MOVEMENT);
        }else
        {
             if (conversation != null) StopCoroutine(conversation);
        NextConversation();
        }
        // if (conversation == null)
        // {
        //     
        // }

       
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
