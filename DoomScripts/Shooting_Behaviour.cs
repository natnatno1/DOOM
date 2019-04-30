using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Behaviour : MonoBehaviour
{
    private GameManagerScript GM_Script;
    public GameObject Bullet;
    public GameObject Shell;
    public GameObject Rocket;
    public GameObject EndOfBarrel;
    public int[] Ammo;
    public int ActiveAmmo;
    private Animator Anim;
    private float ShootingCooldown;


    // Start is called before the first frame update
    void Start()
    {
        // Create a new list with 3 entries

        Ammo = new int[3];
        Ammo[0] = 100;
        Ammo[1] = 75;
        Ammo[2] = 50;

        // Set which entry in the Ammo list is being used

        ActiveAmmo = 0;

        // Define variables

        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Define what happens when the "1" key is pressed

        if (Input.GetKeyDown("1"))
        {
            // Set active ammo to the 1st entry in the Ammo list

            ActiveAmmo = 0;

            // Set the "Pistol" boolean in the animator to true

            Anim.SetBool("Pistol", true);

            // Set the "Shotgun" boolean in the animator to false

            Anim.SetBool("Shotgun", false);

            // Set the "RL" boolean in the animator to false

            Anim.SetBool("RL", false);
        }
        
        // Define what happens when the "2" key is pressed

        if (Input.GetKeyDown("2") && GM_Script.Shotgun == true)
        {
            // Set active ammo to the 2nd entry in the Ammo list

            ActiveAmmo = 1;

            // Set the "Pistol" boolean in the animator to false

            Anim.SetBool("Pistol", false);

            // Set the "Shotgun" boolean in the animator to true

            Anim.SetBool("Shotgun", true);

            // Set the "RL" boolean in the animator to false

            Anim.SetBool("RL", false);
        }

        // Define what happens when the "3" key is pressed

        if (Input.GetKeyDown("3") && GM_Script.RL == true)
        {
            // Set active ammo to the 3rd entry in the Ammo list

            ActiveAmmo = 2;

            // Set the "Pistol" boolean in the animator to false

            Anim.SetBool("Pistol", false);

            // Set the "Shotgun" boolean in the animator to false

            Anim.SetBool("Shotgun", false);

            // Set the "RL" boolean in the animator to true

            Anim.SetBool("RL", true);
        }

        // Decrease the ShootingCooldown variable over time

        ShootingCooldown -= Time.deltaTime;

        // Define what happens when the "Fire1" button is pressed

        if (Input.GetButtonDown("Fire1"))
        {
            // Define what happens when the active weapon from the weapons list in the game manager is "Pistol"

            if (GM_Script.ActiveWeapon == "Pistol" && Ammo[0] > 0)
            {
                // Set the "PistolShoot" boolean in the animator to true

                Anim.SetBool("PistolShoot", true);
                
                // Define what happens when the ShootingCooldown variable is less than or equal to 0

                if (ShootingCooldown <= 0)
                {
                    // Create a bullet at the end of the barrel

                    Instantiate(Bullet, EndOfBarrel.transform.position, Quaternion.identity);

                    // Decrease the 1st entry in the ammo list by 1

                    Ammo[0] -= 1;

                    // Reset the ShootingCooldown variable to 0.6

                    ShootingCooldown = 0.6f;
                }
           
            }

            // Define what happens when the active weapin from the weapons list in the game manager is "Shotgun"

            else if (GM_Script.ActiveWeapon == "Shotgun" && Ammo[1] > 0)
            {
                // Define what happens when the ShootingCooldown is less than or equal to 0

                if (ShootingCooldown <= 0)
                {
                    // Set the "ShotgunShoot" boolean in the animator to true

                    Anim.SetBool("ShotgunShoot", true);

                    // Create a shell at the end of the barrel

                    Instantiate(Shell, EndOfBarrel.transform.position, Quaternion.identity);

                    // Decrease the 2nd entry in the ammo list by 1

                    Ammo[1] -= 1;

                    // Reset the ShootingCooldown cariable to 0.867

                    ShootingCooldown = 0.867f;
                }
            }

            // Define what happens when the active weapon from the weapons list in the game manager is "RL"

            else if (GM_Script.ActiveWeapon == "Rocket Launcher" && Ammo[2] > 0)
            {
                // Define what happens when the ShootingCooldown is less than or equal to 0

                if (ShootingCooldown <= 0)
                {
                    // Set the "RLShoot" boolean in the animator to true

                    Anim.SetBool("RLShoot", true);

                    // Create a Rocket at the end of the barrel

                    Instantiate(Rocket, EndOfBarrel.transform.position, Quaternion.identity);

                    // Decrease the 3rd entry in the ammo list by 1

                    Ammo[2] -= 1;

                    // Reset the ShootingCooldown variable to 0.6

                    ShootingCooldown = 0.6f;
                }
            }
        }

        // Define what happens when the current animation for this game object is "PistolShoot"

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("PistolShoot"))
        {
            // Set the "PistolShoot" boolean to false in the animator

            Anim.SetBool("PistolShoot", false);
        }

        // Define what happens when the current animation for this game object is "Shotgun"

        else if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Shotgun"))
        {
            // Set the "ShootingShoot" boolean to false in the animator

            Anim.SetBool("ShotgunShoot", false);
        }

        // Define what happens when the current animation for this game object is "RL"

        else if (Anim.GetCurrentAnimatorStateInfo(0).IsName("RL"))
        {
            // Set the "RLShoot" boolean to false in the animator

            Anim.SetBool("RLShoot", false);
        }
    }
}
