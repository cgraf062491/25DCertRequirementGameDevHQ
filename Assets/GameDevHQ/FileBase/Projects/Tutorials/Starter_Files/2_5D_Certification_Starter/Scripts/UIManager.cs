using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
    	get
    	{
    		if(_instance == null)
    		{
    			Debug.LogError("Instance is null");
    		}

    		return _instance;
    	}
    }

    private int _collectableCount;
    [SerializeField]private Text _collectableText;

    void Awake()
    {
    	_instance = this;
    	_collectableText.text = "Collectables: 0";
    }

    public void UpdateCollectables()
    {
    	_collectableCount += 1;
    	_collectableText.text = "Collectables: " + _collectableCount;
    }
}
