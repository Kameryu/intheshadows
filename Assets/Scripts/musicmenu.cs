using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmenu : MonoBehaviour
{
	private static musicmenu instance = null;
	public static musicmenu Instance
	{
		get
		{
			return instance;
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (instance != null && instance != this)
		{
			if (string.Equals(this.gameObject.ToString(), instance.gameObject.ToString()))
			{
				Destroy(this.gameObject);
				return;
			}
			else
			{
				Destroy(instance.gameObject);
				instance = this;
			}
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
