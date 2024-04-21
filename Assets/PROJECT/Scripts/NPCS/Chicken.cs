using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    public float maxSpeed;
    

    public LayerMask sceneCollision;
    float timeToJump;
    public float maxDistanceToWall;
    
    Rigidbody RB;
    public float rotationSpeed = 1.0f; // Speed of rotation
    private Quaternion targetRotation; // Target rotation
    private bool isRotating = false; // Flag to check if rotation is in progress


    float current = 0, target = 1;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();

        RotateToRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        


              
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

        Move();
       

    }



    void Move()
    {
        RB.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        if (RB.velocity.magnitude > maxSpeed)
        {
            RB.velocity = RB.velocity.normalized * maxSpeed;
        }
    }




    void RotateToRandomRotation()
    {
         Debug.DrawRay(transform.position, transform.forward * maxDistanceToWall, Color.red);

        // Check for collisions with walls
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistanceToWall, sceneCollision))
        {
           
            // If there's a wall ahead, rotate away from it
            Vector3 awayFromWallDirection = Vector3.Reflect(transform.forward, hit.normal);
            targetRotation = Quaternion.LookRotation(awayFromWallDirection, Vector3.up);
        }
        else
        {
            // If no wall ahead, pick a random rotation
            float targetYRotation = Random.Range(0f, 360f);
            targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);
        }

        isRotating = true; // Set rotation flag
    }
}
