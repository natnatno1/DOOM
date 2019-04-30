using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile_Behaviour : MonoBehaviour
{
    private Rigidbody RB_Bullet;
    private Transform Player;
    private GameManagerScript GM_Script;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        GM_Script = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        RB_Bullet = GetComponent<Rigidbody>();
        Player = GameObject.Find("Capsule").transform;

        // Rotate self to look at the player

        transform.LookAt(Player);
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy this game object after 0.5 seconds

        Destroy(this.gameObject, 0.5f);

        // Set the forward velocity of this game object to 10

        RB_Bullet.velocity = transform.forward * 10;

        // Unchild this game object

        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Define what the bullet does when it hits the player

        if (other.gameObject.tag == "Player")
        {
            // Set the "Injury" variable in the Game Manager to 0.1

            GM_Script.Injury = 0.1f;

            // Define the damage that the projectile does depending on it's type

            if (this.gameObject.tag == "EnemyProjectile")
            {
                // Decrease the Health variable in the game manager by the damage algorithm

                GM_Script.Health -= ((3 * (Random.Range(1, 5)) * 3));

                // Destroy this game object

                Destroy(this.gameObject);
            }

            else if (this.gameObject.tag == "EnemyFireball")
            {
                // Decrease the Health variable in the game manager by the damage algorithm

                GM_Script.Health -= ((Random.Range(1, 8)) * 8);

                // Destroy this game object

                Destroy(this.gameObject);
            }

            else if (this.gameObject.tag == "EnemyBullet")
            {
                // Decrease the Health variable in the game manager by the damage algorithm

                GM_Script.Health -= ((Random.Range(1, 8) * 20) + 128);

                // Destroy this game object

                Destroy(this.gameObject);
            }
        }

        // Define what the bullet does when it hits anything that is untagged

        else if (other.gameObject.tag == "Untagged")
        {
            // Destroy this game object

            Destroy(this.gameObject);
        }
    }
}
