using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    const int DURATION = 60;
    private int score;
    
    [SerializeField] private float leftBnd = -6;
    [SerializeField] private float rightBnd = 6;
    [SerializeField] private float topBnd = 5;
    [SerializeField] private float bottomBnd = -5;

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
    public GameObject WeaponPrefab;
    public bool isGameActive;
    public float timeRemaining = DURATION;

    void Start()
    {
        Instantiate(WeaponPrefab);
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

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = timeRemaining.ToString("F0");

            if (timeRemaining < 0)
                GameOver();
        }
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
        timeRemaining = DURATION;
        Score(0);
        isGameActive = true;
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

    public void Spawn()
    {
        int rand = Random.Range(0, 2);
        if (rand > 0)
            Instantiate(LadybugPrefab, new Vector3(Random.Range(leftBnd, rightBnd), Random.Range(bottomBnd, topBnd), 0), LadybugPrefab.transform.rotation);
        else
            Instantiate(FlyPrefab, new Vector3(Random.Range(leftBnd, rightBnd), Random.Range(bottomBnd, topBnd), 0), FlyPrefab.transform.rotation);
    }
    /*
    public void Shoo()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            StartCoroutine(Flee(enemy));
        }
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
        foreach (GameObject friend in friends)
        {
            StartCoroutine(Flee(friend));
        }
    }
    */
    public bool IsInBnd(Vector3 loc)
    {
        if (loc.x > leftBnd   && loc.x < rightBnd &&
            loc.y > bottomBnd && loc.y < topBnd)
            return true;
        else
            return false;
    }

    public void Score(int points)
    {
        score+=points;
        scoreText.text = "Score: " + score;
    }
}
