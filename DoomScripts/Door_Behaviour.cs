using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Behaviour : MonoBehaviour
{
    public string ItemReq;
    private GameManagerScript GM_Script;
    public int ItemNo;
    public GameObject Switch;
    public bool Effectedbyswitch;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Define what to do when the Effectedbyswitch boolean is true

        if (Effectedbyswitch == true)
        {
            // Define what happens if the switch for this door is not active in the hierarchy

            if (Switch.activeInHierarchy == false)
            {
                // Destroy this game object

                Destroy(this.gameObject);
            }
        }
    }

    // Define what happens when the player collides with this game object
    private void OnTriggerEnter(Collider other)
    {
        // Define what happens when the player is colliding with this gameobject

        if (other.gameObject.tag == "Player")
        {
            // Check the relative entry in the Items list in the game manager and see if it matches the required item defined for this door
            if (GM_Script.Items[ItemNo] == ItemReq)
            {
                // Destroy this game object

                Destroy(this.gameObject);
            }
        }
    }
}
