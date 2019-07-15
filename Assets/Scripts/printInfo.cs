using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printInfo : MonoBehaviour
{
	public GameObject	that;
	public GameObject	other;
	public bool			mainCam;
	// public GameObject	pPref;

	// Use this for initialization
	void Start ()
	{
		if (!mainCam)
			that.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			other.SetActive(true);
			that.SetActive(false);
		}
	}
}
