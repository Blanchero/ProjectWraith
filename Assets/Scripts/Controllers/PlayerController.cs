using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerController player;
    public StatsMorrigan playerShip;
    public float playerMaxThrust;
    public float playerMaxAccel;
    public float playerRoll;
    public float playerPitch;
    public float forwardThrust = 0;

    public Text throttleText;

    private Rigidbody playerRigidBody;
    private Vector3 eulerRotation;

    private void Start()
    {
        player = this;
        playerShip = FindObjectOfType<StatsMorrigan>();
        playerMaxThrust = playerShip.maxThrust;
        playerMaxAccel = playerShip.maxAccel;
        playerRoll = playerShip.roll;
        playerPitch = playerShip.pitch;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    { 
        //Capture inputs for roll, pitch, and throttle
        float inputRoll = Input.GetAxis("Roll");
        float inputPitch = Input.GetAxis("Pitch");
        float inputThrottleUp = Input.GetAxis("Throttle Up");
        float inputThrottleDown = Input.GetAxis("Throttle Down");

        //Throttle up and down
        bool accelerating = false;
        bool decelerating = false;
        float currentAccel = 0;
        
        if (inputThrottleUp > 0)
        {
            accelerating = true;
            decelerating = false;
        }

        if (inputThrottleDown > 0)
        {
            accelerating = false;
            decelerating = true;
        }

        if (accelerating == true)
        {
            Mathf.Clamp(currentAccel++, 0.0f, playerMaxAccel);
        }

        if (decelerating == true)
        {
            Mathf.Clamp(currentAccel--, 0.0f, playerMaxAccel);
        }

        forwardThrust = Mathf.Clamp(forwardThrust + currentAccel, 0.0f, playerMaxThrust);
        playerRigidBody.velocity = transform.forward * forwardThrust;
        throttleText.text=("Throttle: " + Mathf.RoundToInt((forwardThrust/playerMaxThrust) * 100) + "%");
        
        //Pitch and roll the ship
        eulerRotation = new Vector3(inputPitch * playerPitch, 0.0f, inputRoll * -playerRoll);
        Quaternion deltaRotation = Quaternion.Euler(eulerRotation * Time.deltaTime);
        playerRigidBody.MoveRotation(playerRigidBody.rotation * deltaRotation);
    }
    
}
