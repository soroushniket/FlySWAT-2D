using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugInstinct : MonoBehaviour
{
    private BugManager bugManager;
    // Start is called before the first frame update
    void Start()
    {
        bugManager = GetComponentInParent<BugManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (!bugManager.IsSpooked && 
            other.gameObject.CompareTag("Weapon") &&
            other.gameObject.GetComponent<WeaponColliderManager>().IsSpooky)
        {
            bugManager.IsSpooked = true;
            bugManager.ThreatPosition = other.gameObject.transform.position;
        }
            
    }
}
