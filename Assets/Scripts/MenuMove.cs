using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMove : MonoBehaviour
{

	public GameObject	that;
	public GameObject	pPref;
	public int			level;
	public string		scene;

	int			sens;
	float		dir;
	Quaternion	rotation;
	Vector3		position;
	int l;
	int k;

	// Use this for initialization
	void Start ()
	{
		rotation = new Quaternion(that.transform.rotation.x, that.transform.rotation.y, that.transform.rotation.z, that.transform.rotation.w);
		position = new Vector3(that.transform.position.x, that.transform.position.y, that.transform.position.z);
		dir = 0.2f;
		sens = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		l = pPref.GetComponent<myPlayerPref>().get_level();
		k = pPref.GetComponent<myPlayerPref>().get_kind();

		if (k == -1)
			that.SetActive(true);
		else if (level < l)
			that.SetActive(true);
		else
			that.SetActive(false);

		switch (sens)
		{
			case 0:
				rotateZ();
				break;
			case 1:
				rotateX();
				break;
			case 2:
				translate1();
				break;
			case 3:
				translate2();
				break;
			default:
				sens = 0;
				break;
		}
	}

	void rotateZ()
	{
		that.transform.Rotate(Vector3.up * Time.deltaTime * 10 * (1 + level), Space.World);
		if ((rotation.y - that.transform.rotation.y) < 0.01f && (rotation.y - that.transform.rotation.y) > -0.01f)
			sens = (sens + 1) % (level + 1);
	}

	void rotateX()
	{
		that.transform.Rotate(Vector3.left * Time.deltaTime * 10 * (1 + level), Space.World);
		if ((rotation.x - that.transform.rotation.x) < 0.01f && (rotation.x - that.transform.rotation.x) > -0.01f)
			sens = (sens + 1) % (level + 1);
	}

	void translate1()
	{
		float y = that.transform.position.y;
		if (that.transform.position.y < position.y + 0.1 && that.transform.position.y > position.y - 0.1)
			that.transform.Translate(0, dir * Time.deltaTime, 0, Space.World);
		else
		{
			dir *= (-1);
			that.transform.Translate(0, dir * Time.deltaTime, 0, Space.World);
		}
		if (y < position.y && that.transform.position.y > position.y && dir == 0.2f)
			sens = (sens + 1) % (level + 1);
	}

	void translate2()
	{
		float y = that.transform.position.x;
		if (that.transform.position.x < position.x + 0.1 && that.transform.position.x > position.x - 0.1)
			that.transform.Translate(dir * Time.deltaTime, 0, 0, Space.World);
		else
		{
			dir *= (-1);
			that.transform.Translate(dir * Time.deltaTime, 0, 0, Space.World);
		}
		if (y < position.x && that.transform.position.x > position.x && dir == 0.2f)
			sens = (sens + 1) % (level + 1);
	}

	void OnMouseDown()
	{
		SceneManager.LoadScene(scene);
	}
}
