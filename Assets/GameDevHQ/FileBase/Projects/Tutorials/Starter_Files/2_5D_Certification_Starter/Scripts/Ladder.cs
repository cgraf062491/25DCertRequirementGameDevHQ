using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
	[SerializeField] private Vector3 _grabPosition = new Vector3(0.5f, 67.5f, 123.5f);
	[SerializeField] private Vector3 _standPosition = new Vector3(0.5f, 74.3f, 125f);

	[SerializeField] private float _ladderTop;
	[SerializeField] private float _ladderBottom;




    void OnTriggerEnter(Collider other)
    {
    	if(other.CompareTag("Player"))
    	{
    		var player = other.GetComponent<Player>();

    		if(player != null)
    		{
    			player.NearLadder(this, _ladderTop, _ladderBottom);
    		}
    	}
    }

    void OnTriggerExit(Collider other)
    {
    	if(other.CompareTag("Player"))
    	{
    		var player = other.GetComponent<Player>();

    		if(player != null)
    		{
    			player.NotNearLadder();
    		}
    	}
    }

    public Vector3 GetStandPos()
    {
    	return _standPosition;
    }

    public Vector3 GetGrabPos()
    {
        return _grabPosition;
    }
}
