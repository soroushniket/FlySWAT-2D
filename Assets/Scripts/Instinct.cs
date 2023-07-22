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
        if (other.gameObject.CompareTag("Weapon"))
            StartCoroutine(parent.FlyOut());
    }
}
