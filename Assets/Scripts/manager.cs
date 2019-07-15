using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
	[HideInInspector]
	public myPlayerPref pPref;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TurnOff()
	{
		Application.Quit();
	}

	public void LoadSceneX(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("MenuPrincipal");
	}

	public void LoadVictory()
	{
		if (pPref.get_kind() == -1)
			SceneManager.LoadScene("AllDone");
		else
			SceneManager.LoadScene("Victory");
	}
}
