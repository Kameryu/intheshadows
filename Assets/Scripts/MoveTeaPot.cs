using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeaPot : MonoBehaviour
{
	public GameObject	myObject1;
	public GameObject	endLvl;
	public GameObject	goodSet;
	public Vector3		baseRot;

	[HideInInspector]
	public bool			isOk = false;
	[HideInInspector]
	public bool			isActiv = false;


	int		lvl;
	int		test;
	bool	mouseIsHold = false;
	float	myVal;
	Vector3	mousePosition;

	// Start is called before the first frame update
	void Start()
	{
		lvl = myObject1.GetComponent<myPlayerPref>().get_level();
		test = myObject1.GetComponent<myPlayerPref>().get_kind();
		endLvl.SetActive(false);
		myObject1.transform.rotation = Quaternion.Euler(0, 135, 0);
		myVal = 135;
	}

	// Update is called once per frame
	void Update()
	{
		if (isOk)
			return;

		while (myVal > 180 || myVal < -180)
			myVal += myVal < -180 ? 360 : -360;

		level0();
	}

	void OnMouseDown()
	{
		mouseIsHold = true;
		isActiv = true;
		mousePosition = Input.mousePosition;
		myObject1.GetComponent<writeMyData>().toWrite = (test == -1);
	}

	void OnMouseUp()
	{
		mouseIsHold = false;
		isActiv = false;
		myObject1.GetComponent<writeMyData>().toWrite = false;
		checkOk();
	}

	void level0()
	{
		if (mouseIsHold)
		{
			myVal += (Input.mousePosition.x - mousePosition.x) * Time.deltaTime;
			myObject1.transform.rotation = Quaternion.Euler(0, myVal, 0);
		}
	}

	void checkOk()
	{
		Vector3 angle = myObject1.transform.eulerAngles;

		float angle2;

		angle2 = angle.y + angle.z;

		while (angle2 > 180 || angle2 < -180)
			angle2 += angle2 > 180 ? -360 : 360;

		float angle3;

		angle3 = angle2 + 180;

		while (angle3 > 180 || angle3 < -180)
			angle3 += angle3 > 180 ? -360 : 360;

		if (Mathf.Abs(angle2 - baseRot.y) < 3.5f)
			affVictory();
		else if (Mathf.Abs(angle3 - baseRot.y) < 3.5f)
			affVictory();
	}

	IEnumerator tango()
	{
		AudioSource audio = goodSet.GetComponent<AudioSource>();
		audio.Play();
		yield return new WaitForSeconds(1);
		audio.Stop();
	}

	void affVictory()
	{
		isOk = true;
		endLvl.SetActive(true);
		if (test != -1 && lvl == 0)
			myObject1.GetComponent<myPlayerPref>().save_data(1);
		StartCoroutine(tango());
	}
}
