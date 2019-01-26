using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInteractable : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void CreateAInteractable(Vector2 position)
    {
        GameObject o = Instantiate(obj) as GameObject;
        o.AddComponent<KittenDistraction>();
        o.transform.position = new Vector3(position.x, 0, position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
