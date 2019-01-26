using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimationData", menuName = "Create/AnimationData", order = 1)]
public class AnimationSprites : ScriptableObject 
{
    public string objectName = "New MyScriptableObject";
    
    public List<Sprite> forward;
    public List<Sprite> back;
    public List<Sprite> sideways;

    public Sprite idle;
}


