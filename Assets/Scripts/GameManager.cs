using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float leftBound = -10;
    [SerializeField] private float righBound = 10;
    [SerializeField] private float topBound = 5;
    [SerializeField] private float bottomBound = -5;

    public GameObject FlyPrefab;
    public GameObject LadybugPrefab;
    public int Score;
    public bool isGameOver;
    public float timeRemaining = 60;

    private Camera mainCamera;

    void Start()
    {
        Score = 0;
        Spawn();
    }

    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Destroy(hit.collider.gameObject);
                    Score++;
                    Spawn();
                }
                else
                {
                    Destroy(hit.collider.gameObject);
                    isGameOver = true;
                }   
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
            foreach (GameObject friend in friends)
            {
                Destroy(friend);
            }
            Spawn();
        }
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
            isGameOver = true;
    }

    public void Spawn()
    {
        int rand = Random.Range(0, 2);
        if (rand > 0)
            Instantiate(LadybugPrefab, new Vector3(Random.Range(leftBound, righBound), Random.Range(bottomBound, topBound), 0), LadybugPrefab.transform.rotation);
        else
            Instantiate(FlyPrefab, new Vector3(Random.Range(leftBound, righBound), Random.Range(bottomBound, topBound), 0), FlyPrefab.transform.rotation);
    }

}
