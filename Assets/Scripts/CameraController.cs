using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private float camWidth;
	private float camHeight;
	private float limitWidth = 2f;
	private float limitHeight = 2f;

	private const float followSpeed = 2f;
	private const float scaleSpeed = 10f;
	private const float z = -10f;


	private Status status = Status.Friend;
	private Vector2 pos;
	private float size;
	// Start is called before the first frame update
	void Start()
	{
		camHeight = Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
	}

	// Update is called once per frame
	void Update()
	{
		switch (status) {
			case Status.Friend:
				freeMove();
				break;
		}
	}

	private void freeMove() {
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		Vector2 avg = new Vector2(0, 0);
		float leftMost = 10000;
		float rightMost = -10000;
		float upMost = -10000;
		float downMost = 10000;
		float width;
		float height;
		foreach (GameObject unit in units) {
			avg += new Vector2(unit.transform.position.x, unit.transform.position.y);
			leftMost = leftMost < unit.transform.position.x - transform.position.x ? leftMost : unit.transform.position.x - transform.position.x;
			rightMost = rightMost > unit.transform.position.x - transform.position.x ? rightMost : unit.transform.position.x - transform.position.x;
			upMost = upMost > unit.transform.position.y - transform.position.y ? upMost : unit.transform.position.y - transform.position.y;
			downMost = downMost < unit.transform.position.y - transform.position.y ? downMost : unit.transform.position.y - transform.position.y;
		}

		if (units.Length > 0) {
			avg /= units.Length;
			width = rightMost - leftMost;
			height = upMost - downMost;
			width /= 2;
			height /= 2;
			width = width < camWidth ? width : camWidth;
			height = height < camHeight ? height : camHeight;
			width = width > limitWidth ? width : limitWidth;
			height = height > limitHeight ? height : limitHeight;
			width /= Camera.main.aspect;
			height = height > width ? height : width;
			height *= 1.2f;
		}
		else {
			height = camHeight;
		}
		pos = avg;
		size = height;

		transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, z), Time.deltaTime * followSpeed);
		GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, size, Time.deltaTime * scaleSpeed);
	}

	private enum Status {
		Friend,
		All,
		Controlled
	}
}
