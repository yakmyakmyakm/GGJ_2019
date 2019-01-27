using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpeechBubbleUI : MonoBehaviour
{
    public Image image;
    public Text text;

    CanvasGroup cg;

    int offsetY = 175;

    private void Start()
    {
        cg = this.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    public void Show()
    {
        StartCoroutine(ShowDelay());
    }

    IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(0.15f);
        Ease.Go(this, 0, 1, 0.15f, SetAlpha, Done, Ease.Type.Linear);
    }

    public void SetAlpha(float value)
    {
        cg.alpha = value;
    }

    public void MoveUp()
    {
        Ease.Go(this, this.transform.localPosition.y, this.transform.localPosition.y + offsetY, 0.15f, MoveY, Done, Ease.Type.Linear);
    }

    void MoveY(float value)
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, value, this.transform.localPosition.z);
    }

    void Done()
    {
    }

    public void DisplayText(CharacterType type, string text)
    {
        this.text.text = text;
        if (type == CharacterType.AI)
            image.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

    }
}
