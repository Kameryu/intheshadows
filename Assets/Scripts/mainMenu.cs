using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	public GameObject	that;
	public bool			exit;
	public bool			test;
	public bool			credit;
	public bool			delPref;

	Color				atStart = Color.red;
	Color				whenClick = Color.blue;

	// Use this for initialization
	void Start () {
		that.GetComponentInChildren<Text>().color = atStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		that.GetComponentInChildren<Text>().color = whenClick;
		if (exit)
			Application.Quit();
		else if (delPref)
			that.GetComponent<myPlayerPref>().delete_pref();
		else
			setScene();
	}

	void OnMouseUp()
	{
		that.GetComponentInChildren<Text>().color = atStart;
	}

	void setScene()
	{
		if (credit)
			SceneManager.LoadScene("Credits");
		else if (test)
		{
			that.GetComponent<myPlayerPref>().set_kind(-1);
			SceneManager.LoadScene("AllDone");
		}
		else
		{
			that.GetComponent<myPlayerPref>().set_kind(that.GetComponent<myPlayerPref>().get_level());
			SceneManager.LoadScene("Select");
		}
	}
}
