using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;

	private const float moveSpeed = 0.01f;
	private const float dragSpeed = 2.5f;
	private const float followSpeed = 1f;
	private const float gatherTime = 0.3f;
	private const float angleRange = 0.1f;
	private const float maxGatherDist = 2f;
	private const float cooldownTime = 2f;
	private const float randomRange = 0f;

	private Status status = Status.Free;
	private float camWidth;
	private float camHeight;

	private float moveAngle;
	private Vector2 startPos;
	public Vector2 targetPos;
	private Vector2 gatherTar;
	private float timer;
	private float cooldown = 0f;

	// Start is called before the first frame update
	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
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

		if (cooldown > 0) {
			cooldown -= Time.deltaTime;
			spriteRenderer.color = new Color(0, 1f, 0, 0.25f);
		}
		else {
			spriteRenderer.color = new Color(0, 1f, 0, 1f);
		}
	}

	private void move() {
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * (cooldown <= 0 ? followSpeed : followSpeed / 5f));
		if (Random.Range(0f, 50f) > 1f) {
			moveAngle += Random.Range(-angleRange, angleRange);
		}
		moveAngle += 4 * Mathf.PI;
		moveAngle = Mathf.Repeat(moveAngle, 2 * Mathf.PI);
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
	}

	private void gather() {
		transform.position = Vector2.Lerp(startPos, gatherTar, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / gatherTime - 0.5f))) );
		timer += Time.deltaTime;
		if (timer > gatherTime) {
			timer = 0f;
			status = Status.Spreading;
			cooldown = cooldownTime;
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

	public bool tryGather(Vector3 tar) {
		if (Vector3.Distance(tar, transform.position) < maxGatherDist && status == Status.Free && cooldown <= 0f) {
			Vector2 tar2 = new Vector2(tar.x, tar.y) + new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange));
			targetPos = Vector2.Lerp(targetPos, tar2, Time.deltaTime * dragSpeed);
			Debug.Log(targetPos);
			return true;
		}
		else {
			return false;
		}
	}

	public bool startGather(Vector3 tar) {
		if (tryGather(tar)) {
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

	private enum Status {
		Free,
		Gathering,
		Spreading
	}
}
