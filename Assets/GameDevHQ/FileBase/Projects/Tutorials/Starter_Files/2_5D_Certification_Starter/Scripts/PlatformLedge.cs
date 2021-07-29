using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLedge : MonoBehaviour
{
	[SerializeField] private Vector3 _grabPosition = new Vector3(0.5f, 67.5f, 123.5f);
	[SerializeField] private Vector3 _standPosition = new Vector3(0.5f, 74.3f, 125f);

    void OnTriggerEnter(Collider other)
    {
    	if(other.CompareTag("Player_Ledge_Checker"))
    	{
    		var player = other.transform.parent.gameObject.GetComponent<Player>();

    		if(player != null)
    		{
    			player.LedgeGrab(_grabPosition, this);
    		}
    	}
    }

    public Vector3 GetStandPos()
    {
    	return _standPosition;
    }
}
