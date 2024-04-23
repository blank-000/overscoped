using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDrag, deccelDrag;
    [SerializeField] float dashForce;
    [SerializeField] float dashCooldown;
    [SerializeField] float MaxSpeed;
    [SerializeField] float FallSpeed;
    [SerializeField] float dashParticlesDownOffset;
    [SerializeField] AudioSource dashSource;
    public float rayDistance;
    public LayerMask ground;

    Rigidbody RB;
    Vector3 _moveInput;

    float _drag;
    float _dashTimer;
    bool IsDashing;

    [SerializeField] GameObject dashParticles;
    Vector3 velocity;

    bool isStopped;
    public bool IsMoving {get; private set ;}
    PlaySounds sounds;

    float maxTimeWithDisabledControls = 3f;
    float controlReEnableTimer;
    bool controlsDisabled;
    
 
    public OnScreenStickCustom movementStick;
 

 
    public void OnStickMove(Vector2 direction)
    {
        _moveInput = new Vector3(direction.x, 0F, direction.y);
    }
 
    public void OnStickRelease()
    {
        _moveInput = Vector3.zero;
    }





    void Start()
    {
        movementStick.onJoystickMove.AddListener(OnStickMove);
        movementStick.onJoystickRelease.AddListener(OnStickRelease);
        sounds = GetComponent<PlaySounds>();
        RB = GetComponent<Rigidbody>();
        _dashTimer = dashCooldown;

    }

    public void StopMoving()
    {
        isStopped = true;
    }

    public void StartMoving()
    {
        isStopped = false;
    }
    
    public void DisableControls()
    {
        controlsDisabled =true;
        controlReEnableTimer = maxTimeWithDisabledControls;


    }
    
    public void EnableControls()
    {
        controlsDisabled = false;
        RB.AddForce(Vector3.down * FallSpeed, ForceMode.Impulse);
    }


    void Update()
    {
        IsMoving = _moveInput!=Vector3.zero ? false : true;
        
        _dashTimer -= Time.deltaTime;
        if(_dashTimer < 0)
        {
            IsDashing = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(RB.isKinematic) return;

        if(controlsDisabled) 
        {
            controlReEnableTimer -= Time.deltaTime;
            if(controlReEnableTimer < 0)
            {
                EnableControls();
                controlReEnableTimer = maxTimeWithDisabledControls;
            }
            return;
        }
        if(!CheckGround())
        {   
           
            RB.AddForce(Vector3.down * FallSpeed, ForceMode.Acceleration);
            MaxSpeed = 100;
        }  else {
            MaxSpeed = 12;
        }

        if(isStopped) 
        {
            RB.velocity = Vector3.zero;
            return;
        }
        
        RB.drag = _drag;
        
        if(IsDashing)
        {
            _drag = moveDrag;
            return;
        } 
        if(_moveInput!=Vector3.zero){
            _drag = moveDrag;  
            RB.AddForce(_moveInput * moveSpeed, ForceMode.Force);   
            // velocity = _moveInput * moveSpeed;
            // RB.velocity = velocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveInput), .2f);
            
        } else {
            _drag = deccelDrag;
            
        }

        ClampVelocity();
    }

    bool CheckGround()
    {
        return Physics.Raycast(transform.position, -transform.up, rayDistance, ground);
    }


    // movement methods
    void ClampVelocity(){
        Vector3 flatVelocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);

        if(flatVelocity.magnitude > MaxSpeed){
            flatVelocity = flatVelocity.normalized * MaxSpeed;
        }

        RB.velocity = new Vector3(flatVelocity.x, RB.velocity.y, flatVelocity.z);
    }


    void Dash(){
        // sounds.PlaySpecial();
        dashSource.Play();
        IsDashing = true;
        RB.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        GameObject particles = Instantiate(dashParticles, null);
        particles.transform.position = transform.position - Vector3.up * dashParticlesDownOffset;
        particles.transform.rotation = transform.rotation;

    }

    // Input
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 _input = ctx.ReadValue<Vector2>();
 
        
        _moveInput = new Vector3(_input.x, 0f, _input.y);
    }

    public void UIDash()
    {
        if(_dashTimer < 0){
            Dash();
            _dashTimer = dashCooldown; 
        }
    }
    public void OnDash(InputAction.CallbackContext ctx){
        if(ctx.performed && _dashTimer < 0){
            Dash();
            _dashTimer = dashCooldown;
            
        } 
        
    }
}
