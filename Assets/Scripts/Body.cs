using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private GameManager gameManager;
    private Bug parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Bug>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            parent.Die();
        }
    }
}
