using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 100f;
    [Tooltip("In m")] [SerializeField] float xRange = 21f;
    [Tooltip("In m")] [SerializeField] float yRange = 16.5f;


    [SerializeField] GameObject[] guns;


    [Header("Position and Rotation")]
    [SerializeField] float positionPitchFactor = -1.6f;
    [SerializeField] float controlPitchFactor = -1.9f;
    [SerializeField] float positionYawFactor = 1.2f;
    [SerializeField] float controlRollFactor = -2f;

    void Awake()
    {
        // Make the game run as fast as possible in Windows
        Application.targetFrameRate = 200;
    }

    float xThrow, yThrow;
    bool isControlEnabled = true;
    void Update()

    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
       // called by string reference - if you rename either this won't work
    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); 
    }
    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else 
        {
            SetGunsActive(false);

        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns) // may affect death FX
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;

            emissionModule.enabled = isActive;
        }
    }

   
}
