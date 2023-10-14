using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Quaternion drawnRotation;
    [SerializeField] private Quaternion firedRotation;
    private AudioSource hitSound;
    public bool IsSpooky;

    void Start()
    {
        hitSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, firedRotation, speed * Time.deltaTime);  
    }

    public void Draw()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, drawnRotation, speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Wall")
        {
            hitSound.Play();
        }

    }
    
}
