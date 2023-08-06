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
    Camera mainCamera;
    public float vertExtent;
    public float horzExtent;
    [SerializeField] public float leftBound;
    [SerializeField] public float rightBound;
    [SerializeField] public float topBound;
    [SerializeField] public float bottomBound;

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
    public Weapon weapon;
    public bool isGameActive;
    public float timeRemaining = DURATION;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        vertExtent = mainCamera.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        leftBound = -horzExtent*0.9f;
        rightBound = horzExtent*0.9f;
        topBound = vertExtent*0.8f; // lower To account for the UI components
        bottomBound = -vertExtent*0.9f;

        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();

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
        int isFly = Random.Range(0, 2);
        //Quaternion spawnRotation = Quaternion.AngleAxis(-90, transform.right);
        Quaternion spawnRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        Vector3 spawnPosition = new Vector3(Random.Range(-horzExtent, horzExtent), Random.Range(-vertExtent, vertExtent), 0);
        if (Mathf.Abs(weapon.transform.position.x) > Mathf.Abs(weapon.transform.position.y))
            spawnPosition.x = (weapon.transform.position.x < 0) ? horzExtent : -horzExtent;
        else
            spawnPosition.y = (weapon.transform.position.y < 0) ? vertExtent : -vertExtent;

        if (isFly > 0)
            Instantiate(FlyPrefab, spawnPosition, spawnRotation);
        else
            Instantiate(LadybugPrefab, spawnPosition, spawnRotation);
    }

    public void Score(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
