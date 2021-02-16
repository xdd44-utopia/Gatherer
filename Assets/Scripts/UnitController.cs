using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

	private const float moveSpeed = 0.01f;
	private const float followSpeed = 5f;
	private const float gatherTime = 0.3f;
	private const float angleRange = 0.1f;
	private const float maxGatherDist = 3f;

	private Status status = Status.Free;
	private float camWidth;
	private float camHeight;

	private float moveAngle;
	private Vector2 startPos;
	private Vector2 targetPos;
	private Vector2 gatherTar;
	private float timer;

	// Start is called before the first frame update
	void Start() {
		moveAngle = Random.Range(0f, 2 * Mathf.PI);
		targetPos = transform.position;
		Camera cam = Camera.main;
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
	}

	// Update is called once per frame
	void Update() {
		switch (status) {
			case Status.Free:
				move();
				break;
			case Status.Gathering:
				gather();
				break;
			case Status.Spreading:
				spread();
				break;
		}
	}

	private void move() {
		targetPos = targetPos + new Vector2(Mathf.Cos(moveAngle) * moveSpeed, Mathf.Sin(moveAngle) * moveSpeed);
		if (targetPos.x < -camWidth) {
			targetPos.x = -camWidth;
			moveAngle = 0f;
		}
		if (targetPos.x > camWidth) {
			targetPos.x = camWidth;
			moveAngle = - Mathf.PI;
		}
		if (targetPos.y < -camHeight) {
			targetPos.y = -camHeight;
			moveAngle = Mathf.PI / 2f;
		}
		if (targetPos.y > camHeight) {
			targetPos.y = camHeight;
			moveAngle = - Mathf.PI / 2f;
		}
		if (Random.Range(0f, 50f) > 1f) {
			moveAngle += Random.Range(-angleRange, angleRange);
		}
		moveAngle += 4 * Mathf.PI;
		moveAngle = Mathf.Repeat(moveAngle, 2 * Mathf.PI);
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
	}

	private void gather() {
		transform.position = Vector2.Lerp(startPos, gatherTar, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / gatherTime - 0.5f))) );
		timer += Time.deltaTime;
		if (timer > gatherTime) {
			timer = 0f;
			status = Status.Spreading;
		}
	}

	private void spread() {
		transform.position = Vector2.Lerp(gatherTar, startPos, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / gatherTime - 0.5f))) );
		timer += Time.deltaTime;
		if (timer > gatherTime) {
			timer = 0f;
			status = Status.Free;
		}
	}

	public bool startGather(Vector3 tar) {
		Debug.Log("try gather " + tar + " " + transform.position);
		if (Vector3.Distance(tar, transform.position) < maxGatherDist) {
			timer = 0f;
			gatherTar = tar;
			startPos = transform.position;
			status = Status.Gathering;
			return true;
		}
		else {
			return false;
		}
	}

	public bool isFree() {
		return (status == Status.Free);
	}

	private enum Status {
		Free,
		Gathering,
		Spreading
	}
}
