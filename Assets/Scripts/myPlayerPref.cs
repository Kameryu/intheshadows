using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myPlayerPref : MonoBehaviour
{

	[HideInInspector]
	public static int level = 0;

	// Use this for initialization
	void Start ()
	{
		level = PlayerPrefs.GetInt("level", 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void delete_pref()
	{
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("MenuPrincipal");
	}

	public int get_level()
	{
		return(level);
	}

	public int get_kind()
	{
		return(PlayerPrefs.GetInt("kind", 0));
	}

	public void set_kind(int test)
	{
		if (test == -1)
			PlayerPrefs.SetInt("kind", -1);
		else if (test == level)
			PlayerPrefs.SetInt("kind", test);
		else
			PlayerPrefs.SetInt("kind", level);
		savePrefs();
	}

	public void save_data(int lvl)
	{
		level = lvl;
		PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetInt("kind", level);
		savePrefs();
	}

	void savePrefs()
	{
		PlayerPrefs.Save();
	}
}
