using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
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

    /*private void OnMouseDown()
    {
        Destroy(gameObject);
        gameManager.KillCount++;
        gameManager.Spawn();
    }
    */
}
