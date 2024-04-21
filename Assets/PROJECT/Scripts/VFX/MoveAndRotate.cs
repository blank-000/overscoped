using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour
{
    public float rightDetectionRayOffset;
    public float wallDetectionRange;
    public float moveSpeed;
    public LayerMask walls;
    public bool startMoving;

    Vector3 directionToMoveTo;
    Rigidbody RB;
    bool useRigidBody;
    Golem golem;
    



    void Start()
    {
        transform.Rotate(new Vector3(0,Random.Range(30,300),0));
        directionToMoveTo = transform.forward;
        RB = GetComponent<Rigidbody>();
        golem = GetComponent<Golem>();
    }
    // Update is called once per frame
    void Update()
    {
        if(startMoving){

            CheckWall();
            if(golem.SmashTarget != null)
            {
                directionToMoveTo = (golem.SmashTarget.transform.position - transform.position).normalized;
            } 

            RB.velocity += directionToMoveTo * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(directionToMoveTo);

            

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(castPosition + directionToMoveTo,wallDetectionRange);
    }

    Vector3 castPosition;
    void CheckWall(){
        RaycastHit hit;
        castPosition = transform.position + Vector3.up * 2;
        Ray ray = new Ray(castPosition, directionToMoveTo);
        Ray leftRay  = new Ray(castPosition - transform.right * rightDetectionRayOffset, directionToMoveTo);
        Ray rightRay = new Ray(castPosition + transform.right * rightDetectionRayOffset, directionToMoveTo);

        
        Debug.DrawRay(castPosition, directionToMoveTo * wallDetectionRange, Color.red);
        Debug.DrawRay(castPosition - transform.right * rightDetectionRayOffset, directionToMoveTo * wallDetectionRange, Color.red);
        Debug.DrawRay(castPosition + transform.right * rightDetectionRayOffset, directionToMoveTo * wallDetectionRange, Color.red);

        // if(Physics.Raycast(ray,out hit, wallDetectionRange,walls) ||Physics.Raycast(leftRay,out hit, wallDetectionRange,walls) || Physics.Raycast(rightRay,out hit, wallDetectionRange,walls))
        if(Physics.SphereCast(ray, wallDetectionRange, out hit, wallDetectionRange, walls))
        {
            Debug.Log("a wall has been encountered, Evasive Manuvers Activate");
            directionToMoveTo = Vector3.Reflect(directionToMoveTo, hit.normal );
            directionToMoveTo = new Vector3(directionToMoveTo.x, 0,directionToMoveTo.z).normalized;
        }
    }
}

    

