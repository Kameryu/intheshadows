using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElephant : MonoBehaviour
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
	bool	CtrlIsHold = false;
	Vector2	myVal;
	Vector3	mousePosition;

	// Start is called before the first frame update
	void Start()
	{
		lvl = myObject1.GetComponent<myPlayerPref>().get_level();
		test = myObject1.GetComponent<myPlayerPref>().get_kind();
		endLvl.SetActive(false);
		myObject1.transform.rotation = Quaternion.Euler(0, -45, 0);
		myVal.x = 0;
		myVal.y = -45;
	}

	// Update is called once per frame
	void Update()
	{
		if (isOk)
			return;

		while (myVal.x > 180 || myVal.x < -180)
			myVal.x += myVal.x < -180 ? 360 : -360;
		while (myVal.y > 180 || myVal.y < -180)
			myVal.y += myVal.y < -180 ? 360 : -360;

		if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
			CtrlIsHold = true;
		if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl))
			CtrlIsHold = false;

		if (CtrlIsHold)
			level1();
		else
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
			myVal.y += (Input.mousePosition.x - mousePosition.x) * Time.deltaTime;
			myObject1.transform.rotation = Quaternion.Euler(myVal.x, myVal.y, 0);
		}
	}

	void level1()
	{
		if (mouseIsHold)
		{
			myVal.x += (Input.mousePosition.y - mousePosition.y) * Time.deltaTime;
			myObject1.transform.rotation = Quaternion.Euler(myVal.x, myVal.y, 0);
		}
	}

	void checkOk()
	{
		Vector3 angle = myObject1.transform.eulerAngles;

		Vector2 angle2;

		angle2.x = angle.x + angle.z;
		angle2.y = angle.y + angle.z;

		while (angle2.x > 180 || angle2.x < -180)
			angle2.x += angle2.x > 180 ? -360 : 360;
		while (angle2.y > 180 || angle2.y < -180)
			angle2.y += angle2.y > 180 ? -360 : 360;

		float angle3;

		angle3 = angle2.y + 180;

		while (angle3 > 180 || angle3 < -180)
			angle3 += angle3 > 180 ? -360 : 360;

		if (Mathf.Abs(angle2.y - baseRot.y) < 3.5f)
		{
			if (Mathf.Abs(angle2.x - baseRot.x) < 5)
				affVictory();
		}
		else if (Mathf.Abs(angle3 - baseRot.y) < 3.5f)
			if (Mathf.Abs(angle2.x - baseRot.x) < 5)
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
		if (test != -1 && lvl == 1)
			myObject1.GetComponent<myPlayerPref>().save_data(2);
		StartCoroutine(tango());
	}
}
