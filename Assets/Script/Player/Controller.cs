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

    ObjectCollection _objects;
    [SerializeField] Player_Stats _playerStats;
    ShakeController _shakeController;
    Rigidbody2D _rigidbody2D;
    Animator _anim;

    //MOVE
    [Header ("Move")]
    [SerializeField]float _speed;
    float x;
    //JUMP
    [Header("jump")]
    [SerializeField] Transform _ground;
    [SerializeField]float _jumpForce;
     LayerMask _layer;
    [SerializeField] Vector2 colliderDistance;
    //POSITIONS
    [Header("Position")]
    [SerializeField] List<Transform> _positions = new List<Transform>();
    [SerializeField] GameObject _fade;
    int _indexpos=0;

    [Header("Audios")]
    [SerializeField] FootStepSound _stepSound;
    [SerializeField] AudioSource _jumpSound;
    //Dimension 4 directions
    bool _permiteMove =true;

    [Header("Estados")]
    public States _states = States.Default;

    private void Awake() => InitPlayer();

    private void Start()
    {
        _objects = GetComponent<ObjectCollection>();
        _shakeController = GetComponent<ShakeController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
 
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
            case States.LowGravity:
                _rigidbody2D.gravityScale = 2;
                Jump();
                break;
            case States.ReverseGravity:
                Gravity();
                break;
            case States.Doors:
                
                break;

            case States.Stop:
                _rigidbody2D.gravityScale = 0;
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
  

            case States.Stop:
                _rigidbody2D.velocity = Vector2.zero;
                break;
            default:
                 Movement();
                break;
        }
        
    }

    void Movement()
    {
         x = Input.GetAxisRaw("Horizontal") * _speed;
        //transform.position += new Vector3(x, 0);
        _rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
        PlayerDirection();
        if (IsOnFloor())
        {
            _anim.SetBool("walk", x != 0);
            if(x != 0)
            {
                _stepSound.PlayFootstepSound();
            }
        }
    }
    void Eje()
    {
         x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        transform.position += new Vector3(-x, 0);
        PlayerDirection();
    }
    void MovementDontStop() => transform.position += _speed * Time.deltaTime * Vector3.right;
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor())
        {
            _jumpSound.Play();
            _rigidbody2D.velocity= new Vector2(_rigidbody2D.velocity.x,_jumpForce);
        }
    }
    bool IsOnFloor()
    {
        bool raycast = Physics2D.OverlapCapsule(_ground.position, colliderDistance, CapsuleDirection2D.Horizontal,0, _layer);

        return raycast;
    }

    void Gravity() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpSound.Play();
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
        _fade.SetActive(true);
        States[] states = (States[])Enum.GetValues(typeof(States));

        int currentIndex = Array.IndexOf(states, _states);

        int nextIndex = (currentIndex + 1) % states.Length;

        
        _states = States.Stop;
        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(1f);
        transform.position = _positions[_indexpos].position;
        _indexpos++;
        _states = states[nextIndex];
        _rigidbody2D.gravityScale = 5;
        _objects.setPieces(0);
    }

    
    public void RestartState() => _states = States.Default;

    public void setIndexPos(int pos)
    {
        _indexpos = pos;
    }

    void PlayerDirection()
    {
        if (x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
    void InitPlayer()
    {
        _speed = _playerStats._speed;
        _jumpForce = _playerStats._jumpForce;
        _layer = _playerStats._layer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            StartCoroutine(ChangeDimension(2f));
        }
    }

}
