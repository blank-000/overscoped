using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float timeBetweenJumps = 2f;
    
    public float maxDistanceToWall = .5f;
    Animator anim;
    [SerializeField]LayerMask sceneCollision;

    float timeToJump;
    
    Rigidbody RB;
    public float rotationSpeed = 1.0f; // Speed of rotation
    private Quaternion targetRotation; // Target rotation
    private bool isRotating = false; // Flag to check if rotation is in progress

    float current = 0, target = 1;

    public bool canRotate;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        timeToJump = timeBetweenJumps;
        RotateToRandomRotation();
    }

void Update()
{



    timeToJump -= Time.deltaTime;

    if(timeToJump < 0)
    {
        Jump();
    }

    if(!canRotate) return;
    Debug.Log("hrere");
    if (isRotating)
    {
        current = Mathf.MoveTowards(current, target, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, current);

        // If we're very close to the target rotation, we consider the rotation complete
        if (current >= target -.1f)
        {
            current = 0;
            target = 1;
            isRotating = false;
        }
    }
    // If rotation is complete, start the rotation process again
    else
    {
        RotateToRandomRotation();
    }
}





    void Jump()
    {
        RB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        RB.AddForce(transform.forward * jumpForce * 3f, ForceMode.Impulse);
        timeToJump = timeBetweenJumps;
        anim.SetTrigger("Jump");
    }



void RotateToRandomRotation()
{
    // Pick a random rotation around the Y axis
    float targetYRotation = Random.Range(0f, 360f);
    targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);

    isRotating = true; // Set rotation flag
}

    // void RotateToRandomRotation()
    // {
    //     // Pick a random rotation around the Y axis
    //     float targetYRotation = Random.Range(0f, 360f);
    //     targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);

    //     isRotating = true; // Set rotation flag
    // }
}
