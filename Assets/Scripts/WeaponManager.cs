using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{


    public WeaponColliderManager weaponColliderManager;
    private Vector3 previousMousePosition;
    
    private float spookTransitionThresshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        weaponColliderManager = gameObject.transform.Find("Collider").GetComponent<WeaponColliderManager>();
        Mouse mouse = Mouse.current;
        Vector3 previousMousePosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        mousePosition.z = transform.position.z;
        transform.position = mousePosition;
        if (Vector3.Distance(mousePosition, previousMousePosition)> spookTransitionThresshold)
        {
            weaponColliderManager.IsSpooky = true;
        }
        else
        {
            weaponColliderManager.IsSpooky = false;
        }
        previousMousePosition = mousePosition;
        if (mouse.leftButton.isPressed)
        {
            weaponColliderManager.Fire();
        }
        else
        {
            weaponColliderManager.Draw();
        }
    }
}
