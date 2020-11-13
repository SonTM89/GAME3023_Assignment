﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 5;

    private string saveKey;

    // Start is called before the first frame update
    void Start()
    {
        saveKey = "PlayerLocation";

        SaveLocation.LoadPlayerLocation();

        Saver.OnSave.AddListener(SaveLocation.SavePlayerLocation);
    }

    // Update is called once per frame
    void Update()
    {
        // Set Player velocity
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementVector *= speed;

        //transform.Translate(movementVector);
        rb.velocity = movementVector;
    }
}
