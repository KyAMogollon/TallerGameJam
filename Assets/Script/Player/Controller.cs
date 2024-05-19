using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
   public enum States
    {
        Default,
        LowGravity,
        PlayerAxis,
        MoveDontStop,
        ReverseGravity,
        Doors,
        Gravity4Directions,
        Stop
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
    //POSITIONS
    [SerializeField] List<Transform> _positions = new List<Transform>();

    //Dimension 4 directions
    bool _permiteMove=true;
    

    public States _states = States.Default;

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

            case States.Stop:
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

            case States.Stop:
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
    
   /* void Corrutine4Directions()
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
    }*/

    IEnumerator ChangeDimension(float time)
    {
        
        States[] states = (States[])Enum.GetValues(typeof(States));

        int currentIndex = Array.IndexOf(states, _states);

        int nextIndex = (currentIndex + 1) % states.Length;

        
        _states = States.Stop;
        yield return new WaitForSeconds(time);
        _states = states[nextIndex];

    }

    
    public void RestartState() => _states = States.Default;
    void InitPlayer()
    {
        _speed = _playerStats._speed;
        _jumpForce = _playerStats._jumpForce;
        _layer = _playerStats._layer;
    }

}
