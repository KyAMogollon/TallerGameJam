using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    enum States
    {
        Default,
        LowGravity,
        PlayerAxis,
        MoveDontStop,
        ReverseGravity,
        Doors,
        Gravity4Directions
    }

    [SerializeField] Player_Stats _playerStats;
    ShakeController _shakeController;
    Rigidbody2D _rigidbody2D;

    //MOVE
    [SerializeField]float _speed;
    //JUMP
    [SerializeField] Transform _ground;
    [SerializeField]float _jumpForce;
     LayerMask _layer;

    //Dimension 4 directions
    bool _permiteMove=true;
    

    [SerializeField]States _states = States.Default;

    private void Awake() => InitPlayer();

    private void Start()
    {

        _shakeController = GetComponent<ShakeController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
 
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) _states = States.Default;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _states = States.LowGravity;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _states = States.PlayerAxis;
        if (Input.GetKeyDown(KeyCode.Alpha4)) _states = States.MoveDontStop;
        if (Input.GetKeyDown(KeyCode.Alpha5)) _states = States.ReverseGravity;
        if (Input.GetKeyDown(KeyCode.Alpha6)) _states = States.Doors;
        if (Input.GetKeyDown(KeyCode.Alpha7)) _states = States.Gravity4Directions;

        switch(_states)
        {
            case States.ReverseGravity:
                Gravity();
                break;
            case States.Doors:
                
                break;
            case States.Gravity4Directions:
                Jump();
                Corrutine4Directions();
                break;

            default:
                Jump();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_states)
        {
            case States.PlayerAxis:
                Eje();
                break;
            case States.MoveDontStop:
                MovementDontStop();
                break;
            case States.Gravity4Directions:
                if (_permiteMove) Movement();
                break;
            default:
                 Movement();
                break;
        }
        
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed;
        //transform.position += new Vector3(x, 0);
        _rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
    }
    void Eje()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        transform.position += new Vector3(-x, 0);
    }
    void MovementDontStop() => transform.position += _speed * Time.deltaTime * Vector3.right;
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor())
        {
            _rigidbody2D.velocity= new Vector2(_rigidbody2D.velocity.x,_jumpForce);
            
        }
    }
    bool IsOnFloor()
    {
        bool raycast = Physics2D.OverlapCapsule(_ground.position, new Vector2(0.92f, 0.08f), CapsuleDirection2D.Horizontal,0, _layer);

        return raycast;
    }

    void Gravity() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.gravityScale *= -1;
            _shakeController.Shake();
        }
    }
    
    void Corrutine4Directions()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { StopAllCoroutines(); StartCoroutine(Gravity4Direction(Vector2.up)); }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { StopAllCoroutines(); StartCoroutine(Gravity4Direction(Vector2.down)); }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        { StopAllCoroutines(); StartCoroutine(Gravity4Direction(Vector2.right)); }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { StopAllCoroutines(); StartCoroutine(Gravity4Direction(Vector2.left)); }
    }

    IEnumerator Gravity4Direction(Vector2 directions)
    {
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.velocity = Vector2.zero;
        _permiteMove = false;

        yield return new WaitForSeconds(1f);

        Physics2D.gravity = directions * 9.81f; ;
        _rigidbody2D.gravityScale = 5;
        _permiteMove = true;
    }

    void InitPlayer()
    {
        _speed = _playerStats._speed;
        _jumpForce = _playerStats._jumpForce;
        _layer = _playerStats._layer;
    }

   
}
