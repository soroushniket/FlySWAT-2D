using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bug : MonoBehaviour
{

    private GameManager gameManager;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FlyOut()
    {
        while (gameManager.IsInBnd(transform.position))
        {
            transform.position +=   speed * Time.deltaTime * Vector3.up;
            yield return null;
        }
        Destroy(gameObject);
        gameManager.Spawn();
    }

    public void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameManager.Score(1);
            gameManager.Spawn();
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Friend"))
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}
