using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Public Variables
    public Color color;

    // Private Variables
    private Renderer body;
    private Rigidbody _rigidBody;
    private Coroutine _powerUpCoroutine;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float powerUpDuration;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private void Awake()
    {
        // Getting rigidbody component
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Locking Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Getting the renderer of the capsule body
        // Doing it this way in case we need to add other children in the future
        Renderer[] child =  GetComponentsInChildren<Renderer>();
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].gameObject.name == "Body")
            {
                body = child[i];
                break;
            }
        }

        // Setting the material of the body to the given color
        body.material.color = color;
    }

    // Update is called once per frame
    private void Update()
    {
        // Getting horizontal and vertical values
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Adjust the horizontal and vertical direction to use the camera's
        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;

        // Make movement in the y direction 0
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        // Calculating and setting velocity
        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }

    public void PickPowerUp()
    {
        Debug.Log("Pick Power Up");
        if (_powerUpCoroutine != null)
        {
            StopCoroutine( _powerUpCoroutine );
        }
        
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        Debug.Log("Start Power Up");
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        Debug.Log("Stop Power Up");
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }
}
