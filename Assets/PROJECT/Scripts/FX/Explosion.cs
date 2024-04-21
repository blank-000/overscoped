
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float explosiveForce = 50;
    public float UpwardMultiplier = 2;
    TopDownMovement movement;
    PlaySounds sound;
    public LayerMask AffectedByExplosions;
    public GameObject Fire;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
    }
    // I need a cached reference to movement here
    void Awake()
    {
        sound = GetComponent<PlaySounds>();
        movement = FindFirstObjectByType<TopDownMovement>();
    }

    void OnEnable()
    {
        sound.PlaySpecial();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius,AffectedByExplosions);

        foreach (Collider hit in colliders)
        {
            Instantiate(Fire, hit.transform);
            Rigidbody rb = hit.attachedRigidbody;
            // if(rb = null) rb = hit.GetComponentInParent<Rigidbody>();
           
            if (hit.GetComponent<TopDownMovement>() != null)
            {   
                movement = hit.GetComponent<TopDownMovement>();
                movement.DisableControls();
            }


            if (rb != null)
            {
                Vector3 explosiveDirection =  rb.transform.position - transform.position;
                rb.AddForce(Vector3.up * explosiveForce * UpwardMultiplier, ForceMode.Impulse);
                rb.AddForce(explosiveDirection.normalized * explosiveForce, ForceMode.Impulse);
                
            }
        }
    }

    void OnDisable()
    {
        
        movement.EnableControls();
    }
}