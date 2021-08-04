using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
    	if(other.CompareTag("Player"))
    	{
    		UIManager.Instance.UpdateCollectables();
    		Destroy(this.gameObject);
    	}
    }
}
