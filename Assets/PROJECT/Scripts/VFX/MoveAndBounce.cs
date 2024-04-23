using UnityEngine;

public class MoveAndBounce : MonoBehaviour
{
    public float wallDetectionRange;
    public float moveSpeed;
    public LayerMask walls;
    public bool startMoving;

    Vector3 directionToMoveTo;
    Rigidbody RB;
    bool useRigidBody;
    



    void Start()
    {
        transform.Rotate(new Vector3(0,Random.Range(30,300),0));
        directionToMoveTo = transform.forward;
        RB = GetComponent<Rigidbody>();
        if (RB != null) useRigidBody = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(startMoving){

            CheckWall();
            if(!useRigidBody) transform.position += directionToMoveTo * moveSpeed * Time.deltaTime;
            else RB.velocity += directionToMoveTo * moveSpeed * Time.deltaTime;
        }
    }

    void CheckWall(){
        RaycastHit hit;
        Ray ray = new Ray(transform.position, directionToMoveTo);
        Debug.DrawRay(transform.position, directionToMoveTo * wallDetectionRange, Color.red);
        if(Physics.Raycast(ray,out hit, wallDetectionRange,walls))
        {
            // Debug.Log("a wall has been encountered, Evasive Manuvers Activate");
            directionToMoveTo = Vector3.Reflect(directionToMoveTo, hit.normal );
            directionToMoveTo = new Vector3(directionToMoveTo.x, 0,directionToMoveTo.z).normalized;
        }
    }
}
