using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLift : MonoBehaviour
{
	[SerializeField] private Transform _targetA;
	[SerializeField] private Transform _targetB;
	[SerializeField] private float _speed = 3.0f;

	private bool _goingUp = true;
	private bool _goingDown = false;
	private bool _stopTime = false;

    // Start is called before the first frame update
    void Start()
    {
        _targetA.position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_goingDown == true && _stopTime == false)
    	{
    		transform.position = Vector3.MoveTowards(transform.position, _targetA.position, Time.deltaTime * _speed);
    		if(transform.position == _targetA.position)
    		{
    			_stopTime = true;
    			StartCoroutine(ElevatorWait());
    		}
    	}

    	if(_goingUp == true && _stopTime == false)
    	{
    		transform.position = Vector3.MoveTowards(transform.position, _targetB.position, Time.deltaTime * _speed);
    		if(transform.position == _targetB.position)
    		{
    			_stopTime = true;
    			StartCoroutine(ElevatorWait());
    		}
    	}
    }

    IEnumerator ElevatorWait()
    {
    	//Debug.Log("In coroutine");
    	yield return new WaitForSeconds(5.0f);
    	if(_goingDown == true)
    	{
    		_goingDown = false;
    		_goingUp = true;
    		_stopTime = false;
    	}
    	else if(_goingUp == true)
    	{
    		_goingUp = false;
    		_goingDown = true;
    		_stopTime = false;
    	}
    }
}
