using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD_Behaviour : MonoBehaviour
{
    private GameManagerScript GM_Script;
    private Shooting_Behaviour Shoot_Script;
    private Text HealthText;
    private Text ArmourText;
    private Text AmmoText;
    private Text GunText;
    private RawImage Injured;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        // Define relevent variables if the active scene frim the build index is 1

       if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Shoot_Script = GameObject.Find("Capsule/Gun").GetComponent<Shooting_Behaviour>();
            HealthText = GameObject.Find("HealthText").GetComponent<Text>();
            ArmourText = GameObject.Find("ArmourText").GetComponent<Text>();
            AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();
            GunText = GameObject.Find("GunText").GetComponent<Text>();
            Injured = GameObject.Find("Injured").GetComponent<RawImage>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Define what the HUD will display when the active scene from the build index is 1

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            HealthText.text = "Health = " + Mathf.Round(GM_Script.Health);
            ArmourText.text = "Armour = " + Mathf.Round(GM_Script.Armour) + "%";
            AmmoText.text = "Ammo = " + Mathf.Round(Shoot_Script.Ammo[Shoot_Script.ActiveAmmo]);
            GunText.text = "Gun: " + GM_Script.ActiveWeapon;

            // Define what happens when the Injury variable in the game manager is more than 0

            if (GM_Script.Injury > 0)
            {
                // Enable the Injured game object

                Injured.enabled = true;

                // Decrease the Injury variable in the game manager over time

                GM_Script.Injury -= Time.deltaTime;
            }

            // Define what happens when the Injury variable in the game manager is less than 0

            else if (GM_Script.Injury <= 0)
            {
                // Disable the Injured game object

                Injured.enabled = false;
            }
        }
    }
}
