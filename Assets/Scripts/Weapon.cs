using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        mousePosition.z = transform.position.z;
        transform.position = mousePosition;
        if (mouse.leftButton.isPressed)
            Fire();
        else
            Draw();
    }

    public void Fire()
    {
        if (transform.position.z < 0)
            transform.position += speed * Vector3.forward * Time.deltaTime;
    }

    public void Draw()
    {
        if (transform.position.z > startingPosition.z)
            transform.position += speed * Vector3.back * Time.deltaTime;
    }
}
