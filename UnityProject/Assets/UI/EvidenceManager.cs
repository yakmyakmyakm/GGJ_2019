using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public void ShowEvidence()
    {
        this.gameObject.SetActive(true);
    }

    public void HideEvidence()
    {
        this.gameObject.SetActive(false);
    }
}
