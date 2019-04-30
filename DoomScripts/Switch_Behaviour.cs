using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Behaviour : MonoBehaviour
{
    public GameObject Switch;
    public Sprite On;
    public SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Define what happens when the switch is no longer active in the hierarchy

        if (Switch.activeInHierarchy == false)
        {
            // Change the sprite to "On"

            SR.sprite = On;
        }
    }
}
