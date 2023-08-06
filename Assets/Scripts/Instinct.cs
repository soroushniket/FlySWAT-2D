using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instinct : MonoBehaviour
{
    private Bug parent;
    // Start is called before the first frame update
    void Start()
    {
        parent  = GetComponentInParent<Bug>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (!parent.IsSpooked && 
            other.gameObject.CompareTag("Weapon"))
        {
            parent.IsSpooked = true;
            parent.ThreatPosition = other.gameObject.transform.position;
        }
            
    }
}
