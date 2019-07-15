using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class writeMyData : MonoBehaviour
{

	public GameObject myObj;
	public GameObject myText;

	[HideInInspector]
	public bool toWrite = false;

	// Use this for initialization
	void Start ()
	{
		string str = "";
		myText.GetComponent<Text>().text = str;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (toWrite)
		{
			string str = "";
			str += "X: " + myObj.transform.eulerAngles.x + " ; Y: " + myObj.transform.eulerAngles.y + " ; Z: " + myObj.transform.eulerAngles.z;
			myText.GetComponent<Text>().text = str;
		}
	}
}
