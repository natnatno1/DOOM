using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Behaviour : MonoBehaviour
{

    public float Speed;
    public float Lifetime;
    private int Damage;
    private Rigidbody RB_Bullet;
    private Transform EndOfBarrel;

    // Start is called before the first frame update
    void Start()
    {
        // Define variables

        RB_Bullet = GetComponent<Rigidbody>();
        EndOfBarrel = GameObject.Find("Capsule/Gun/EndOfBarrel").transform;

        // Rotate the ball so that it is facing the end of the player's gun barrel

        transform.LookAt(EndOfBarrel);

        // Add a force to the bullet in the opposite direction of the player's gun barrel

        RB_Bullet.AddForce(EndOfBarrel.transform.forward * Speed);

        // Define the damage that the bullet does depending on it's type

        if (this.gameObject.tag == "Bullet")
        {
            Damage = ((Random.Range(1, 3)) * 5);
        }

        if (this.gameObject.tag == "Shell")
        {
            Damage = ((7 * (Random.Range(1, 3))) * 5);
        }

        if (this.gameObject.tag == "Rocket")
        {
            Damage = (Random.Range(1, 8) * 20);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Destroy bullet after a certain amount of time

        Destroy(this.gameObject, Lifetime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Define what the bullet does when it hits an enemy

        if (other.gameObject.tag == "Enemy")
        {
            // Send damage value to the enemy

            other.SendMessage("TakeDamage", Damage);

            // Destroy the bullet

            Destroy(this.gameObject);
        }

    }
}
