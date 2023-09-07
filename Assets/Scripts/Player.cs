using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationalSpeed;
    //public Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = -Input.GetAxis("Horizontal") * rotationalSpeed * Time.deltaTime;
        transform.Rotate(0, 0, horizontalMovement);
        Console.WriteLine(horizontalMovement);
    }

    private void FixedUpdate()
    {
        
    }
}
