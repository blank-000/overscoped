
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float force;
    public float kickForce;
    public float playerforceMulti;

    public Transform throwOutPosition;
    float multiplier;

    void OnTriggerEnter(Collider other)
    {  
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<TopDownMovement>().DisableControls();
            multiplier = playerforceMulti;
        }



        
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody otherRB = other.attachedRigidbody;
        if(otherRB != null)
        {   
            Vector3 outPosition = throwOutPosition.transform.position;
            
            
            Vector3 forceDirection = ( outPosition - other.transform.position).normalized;
            
            

            // I should figure out how to invert this. so that ,it applies more force the closer it gets to the center, I figure that will add some swinging effect
            float flatDistanceToCenter = Vector3.Distance(new Vector3(other.transform.position.x, 0, other.transform.position.z), new Vector3(transform.position.x, 0f, transform.position.z));


            otherRB.AddForce(forceDirection * multiplier * force * flatDistanceToCenter ,ForceMode.Force);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            multiplier = 1;
            other.gameObject.GetComponent<TopDownMovement>().EnableControls();
        } 
        Rigidbody otherRB = other.attachedRigidbody;
        otherRB.AddForce(throwOutPosition.forward * kickForce, ForceMode.Impulse);
        
    }
}
