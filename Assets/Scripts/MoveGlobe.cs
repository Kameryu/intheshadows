using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGlobe : MonoBehaviour
{
	public GameObject	myObject1;
	public GameObject	myObject2;
	public GameObject	goodSet;
	public Vector3		baseRot;

	[HideInInspector]
	public bool			isOk = false;
	[HideInInspector]
	public bool			isActiv = false;


	int		test;
	bool	mouseIsHold = false;
	bool	CtrlIsHold = false;
	bool	ShiftIsHold = false;
	bool	CmdIsHold = false;
	bool	ended = false;
	Vector2	myVal;
	Vector3	mousePosition;

	// Start is called before the first frame update
	void Start()
	{
		test = myObject1.GetComponent<myPlayerPref>().get_kind();
		myObject1.transform.rotation = Quaternion.Euler(75, -50, 0);
		myVal.x = 75;
		myVal.y = -50;
		myObject1.transform.position = new Vector3(-3, 4, 0);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 pos1 = myObject1.transform.position;
		Vector3 pos2 = myObject2.transform.position;
		if (ended)
			return;

		bool ok2 = myObject2.GetComponent<MoveBase>().isOk;
		if (isOk && ok2 && Mathf.Abs(pos2.y - pos1.y) < 0.45f && Mathf.Abs(pos2.x - pos1.x) < 0.45f)
			affVictory();

		while (myVal.x > 180 || myVal.x < -180)
			myVal.x += myVal.x > 180 ? -360 : 360;
		while (myVal.y > 180 || myVal.y < -180)
			myVal.y += myVal.y > 180 ? -360 : 360;

		if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
		{
			CtrlIsHold = true;
			ShiftIsHold = false;
			CmdIsHold = false;
		}
		if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl))
			CtrlIsHold = false;

		if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
		{
			ShiftIsHold = true;
			CtrlIsHold = false;
			CmdIsHold = false;
		}
		if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
			ShiftIsHold = false;

		if (Input.GetKeyDown(KeyCode.RightCommand) || Input.GetKeyDown(KeyCode.LeftCommand))
		{
			CmdIsHold = true;
			CtrlIsHold = false;
			ShiftIsHold = false;
		}
		if (Input.GetKeyUp(KeyCode.RightCommand) || Input.GetKeyUp(KeyCode.LeftCommand))
			CmdIsHold = false;

		if (CmdIsHold)
			level3();
		else if (ShiftIsHold)
			level2();
		else if (CtrlIsHold)
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

	void level2()
	{
		Transform trf = myObject1.transform;
		if (mouseIsHold)
		{
			myObject1.transform.position = new Vector3(trf.position.x, trf.position.y + (Input.mousePosition.y - mousePosition.y) * Time.deltaTime, trf.position.z);
			mousePosition = Input.mousePosition;
		}
	}

	void level3()
	{
		Transform trf = myObject1.transform;
		if (mouseIsHold)
		{
			myObject1.transform.position = new Vector3(trf.position.x + (Input.mousePosition.x - mousePosition.x) * Time.deltaTime, trf.position.y, trf.position.z);
			mousePosition = Input.mousePosition;
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

		Vector2 angle3;

		angle3.x = angle2.x + 180;
		angle3.y = angle2.y + 180;

		while (angle3.x > 180 || angle3.x < -180)
			angle3.x += angle3.x > 180 ? -360 : 360;
		while (angle3.y > 180 || angle3.y < -180)
			angle3.y += angle3.y > 180 ? -360 : 360;

		if (Mathf.Abs(angle2.y - baseRot.y) < 3.5f)
			if (Mathf.Abs(angle2.x - baseRot.x) < 3.5f)
				isOk = true;
			else if (Mathf.Abs(angle3.x - baseRot.x) < 3.5f)
				isOk = true;
			else
				isOk = false;
		else if (Mathf.Abs(angle3.y - baseRot.y) < 3.5f)
			if (Mathf.Abs(angle2.x - baseRot.x) < 3.5f)
				isOk = true;
			else if (Mathf.Abs(angle3.x - baseRot.x) < 3.5f)
				isOk = true;
			else
				isOk = false;
		else
			isOk = false;
		if (isOk)
			StartCoroutine(tango());
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
		ended = true;
	}
}
