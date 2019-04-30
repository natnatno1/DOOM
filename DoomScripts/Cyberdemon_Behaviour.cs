using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyberdemon_Behaviour : MonoBehaviour
{
    public int Health;
    public Transform Player;
    private Animator Anim;
    private Rigidbody RB;
    public GameObject EnemyAmmo;
    public GameObject EnemyGun;
    public GameObject Bullet;
    private float ShootingCooldown;
    public GameObject object1;
    public GameObject object2;
    private GameManagerScript GM_Script;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables
        
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate self to look at the player
        transform.LookAt(Player);

        // Create temporary Vector3 so that the x and z rotations are 0 (this means that the sprites will only rotate to look at the player on their y axis)
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        // Set the altered rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);


        // Set the "Health" variable in the animator to match this enemy's health
        Anim.SetInteger("Health", Health);

        // Define what happens when Health is less than or equal to 0

        if (Health <= 0)
        {
            // Set the Rigidbody to Kinematic to stop it from moving

            RB.isKinematic = true;

            // Destroy this enemy's capsule collider to prevent any more collisions

            Destroy(this.gameObject.GetComponent<CapsuleCollider>());

            // Set the "Completed" boolean in the Game Manager to true

            GM_Script.Completed = true;
            
        }

        // Define what happens when Health is more than 0

        else if (Health >= 0)
        {
            // Set the Rigidbody to not Kinematic so that it will move

            RB.isKinematic = false;
        }

        // Get the distance between this game object and the player

        var distance = Vector3.Distance(object1.transform.position, object2.transform.position);

        // Define what happens when the distance between this game object and the player is less than 2, more than 0.3 and when the Health value is more than 0

        if (distance <= 2 && distance > 1 && Health > 0)
        {
            // Set the Rigidbody to not Kinematic so that it will move

            RB.isKinematic = false;

            // Move this game object towards the player

            transform.position = Vector3.MoveTowards(transform.position, Player.position, 0.8f * Time.deltaTime);

            // Set the "Shooting" variable in the animator to false

            Anim.SetBool("Shooting", false);
        }

        // Define what happens when the distance between this game object and the player is less than 0.3, and the Health value is more than 0

        else if (distance <= 1 && Health > 0)
        {
            // Set the Rigidbody to Kinematic so that it will not move

            RB.isKinematic = true;

            // Set the "Shooting" boolean in the animator to true

            Anim.SetBool("Shooting", true);
        }

        // Define what happens when the "Hit" variable in the animator is more than 0

        if (Anim.GetFloat("Hit") > 0)
        {
            // Set the "Hit" variable in the animator to decrease over time

            Anim.SetFloat("Hit", (Anim.GetFloat("Hit") - Time.deltaTime));
        }

        // Define what happens when the "Hit" variable in the animator is equal to or less than 0

        else if (Anim.GetFloat("Hit") <= 0)
        {
            // Set the "Hit" variable in the animator to 0

            Anim.SetFloat("Hit", 0);
        }

        // Define what happens when the "CyberdemonAttackForward" animation is playing for this gameobject

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("CyberdemonAttackForward"))
        {
            // Decrease the ShootingCooldown variable over time

            ShootingCooldown -= Time.deltaTime;

            // If the ShootingCooldown variable is less than or equal to 0

            if (ShootingCooldown <= 0)
            {
                // Call the Shoot() function

                Shoot();

                // Reset the ShootingCooldown variable to 0.5 (the length of the attack animation)

                ShootingCooldown = 0.5f;
            }
        }

    }

    // Function to create bullet and child it to this gameobject's EnemyGun transform
    void Shoot()
    {
        // Create a bullet at the EnemyGun transform for this gameobject

        Bullet = Instantiate(EnemyAmmo, EnemyGun.transform.position, Quaternion.identity);

        // Child the bullet to the EnemyGun transform for this gameobject

        Bullet.transform.parent = EnemyGun.transform;
    }

    // Function to decrease the Health variable and set the "Hit" variable in the animator
    void TakeDamage(int Damage)
    {
        // Set the "Hit" variable in the animator to 1.125 (the length of the hit animation + 1)

        Anim.SetFloat("Hit", 1.125f);

        // Decrease the Health variable for this gameobject by what is defined by a message

        Health -= Damage;
    }
}