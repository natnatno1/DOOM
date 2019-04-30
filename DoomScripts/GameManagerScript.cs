using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public float Health;
    public GameObject Player;
    public float Armour;
    public string[] Weapons;
    public string ActiveWeapon;
    public string[] Items;
    public GameObject GameOver;
    public bool Completed;
    public bool Shotgun;
    public bool RL;
    public float Injury;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        // Define what happens if the current scene from the build index is 1

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // Create a new list with 3 entries

            Weapons = new string[3];

            // Add 3 entries to the list for Pistol, Shotgun and Rocket Launcher

            Weapons[0] = "Pistol";
            Weapons[1] = "Shotgun";
            Weapons[2] = "Rocket Launcher";

            // Set the active weapon to 1

            ActiveWeapon = Weapons[0];

            // Create a new list with 4 entries

            Items = new string[4];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set the PlayerPosition variable to the player's transform

        PlayerPosition = GameObject.Find("Capsule").transform;

        // Define what happens when the active scene from the build index is 1

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // If the Health variable is less than or equal to 0

            if (Health <= Mathf.Round(0))
            {
                // Load the "GameOver" scene

                SceneManager.LoadScene("GameOver");
            }

            // Define what happens when the "1" key is pressed

            if (Input.GetKeyDown("1"))
            {
                // Set ActiveWeapon to the 1st entry in the Weapons list

                ActiveWeapon = Weapons[0];
            }

            // Define what happens when the "2" key is pressed

            if (Input.GetKeyDown("2") && Shotgun == true)
            {
                // Set the ActiveWeapon variable to the 2nd entry in the Weapons list

                ActiveWeapon = Weapons[1];
            }

            // Define what happens when the "3" key is pressed

            if (Input.GetKeyDown("3") && RL == true)
            {
                // Set the ActiveWeapon variable to the 3rd entry in the Weapons list

                ActiveWeapon = Weapons[2];
            }

            // Define what will happen if the Completed boolean is true

            if (Completed == true)
            {
                // Call the "End" fuction after 1.5 seconds

                Invoke("End", 1.5f);
            }
        }
    }

    void TakeDamage(float Damage)
    {
        // Set the Injury variable to 0.1

        Injury = 0.1f;

        // Define what happens if Armour is more than 0

        if (Armour > 0)
        {
            // Decrease the Armour variable by the defined Damage variable

            Armour -= Damage;
        }

        // Define what happens if Armour is less than or equal to 0

        else if (Armour <= Mathf.Round(0))
        {
            // Decrease the Health variable by the defined Damage variable

            Health -= Damage;
        }
        
    }

    void End()
    {
        // Load the "Ending" scene

        SceneManager.LoadScene("Ending");
    }

}
