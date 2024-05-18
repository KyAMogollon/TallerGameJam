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
        Doors
    }

    [SerializeField] Player_Stats _playerStats;

    Rigidbody2D _rigidbody2D;

    //MOVE
    float _speed;
    Vector3 _moveDirection;
    //JUMP
    Vector2 _rayDirection;
    float _jumpForce;
     LayerMask _layer;
    //

    [SerializeField]States _states = States.Default;

    private void Awake() => initPlayer();

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rayDirection = Vector2.down;
        _moveDirection = Vector2.right;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) _states = States.Default;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _states = States.LowGravity;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _states = States.PlayerAxis;
        if (Input.GetKeyDown(KeyCode.Alpha4)) _states = States.MoveDontStop;
        if (Input.GetKeyDown(KeyCode.Alpha5)) _states = States.ReverseGravity;
        if (Input.GetKeyDown(KeyCode.Alpha6)) _states = States.Doors;

        switch(_states)
        {
            case States.ReverseGravity:
                Jump(true);
                break;
            case States.Doors:
                
                break;

            default:
                Jump(false);
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
            default:
                Movement();
                break;
        }
        
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        transform.position += new Vector3(x, 0);
    }
    void Eje()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        transform.position += new Vector3(-x, 0);
    }
    void MovementDontStop()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;

        if(x > 0) { _moveDirection = Vector2.right; }
        else if(x < 0) { _moveDirection = Vector2.left; }

        transform.position += _moveDirection * _speed * Time.deltaTime;
    }
    void Jump(bool _reverseGravity)
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor())
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
           if(_reverseGravity) Gravity();
        }
    }

    bool IsOnFloor()
    {
        bool raycast = Physics2D.Raycast(transform.position, _rayDirection, 0.6f, _layer);

        return raycast;
    }

    void Gravity() { _rigidbody2D.gravityScale *= -1;  _jumpForce *= -1; _rayDirection.y *= -1; }
    

    void initPlayer()
    {
        _speed = _playerStats._speed;
        _jumpForce = _playerStats._jumpForce;
        _layer = _playerStats._layer;
    }

   
}
