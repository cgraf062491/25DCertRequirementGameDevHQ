using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed = 9.0f;
	[SerializeField] private float _gravity = 23.0f;
	[SerializeField] private float _jumpHeight = 8.0f;
	private Vector3 _direction;
	private Vector3 _yVelocity;

	private CharacterController _controller;
    private Animator _anim;

    private bool _jumping = false;
    private bool _hanging = false;
    private bool _rolling = false;
    private bool _onLadder = false;
    private bool _nearLadder = false;

    private PlatformLedge _activeLedge;
    private Ladder _activeLadder;
    private float _activeLadderTop;
    private float _activeLadderBottom;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.E) && _hanging == true)
        {
            //Debug.Log("climbing");
            _anim.SetTrigger("ClimbUp");
        }

        if(Input.GetKeyDown(KeyCode.E) && _nearLadder == true && _onLadder == false)
        {
        	GetOnLadder();
        }
        else if(Input.GetKeyDown(KeyCode.E) && _nearLadder == true && _onLadder == true)
        {
        	GetOffLadder();
        }
    }

    void CalculateMovement()
    {
        if(_controller.isGrounded == true && _rolling == false)
        {
            if(_jumping == true)
            {
                _anim.SetBool("Jumping", false);
            }
            float z_move = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, z_move);
            _anim.SetFloat("Speed", Mathf.Abs(z_move));

            if(z_move != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                if(_direction.z > 0)
                {
                    facing.y = 0.0f;
                }
                else
                {
                	if(_onLadder == true)
                	{
                		facing.y = 0.0f;
                	}
                	else
                	{
                		facing.y = 180.0f;
                	}
                }

                transform.localEulerAngles = facing;
            }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", true);
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                _rolling = true;
                _anim.SetBool("Rolling", true);
            }
        }
        else if(_onLadder == true)
        {
        	float y_move = Input.GetAxisRaw("Vertical");
        	_direction = new Vector3(0, y_move, 0);
        	_anim.SetFloat("Speed", Mathf.Abs(y_move));

        	Vector3 ladderClamp = transform.position;
        	ladderClamp.y = Mathf.Clamp(ladderClamp.y, _activeLadderBottom, _activeLadderTop);
        	transform.position = ladderClamp;
        }

        if(_onLadder == false)
        {
        	_direction.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_direction * Time.deltaTime * _speed); 
    }

    public void LedgeGrab(Vector3 grab_pos, PlatformLedge current_ledge)
    {
       // _gravity = 0.0f;
        _controller.enabled = false;
        _anim.SetBool("LedgeGrab", true);
        _anim.SetFloat("Speed", 0.0f);
        _anim.SetBool("Jumping", false);
        _hanging = true;
        transform.position = grab_pos; 
        _activeLedge = current_ledge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("LedgeGrab", false);
        _controller.enabled = true;
    }

    public void NearLadder(Ladder current_ladder, float ladderTop, float ladderBottom)
    {
    	_nearLadder = true;
    	_activeLadder = current_ladder;
    	_activeLadderTop = ladderTop;
    	_activeLadderBottom = ladderBottom;
    	//Debug.Log("Ladder top: " + _activeLadderTop);
    }

    public void NotNearLadder()
    {
    	_nearLadder = false;
    }

    void GetOnLadder()
    {
    	_onLadder = true;
    	_anim.SetBool("OnLadder", true);
    	transform.position = _activeLadder.GetGrabPos();
    }

    void GetOffLadder()
    {
    	_onLadder = false;
    	_anim.SetBool("OnLadder", false);
    	transform.position = _activeLadder.GetStandPos();
    }


    public void RollComplete()
    {
        //Debug.Log("Rolling complete");
        _anim.SetBool("Rolling", false);
        _rolling = false;
    }
}
