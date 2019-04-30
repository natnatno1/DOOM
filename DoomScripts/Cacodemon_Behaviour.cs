using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacodemon_Behaviour : MonoBehaviour
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
    private float AttackCooldown;
    private float AttackCooldown2;
    public bool HasItem;
    public GameObject ItemSpawned;

    // Start is called before the first frame update
    void Start()
    {
        // Define Variables

        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        // Set the attack cooldown to 3 seconds

        AttackCooldown2 = 3;
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

            // Define what happens when this enemy is supposed to drop an item

            if (HasItem == true)
            {
                // Create the item that this enemy is supposed to drop

                Instantiate(ItemSpawned, this.gameObject.transform.position, Quaternion.identity);

                // Set the "HasItem" bool to false

                HasItem = false;
            }
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

        if (distance <= 2 && distance > 0.3f && Health > 0)
        {
            // Set the Rigidbody to not Kinematic so that it will move

            RB.isKinematic = false;

            // Move this game object towards the player

            transform.position = Vector3.MoveTowards(transform.position, Player.position, 0.8f * Time.deltaTime);

            // Set the "Attack1" variable in the animator to false

            Anim.SetBool("Attack1", false);

            // Define what happens depending on how long is left in the AttackCooldown2 float

            // Define what happens when AttackCooldown2 is less than or equal to 0

            if (AttackCooldown2 <= 0)
            {
                // Set the "Attack2" boolean in the animator to true

                Anim.SetBool("Attack2", true);

                // Reset the AttackCooldown2 float to 2.5 seconds

                AttackCooldown2 = 2.5f;
            }
            
            // Define what happens when the AttackCooldown2 is more than 0

            else if (AttackCooldown2 > 0)
            {
                // Set the "Attack2" boolean in the animator to false

                Anim.SetBool("Attack2", false);

                // Set the AttackCooldown2 to decrease over time

                AttackCooldown2 -= Time.deltaTime;
            }

        }

        // Define what happens when the distance between this game object and the player is less than 0.3, and the Health value is more than 0

        else if (distance <= 0.3f && Health > 0)
        {
            // Set the Rigidbody to Kinematic so that it will not move

            RB.isKinematic = true;

            // Set the "Attack1" boolean in the animator to true

            Anim.SetBool("Attack1", true);
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

        // Define what happens when the "CacodemonAttackForward2" animation is playing for this gameobject

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("CacodemonAttackForward2"))
        {
            // Decrease the ShootingCooldown variable over time

           ShootingCooldown -= Time.deltaTime;
        
            // If the ShootingCooldown variable is less than or equal to 0

            if (ShootingCooldown <= 0)
            {
                // Call the Shoot() function

                Shoot();

                // Reset the ShootingCooldown variable to 0.583 (the length of the attack animation)

                ShootingCooldown = 0.583f;
            }
        }

        // Define what happens when the "CacodemonAttackForward1" animation is playing for this gameobject

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("CacodemonAttackForward1"))
        {
            // Decrease the AttackCooldown variable over time

            AttackCooldown -= Time.deltaTime;

            // Define what happens when the AttackCooldown variable is less than or equal to 0

            if (AttackCooldown <= 0)
            {
                // Set the "Injury" variable in the Game Manager to 0.1

                GM_Script.Injury = 0.1f;

                // Decrease the "Health" variable in the Game Manager by the following attack algorithm

                GM_Script.Health -= ((Random.Range(1, 6)) * 10);

                // Reset the "AttackCooldown" variable to 0.417 (the length of the attack animation)

                AttackCooldown = 0.417f;
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
