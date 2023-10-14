using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BugManager : MonoBehaviour
{
    public bool IsSpooked;
    public bool IsDead;
    public bool IsFlying;
    public Vector3 ThreatPosition;

    [SerializeField] private float SpiralVelocity;
    [SerializeField] private float SpiralTowardsAlpha;
    [SerializeField] private float SpiralAwayAlpha;
    [SerializeField] private float ReactionTime;
    [SerializeField] private float dropForce;

    private Vector3 LandingPosition;
    private Animator bugAnimator;
    private GameManager gameManager;
    private Rigidbody rigidBody;
    private AudioSource flyingSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bugAnimator = GetComponentInChildren<Animator>();
        flyingSound = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        LandingPosition.x = (transform.position.x > 0) ? UnityEngine.Random.Range(0, gameManager.rightBound) : UnityEngine.Random.Range(gameManager.leftBound, 0);
        LandingPosition.y = (transform.position.y > 0) ? UnityEngine.Random.Range(0, gameManager.topBound) : UnityEngine.Random.Range(gameManager.bottomBound, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (IsSpooked)
                if (ReactionTime < 0)
                {
                    IsFlying = true;
                    FlySpiral(ThreatPosition, SpiralVelocity, SpiralAwayAlpha);
                }

                else
                {
                    ReactionTime -= Time.deltaTime;
                    IsFlying = false;
                }
            else if (Vector3.Distance(transform.position, LandingPosition) > SpiralVelocity * Time.deltaTime)
            {
                IsFlying = true;
                FlySpiral(LandingPosition, SpiralVelocity, SpiralTowardsAlpha);
            }
            else
            {
                IsFlying = false;
            }
        }
        else
        {
            IsFlying = false;
        }
        
        
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            bugAnimator.SetBool("IsDead", true);
            flyingSound.Stop();
        }
        else if (IsFlying)
        {
            bugAnimator.SetBool("IsFlying", true);
            if (!flyingSound.isPlaying)
                flyingSound.Play();
        }
        else
        {
            bugAnimator.SetBool("IsFlying", false);
            flyingSound.Pause();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameManager.Score(1);
        }
        else if (gameObject.CompareTag("Friend"))
        {
            gameManager.GameOver();
        }
        IsDead = true;
        rigidBody.useGravity = true;
        rigidBody.AddForce(dropForce * Vector3.down);
    }
    public void FlySpiral(Vector3 sourcePosition, float spiralVelocity, float spiralAlpha)
    {
        Vector3 sourceDirection = (sourcePosition - transform.position).normalized;
        float lookAngle = Vector3.Angle(sourceDirection, transform.up);
        transform.Rotate(0, 0, spiralAlpha - lookAngle);
        transform.Translate(0, spiralVelocity * Time.deltaTime, 0);
    }
}
