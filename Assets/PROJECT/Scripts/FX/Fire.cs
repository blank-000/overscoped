
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject FirePrefab;


    void Update()
    {
        if(this.transform.parent.CompareTag("Ice"))
        {
            
            Destroy(this.transform.parent.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
    // if(other.gameObject.CompareTag("Ice")){
    //     Debug.Log("melting the ice");
    //     Destroy(other.gameObject);
        
    // }
        if(other.GetComponentInChildren<Fire>()) return;

        GameObject obj = Instantiate(FirePrefab, other.transform);
        obj.GetComponent<Fire>().InitializeShaderSettings();
    }   

    public void InitializeShaderSettings()
    {
        transform.localScale = new Vector3(Random.Range(.9f,1.6f),Random.Range(.9f,1.2f),Random.Range(.9f,1.6f));

        foreach(Transform c in transform)
        {   
            foreach(Transform t in c)
            {
                
                MeshRenderer rend = t.GetComponent<MeshRenderer>();

                if(rend != null)
                {   
                   
                    Material mat = rend.material;
                    mat.SetFloat("_scaleA", Random.Range(4f, 5.5f));
                    mat.SetFloat("_scaleB", Random.Range(1f, 2f));
                    mat.SetFloat("_speed", Random.Range(-.8f,-1.3f));
                } 

            }
        }
    }
}
