using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rigidBody;
    private Bug parent;
    private float dropForce = 50;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Bug>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Game Manager")
        {
            Destroy(parent.gameObject);
            if (gameManager.isGameActive)
                gameManager.Spawn();
        }
    }

    public void Die()
    {
        if (parent.gameObject.CompareTag("Enemy"))
        {
            gameManager.Score(1); 
        }
        else if (parent.gameObject.CompareTag("Friend"))
        {
            gameManager.GameOver();
        }
        //Destroy(gameObject);
        parent.IsDead = true;
        rigidBody.useGravity = true;
    }
}
