using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryConditions : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Friend") ||
            other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<BugManager>().IsSpooked ||
                other.gameObject.GetComponent<BugManager>().IsDead)
            {
                Destroy(other.gameObject);
                gameManager.Spawn();
            }
        }

    }
}
