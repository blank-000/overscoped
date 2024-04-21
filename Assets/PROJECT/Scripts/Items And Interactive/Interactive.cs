using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] Color interactionEnabledColor;
    public Transform slot;
    MeshRenderer rend;

    PreparationStation station;

    Item currentItem;
    public bool canInteract;
    
    Material _mat;
    Vector4 normalEmmisiveCol;

    bool IsAnimating;
    float timeToAnimationComplete = 1f;
    float timer;
    float normalVis;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToAnimationComplete;
        station = GetComponent<PreparationStation>();
        rend = GetComponent<MeshRenderer>();
        
        _mat = rend.material;
        normalEmmisiveCol = _mat.GetVector("_EmissionColor");
        if(this.gameObject.CompareTag("Circle")) normalVis = _mat.GetFloat("_Visibility");
        
    }
    void Update()
    {
        if (this.gameObject.CompareTag("Circle") && currentItem != null)
        {
            if(IsAnimating)
            {
                _mat.SetInt("_RotateOn", 1);
                timer -= Time.deltaTime;
                if(timer > 0)
                {
                    float elapsed = timer/timeToAnimationComplete;
                    float t = 1 - elapsed;

                    _mat.SetFloat("_UpOffset", t * 0.6f);
                    _mat.SetFloat("_Visibility", t);
                    _mat.SetVector("_EmissionColor", normalEmmisiveCol  * t );

                } else {
                    IsAnimating = false;
                    timer = timeToAnimationComplete;
                    
                }
            }
            // _mat.SetFloat("_Visibility", 1f);
            
            
            // _mat.SetVector("_EmissionColor", normalEmmisiveCol * 3f);
        } else if(!canInteract){ 
            _mat.SetFloat("_Visibility", 0.5f);
            _mat.SetVector("_EmissionColor", normalEmmisiveCol );
            _mat.SetFloat("_UpOffset", 0f);
            _mat.SetInt("_RotateOn", 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if(this.gameObject.CompareTag("Circle")){
                _mat.SetFloat("_Visibility", 1f);
                
                _mat.SetVector("_EmissionColor", normalEmmisiveCol * 3f );
            } else {
                _mat.SetVector("_EmissionColor", normalEmmisiveCol * 2f );
            }
           // rend.material.color = interactionEnabledColor;
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(this.gameObject.CompareTag("Circle")){
                _mat.SetFloat("_Visibility", normalVis);
        }
        if(currentItem == null) _mat.SetVector("_EmissionColor", normalEmmisiveCol );
       // rend.material.color = color;
        canInteract = false;
        
    }

    public virtual Item GetItem()
    {
        if(!canInteract) return null;
        Item _item = currentItem;
        currentItem = null;
        return _item;
        
    }

    public void PrepareItem()
    {
        if(station != null)
        {
            station.ProccessItem(currentItem);
        }
    }

    public virtual bool PlaceItem(Item item)
    {
        if(!canInteract) return false;
        if(currentItem != null) return false;
        item.PlaceItem(slot);
        currentItem = item;
        IsAnimating = true;
        return true;
    }

}
