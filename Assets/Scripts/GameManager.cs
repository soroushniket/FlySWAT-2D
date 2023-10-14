using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    const int DURATION = 60;

    private int score;
    private int highScore;
    private Camera mainCamera;
    private AudioSource audioPlayer;
    private WeaponManager weaponManager;
    private float vertExtent;
    private float horzExtent;
    //private ParticleSystem confetti;

    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public Button startButton;
    public Button restartButton;
    public Button continueButton;
    public Button menuButton;
    public GameObject FlyPrefab;
    public GameObject LadybugPrefab;
    public bool isGameActive;
    public float timeRemaining = DURATION;
    public AudioClip applaudSound;
    public AudioClip booSound;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //confetti = GameObject.Find("Confetti").GetComponent<ParticleSystem>();
        audioPlayer = mainCamera.GetComponent<AudioSource>();
        vertExtent = mainCamera.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        leftBound = -horzExtent*0.9f;
        rightBound = horzExtent*0.9f;
        topBound = vertExtent*0.8f; // lower To account for the UI components
        bottomBound = -vertExtent*0.9f;
        weaponManager = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponManager>();

        //confetti.Stop();
        gameOverText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        weaponManager.gameObject.SetActive(false);
        isGameActive = false;
        highScore = 0;
    }

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = timeRemaining.ToString("F0");

            if (timeRemaining < 0)
                GameFinished();
        }
    }


    public void StartGame()
    {
        gameOverText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        weaponManager.gameObject.SetActive(true);
        audioPlayer.Play();

        score = 0;
        timeRemaining = DURATION;
        Score(0);
        isGameActive = true;
        Spawn();
    }

    public void PauseGame()
    {
        titleText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        isGameActive = false;
        audioPlayer.Pause();
        weaponManager.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        titleText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        isGameActive = true;
        audioPlayer.Play();
        weaponManager.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        DestroyAllBugs();
        gameOverText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        isGameActive = false;
        audioPlayer.Stop();
        audioPlayer.PlayOneShot(booSound);
        weaponManager.gameObject.SetActive(false);
    }

    public void GameFinished()
    {
        DestroyAllBugs();
        gameOverText.gameObject.SetActive(false);
        finalScoreText.text = scoreText.text;
        finalScoreText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        isGameActive = false;
        audioPlayer.Stop();
        weaponManager.gameObject.SetActive(false);
        if (score > highScore)
        {
            highScore = score;
            audioPlayer.PlayOneShot(applaudSound);
            //confetti.Play();
            finalScoreText.text = "New High Score: " + highScore;
        }
        else
        {
            finalScoreText.text = "Score: " + score + "\nHigh Score: " + highScore;
        }
    }

    public void Spawn()
    {
        int isFly = Random.Range(0, 5);
        Quaternion spawnRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        Vector3 spawnPosition = new Vector3(Random.Range(bottomBound, topBound), Random.Range(leftBound, rightBound), 0);
        Vector3 weaponPosition = weaponManager.transform.position;
        if (Mathf.Abs(weaponPosition.x) > Mathf.Abs(weaponPosition.y))
            spawnPosition.x = (weaponPosition.x < 0) ? horzExtent : -horzExtent;
        else
            spawnPosition.y = (weaponPosition.y < 0) ? vertExtent : -vertExtent;

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

    public void DestroyAllBugs()
    {
        Destroy(GameObject.FindGameObjectWithTag("Friend"));
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }
}
