using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    private GameManagerScript GM_Script;
    private Shooting_Behaviour Shoot_Script;
    private bool Damaged;
    private bool Acid;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        Shoot_Script = GameObject.Find("Capsule/Gun").GetComponent<Shooting_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Define what happens when the Damaged boolean is true

        if (Damaged == true)
        {
            // Call the "TakeDamage" function in the game manager with the value of 1
            GM_Script.SendMessage("TakeDamage", 1);
        }

        // Define what happens when the Acid boolean is true

        if (Acid == true)
        {
            // Call the "TakeDamage" function in the game manager with the value of 0.5
            GM_Script.SendMessage("TakeDamage", 0.5f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Define what happens when this gameobject collides with relevent items

        // Define what happens when this gameobject collides with Health item

        if (other.gameObject.tag == "Health")
        {
            // Add 50 to the Health variable in the game manager
            GM_Script.Health += 50;

            // Destroy this other ganme object

            Destroy(other.gameObject);
        }

        // Define what happens when this gameobject collides with Armour item

        if (other.gameObject.tag == "Armour")
        {
            // Add 50 to the Armour variable in the game manager

            GM_Script.Armour += 50;

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this gameobject collides with Ammo item

        if (other.gameObject.tag == "Ammo")
        {
            // Add 25 to the 1st variable of the Ammo list in the game manager
            Shoot_Script.Ammo[0] += 25;

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this gameobject collides with ShotgunAmmo item

        if (other.gameObject.tag == "ShotgunAmmo")
        {
            // Add 6 to the 2nd variable of the Ammo list in the game manager

            Shoot_Script.Ammo[1] += 6;

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this game object collides with the RocketAmmo item

        if (other.gameObject.tag == "RocketAmmo")
        {
            // Add 2 to the 3rd variable of the Ammo list in the game manager

            Shoot_Script.Ammo[2] += 2;

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this game object collides with Acid

        if (other.gameObject.tag == "Acid")
        {
            // Set the Acid boolean to true

            Acid = true;
        }

        // Define what happens when this game object collides with the BlueCardKey(Clone) item

        if (other.gameObject.name == "BlueCardKey(Clone)")
        {
            // Add "BlueCardKey" to the 1st variable in the Items list in the game manager

            GM_Script.Items[0] = "BlueCardKey";

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this game object collides with the RedCardKey(Clone) item

        if (other.gameObject.name == "RedCardKey(Clone)")
        {
            // Add "RedKeyCard" to the 2nd variable in the Items list in the game manager

            GM_Script.Items[1] = "RedKeyCard";

            // Destroy this other game object

            Destroy(other.gameObject);
        }

        // Define what happens when this game object collides with the YellowCardKey(Clone) item

        if (other.gameObject.name == "YellowCardKey(Clone)")
        {
            // Add "YellowKeyCard" to the 3rd variable in the Items list in the game manager

            GM_Script.Items[2] = "YellowKeyCard";

            // Destroy this other game object 

            Destroy(other.gameObject);
        }

        // Define what happens when this game object collides with the Switch item

        if (other.gameObject.name == "Switch")
        {
            // Deacitvate this other game object

            other.gameObject.SetActive(false);
        }
        
        // Define what happens when this game object collides with the Shotgun item

        if (other.gameObject.name == "Shotgun")
        {
            // Set the Shotgun boolean in the game manager to true

            GM_Script.Shotgun = true;
        }

        // Define what happens when this game object collides with the RL item

        if (other.gameObject.name == "RL")
        {
            // Set the RL boolean in the game manager to true

            GM_Script.RL = true;
        }
    }

    // Define what happens when this game object is no longer colliding with Acid
    private void OnTriggerExit(Collider other)
    {
        // If this object is not colliding with Acid

        if (other.gameObject.tag == "Acid")
        {
            // Set the Acid boolean to false

            Acid = false;
        }
    }

}
