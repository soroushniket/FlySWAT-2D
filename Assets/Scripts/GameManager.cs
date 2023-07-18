using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Camera mainCamera;
    private int score;
    [SerializeField] private float leftBound = -10;
    [SerializeField] private float righBound = 10;
    [SerializeField] private float topBound = 5;
    [SerializeField] private float bottomBound = -5;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI gameOverText;
    public Button startButton;
    public Button restartButton;
    public Button continueButton;
    public Button menuButton;
    public GameObject FlyPrefab;
    public GameObject LadybugPrefab;
    public bool isGameActive;
    public float timeRemaining = 60;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        isGameActive = false;
    }

    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        if (isGameActive)
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
                        Score(1);
                        Spawn();
                    }
                    else
                    {
                        Destroy(hit.collider.gameObject);
                        GameOver();
                    }
                }
                Shoo();
                if (isGameActive)
                    Spawn();
            }
            timeRemaining -= Time.deltaTime;
            timerText.text = timeRemaining.ToString("F0");

            if (timeRemaining < 0)
                GameOver();
        }
    }

    public void Spawn()
    {
        int rand = Random.Range(0, 2);
        if (rand > 0)
            Instantiate(LadybugPrefab, new Vector3(Random.Range(leftBound, righBound), Random.Range(bottomBound, topBound), 0), LadybugPrefab.transform.rotation);
        else
            Instantiate(FlyPrefab, new Vector3(Random.Range(leftBound, righBound), Random.Range(bottomBound, topBound), 0), FlyPrefab.transform.rotation);
    }

    public void StartGame()
    {
        gameOverText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        score = 0;
        Score(0);
        isGameActive = true;
        Shoo();
        Spawn();
    }

    public void PauseGame()
    {
        titleText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        isGameActive = false;
    }

    public void ResumeGame()
    {
        titleText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        isGameActive = true;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        isGameActive = false;
    }

    public void Shoo()
    {
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
    }

    private void Score(int points)
    {
        score+=points;
        scoreText.text = "Score: " + score;
    }
}
