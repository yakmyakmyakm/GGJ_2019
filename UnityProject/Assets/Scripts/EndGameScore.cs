using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScore : MonoBehaviour
{
    [SerializeField]
    Text t;
   public static EndGameScore instance;

   void Awake() 
   {
       instance = this;
   }

   public void SetText(string s)
   {
       t.text = s;
   }
}
