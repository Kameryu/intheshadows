using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMove : MonoBehaviour
{

	public GameObject	cube;
	public GameObject	pPref;
	public string		scene;
	public int			lvl;

	Vector3		position;
	float		direction;
	int			l;

	// Use this for initialization
	void Start ()
	{
		l = pPref.GetComponent<myPlayerPref>().get_level();
		if (l > lvl)
			cube.SetActive(false);
		position = new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z);
		direction = 0.2f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		l = pPref.GetComponent<myPlayerPref>().get_level();
		if (l > lvl)
			cube.SetActive(false);

		if (lvl == l)
		{
			if (cube.transform.position.y < position.y + 0.1 && cube.transform.position.y > position.y - 0.1)
				cube.transform.Translate(0, direction * Time.deltaTime, 0);
			else
			{
				direction *= (-1);
				cube.transform.Translate(0, direction * Time.deltaTime, 0);
			}
		}
	}

	void OnMouseDown()
	{
		if (l == lvl)
			SceneManager.LoadScene(scene);
	}
}
