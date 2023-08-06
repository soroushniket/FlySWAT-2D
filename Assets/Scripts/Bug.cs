using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using System;


public class Bug : MonoBehaviour
{
    public bool IsSpooked;
    public bool IsDead;
    public float SpiralVelocity;
    public float SpiralTowardsAlpha;
    public float SpiralAwayAlpha;
    public float ReactionTime;
    public Vector3 ThreatPosition;
    public Vector3 LandingPosition;

    private AudioSource flyingSound;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        flyingSound = GetComponent<AudioSource>();
        flyingSound.Play();
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
                    FlySpiral(ThreatPosition, SpiralVelocity, SpiralAwayAlpha);
                    if (!flyingSound.isPlaying)
                        flyingSound.Play();
                }

                else
                    ReactionTime -= Time.deltaTime;
            else if (Vector3.Distance(transform.position, LandingPosition) > SpiralVelocity * Time.deltaTime)
                FlySpiral(LandingPosition, SpiralVelocity, SpiralTowardsAlpha);
            else
                flyingSound.Pause();
        }   
        else
            flyingSound.Stop();
    }
    public void FlySpiral(Vector3 sourcePosition, float spiralVelocity, float spiralAlpha)
    {
        Vector3 sourceDirection = (sourcePosition - transform.position).normalized;
        float lookAngle = Vector3.Angle(sourceDirection, transform.up);
        transform.Rotate(0, 0, spiralAlpha - lookAngle);
        transform.Translate(0, spiralVelocity * Time.deltaTime, 0);
    }
    /*
    public void FlySpiral(Vector3 source, float velocity, float alpha, float beta)
    {
        float r0 = Mathf.Sqrt(Mathf.Pow(transform.position.y - source.y, 2) +
                            Mathf.Pow(transform.position.y - source.y, 2));
        float theta0 = Mathf.Atan((transform.position.y - source.y) /
                            (transform.position.y - source.y));
        float z0 = transform.position.z - source.z;
        float a = velocity * Mathf.Cos(Mathf.Deg2Rad * alpha) * Mathf.Cos(Mathf.Deg2Rad * beta);
        float b = velocity * Mathf.Cos(Mathf.Deg2Rad * alpha) * Mathf.Sin(Mathf.Deg2Rad * beta);
        float c = Mathf.Tan(alpha) / Mathf.Sin(beta);
        float x = (r0 - b * Time.deltaTime) * Mathf.Cos(theta0 - c * Mathf.Log(1 + a * Time.deltaTime / z0));
        float y = (r0 - b * Time.deltaTime) * Mathf.Sin(theta0 - c * Mathf.Log(1 + a * Time.deltaTime / z0));
        float z = z0 + a * Time.deltaTime;
        transform.position =  new Vector3(x, y, z) + source;
    }
    */
    /*
    public void FlySpiral(Vector3 targetPosition, float velocity, float alpha)
    {
        Vector3 targetDirection = Vector3.ProjectOnPlane(targetPosition - transform.position, new Vector3(0, 0, 1)); // > Define the plane in gameManager
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection, Vector3.forward);
        transform.rotation = lookRotation;
        transform.Rotate(0,0,alpha,Space.World);
        transform.Translate(velocity * Time.deltaTime * Vector3.forward);
    }
    */
    /*
    // Don't look back
    public void FlyAwayFrom(Vector3 threatPosition)
    {
        Vector3 threatDirection = (Vector3.ProjectOnPlane(threatPosition, new Vector3(0, 0, 1)) - transform.position).normalized;
        Quaternion fleeRotation = Quaternion.LookRotation(-threatDirection, -Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, fleeRotation, rotationSpeed * 180 / Mathf.PI * Time.deltaTime);
        transform.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);
    }

    public void FlyInTowards(Vector3 landingPosition)
    {
        Vector3 landingDirection = (Vector3.ProjectOnPlane(landingPosition, new Vector3(0, 0, 1)) - transform.position).normalized;
        Quaternion landingRotation = Quaternion.LookRotation(landingDirection, -Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, landingRotation, rotationSpeed * 180 / Mathf.PI * Time.deltaTime);
        transform.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);
    }
    */

    // Keep an eye on the threst all the time
    /*
    public void FlyAway(GameObject threat)
    {
        Vector3 threatDirection = (Vector3.ProjectOnPlane(threat.transform.position, new Vector3(0, 0, 1)) - transform.position).normalized;
        Quaternion fleeRotation = Quaternion.LookRotation(-threatDirection, -Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, fleeRotation, rotationSpeed * 180/Mathf.PI* Time.deltaTime);
        transform.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);
    
    }
    */


    
    /*
    public class Flight
    {
        public float Alpha
        {
            get => Mathf.Rad2Deg*(Mathf.Acos(Mathf.Sqrt(a * a + b * b) / v));
            set
            {
                a = v * Mathf.Cos(Mathf.Deg2Rad * value) * Mathf.Cos(Mathf.Deg2Rad * Beta);
                b = v * Mathf.Cos(Mathf.Deg2Rad * value) * Mathf.Sin(Mathf.Deg2Rad * Beta);
            } 
        }
        public float Beta
        {
            get => Mathf.Rad2Deg * Mathf.Atan(b/a);
            set
            {
                a = v* Mathf.Cos(Mathf.Deg2Rad * Alpha) * Mathf.Cos(Mathf.Deg2Rad * value);
                b = v* Mathf.Cos(Mathf.Deg2Rad * Alpha) * Mathf.Sin(Mathf.Deg2Rad * value);
            }
        }
        public float Velocity
        {
            get => v;
            set { v = value; }
        }
        float a;
        float b;
        float c;
        float v;
        float r0;
        float theta0;
        float z0;
        Vector3 s;

        public Flight(Vector3 source, Vector3 initialPosition)
        {
            s = source;
            r0 = Mathf.Sqrt(Mathf.Pow(initialPosition.y - source.y, 2)+
                            Mathf.Pow(initialPosition.y - source.y, 2));
            theta0 = Mathf.Atan((initialPosition.y - source.y)/
                                (initialPosition.y - source.y));
            z0 = initialPosition.z - source.z;
        }

        public Vector3 GetCoordinats(float t)
        {
            float x = (r0 - b * t) * Mathf.Cos(theta0 - c * Mathf.Log(1 + a * t / z0));
            float y = (r0 - b * t) * Mathf.Sin(theta0 - c * Mathf.Log(1 + a * t / z0));
            float z = z0 + a * t;
            return (new Vector3(x, y, z) + s);
        }
    }
    */
}
