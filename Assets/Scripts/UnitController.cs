using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
	//掉血特效
	public GameObject bloodFX;
	public GameObject health;
	private SpriteRenderer spriteRenderer;

	private const float moveSpeed = 0.01f;
	private const float dragSpeed = 10f;
	private const float followSpeed = 1f;
	private const float gatherTime = 0.25f;
	private const float angleRange = 0.1f;
	private const float maxGatherDist = 2f;
	private const float cooldownTime = 2f;
	public bool isFrozenUnit = false;

	private Status status = Status.Free;
	private float camWidth;
	private float camHeight;

	private float moveAngle;
	private Vector2 startPos;
	private Vector2 targetPos;
	private Vector2 gatherTar;
	private float timer;
	private float cooldown = 0f;

	private Color unitColor;

	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		moveAngle = Random.Range(0f, 2 * Mathf.PI);
		targetPos = transform.position;
		Camera cam = Camera.main;
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		unitColor=GetComponent<SpriteRenderer>().color;
	}

	// Update is called once per frame
	void Update()
	{
		switch (status)
		{
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

		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
			unitColor.a = 0.25f;
		}
		else
		{
			unitColor.a = 1f;
		}
		spriteRenderer.color=unitColor;

	}

	private void move()
	{
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * (cooldown <= 0 ? followSpeed : followSpeed / 5f));
		if (Random.Range(0f, 50f) > 1f)
		{
			moveAngle += Random.Range(-angleRange, angleRange);
		}
		moveAngle += 4 * Mathf.PI;
		moveAngle = Mathf.Repeat(moveAngle, 2 * Mathf.PI);
		targetPos = targetPos + new Vector2(Mathf.Cos(moveAngle) * moveSpeed, Mathf.Sin(moveAngle) * moveSpeed);
		if (targetPos.x < -camWidth)
		{
			targetPos.x = -camWidth;
			moveAngle = 0f;
		}
		if (targetPos.x > camWidth)
		{
			targetPos.x = camWidth;
			moveAngle = -Mathf.PI;
		}
		if (targetPos.y < -camHeight)
		{
			targetPos.y = -camHeight;
			moveAngle = Mathf.PI / 2f;
		}
		if (targetPos.y > camHeight)
		{
			targetPos.y = camHeight;
			moveAngle = -Mathf.PI / 2f;
		}
	}

	private void gather()
	{
		transform.position = Vector2.Lerp(startPos, gatherTar, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / gatherTime - 0.5f))));
		timer += Time.deltaTime;
		if (timer > gatherTime)
		{
			timer = 0f;
			status = Status.Spreading;
			cooldown = cooldownTime;
		}
	}

	private void spread()
	{
		transform.position = Vector2.Lerp(gatherTar, startPos, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / gatherTime - 0.5f))));
		timer += Time.deltaTime;
		if (timer > gatherTime)
		{
			timer = 0f;
			status = Status.Free;
		}
	}

	public bool tryGather(Vector3 tar)
	{
		if (Vector3.Distance(tar, transform.position) < maxGatherDist && status == Status.Free && cooldown <= 0f)
		{
			float randAngle = Random.Range(0, Mathf.PI * 2);
			targetPos = Vector3.Lerp(targetPos, tar + new Vector3(Mathf.Cos(randAngle) * maxGatherDist, Mathf.Sin(randAngle) * maxGatherDist, 0), Time.deltaTime * dragSpeed);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool startGather(Vector3 tar)
	{
		if (tryGather(tar))
		{
			timer = 0f;
			gatherTar = tar;
			startPos = transform.position;
			status = Status.Gathering;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void getDamaged(float damage)
	{
		health.GetComponent<HealthbarController>().getDamaged(damage);
		DamageEffect();
	}

	public float getGatherTime()
	{
		return gatherTime;
	}

	private enum Status
	{
		Free,
		Gathering,
		Spreading
	}

	//所有受伤后的效果
	private void DamageEffect()
	{
		//掉血特效
		Instantiate(bloodFX, transform.position, Quaternion.identity);
		FindObjectOfType<CameraShake>().Shake();
	}
}