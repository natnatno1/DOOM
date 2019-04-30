using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostSoul_Behaviour : MonoBehaviour
{
    public int Health;
    public Transform Player;
    private Animator Anim;
    private Rigidbody RB;
    public GameObject EnemyAmmo;
    public GameObject EnemyGun;
    public GameObject Bullet;
    private float AttackCooldown;
    private GameManagerScript GM_Script;
    public GameObject object1;
    public GameObject object2;
    public GameObject ItemSpawned;
    public bool HasItem;
    
    // Start is called before the first frame update
    void Start()
    {
        // Define Variables
        
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        object1 = this.gameObject;
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

        // Define what happens when the distance between this game object and the player is less than 2, more than 0.35 and when the Health value is more than 0

        if (distance <= 2 && distance > 0.35f && Health > 0)
        {
            // Set the Rigidbody to not Kinematic so that it will move

            RB.isKinematic = false;

            // Move this game object towards the player

            transform.position = Vector3.MoveTowards(transform.position, Player.position, 0.8f * Time.deltaTime);

            // Set the "Attack" variable in the animator to false

            Anim.SetBool("Attack", false);
        }

        // Define what happens when the distance between this game object and the player is less than 0.35, and the Health value is more than 0

        else if (distance <= 0.35f && Health > 0)
        {
            // Set the Rigidbody to Kinematic so that it will not move

            RB.isKinematic = true;

            // Set the "Attack1" boolean in the animator to true

            Anim.SetBool("Attack", true);
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

        // Define what happens when the "LostSoulAttackForward" animation is playing for this gameobject

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("LostSoulAttackForward"))
        {
            // Decrease the AttackCooldown variable over time

            AttackCooldown -= Time.deltaTime;

            // If the AttackCooldown variable is less than or equal to 0

            if (AttackCooldown <= 0)
            {
                // Set the "Injury" variable in the Game Manager to 0.1

                GM_Script.Injury = 0.1f;

                // Decrease the "Health" variable in the Game Manager by the following attack algorithm

                GM_Script.Health -= ((Random.Range(1, 8)) * 3);

                // Reset the "AttackCooldown" variable to 0.417 (the length of the attack animation)

                AttackCooldown = 0.417f;
            }
        }
    }

    // Function to decrease the Health variable and set the "Hit" variable in the animator
    void TakeDamage(int Damage)
    {
        // Set the "Hit" variable in the animator to 1.125 (the length of the hit animation + 1)

        Anim.SetFloat("Hit", 1.025f);

        // Decrease the Health variable for this gameobject by what is defined by a message

        Health -= Damage;
    }
}
