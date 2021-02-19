using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
	//掉血特效
	public GameObject bloodFX;
	public GameObject health;
	private SpriteRenderer spriteRenderer;

    public bool isFrozenUnit = false;
	private Unit_Status unit_Status;
	private Status status = Status.Free;
	private GameObject cam;
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
		unit_Status = gameObject.GetComponent<Unit_Status>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		moveAngle = Random.Range(0f, 2 * Mathf.PI);
		targetPos = transform.position;
		cam = Camera.main.gameObject;
		camHeight = cam.GetComponent<Camera>().orthographicSize;
		camWidth = camHeight * cam.GetComponent<Camera>().aspect;
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
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * (cooldown <= 0 ? unit_Status.followSpeed : unit_Status.followSpeed / 5f));
		if (Random.Range(0f, 50f) > 1f)
		{
			moveAngle += Random.Range(-unit_Status.angleRange, unit_Status.angleRange);
		}
		moveAngle += 4 * Mathf.PI;
		moveAngle = Mathf.Repeat(moveAngle, 2 * Mathf.PI);
		targetPos = targetPos + new Vector2(Mathf.Cos(moveAngle) * unit_Status.moveSpeed, Mathf.Sin(moveAngle) * unit_Status.moveSpeed);
		if (targetPos.x < cam.transform.position.x - camWidth)
		{
			targetPos.x = cam.transform.position.x - camWidth;
			moveAngle = 0f;
		}
		if (targetPos.x > cam.transform.position.x + camWidth)
		{
			targetPos.x = cam.transform.position.x + camWidth;
			moveAngle = - Mathf.PI;
		}
		if (targetPos.y < cam.transform.position.y - camHeight)
		{
			targetPos.y = cam.transform.position.y - camHeight;
			moveAngle = Mathf.PI / 2f;
		}
		if (targetPos.y > cam.transform.position.y + camHeight)
		{
			targetPos.y = cam.transform.position.y + camHeight;
			moveAngle = - Mathf.PI / 2f;
		}
	}

	private void gather()
	{
		transform.position = Vector2.Lerp(startPos, gatherTar, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / unit_Status.gatherTime - 0.5f))));
		timer += Time.deltaTime;
		if (timer > unit_Status.gatherTime)
		{
			timer = 0f;
			status = Status.Spreading;
			cooldown = unit_Status.cooldownTime;
		}
	}

	private void spread()
	{
		transform.position = Vector2.Lerp(gatherTar, startPos, 1f - 1f / (1f + Mathf.Pow(2.71828f, 10f * (timer / unit_Status.gatherTime - 0.5f))));
		timer += Time.deltaTime;
		if (timer > unit_Status.gatherTime)
		{
			timer = 0f;
			status = Status.Free;
		}
	}

	public bool tryGather(Vector3 tar)
	{
		if (Vector3.Distance(tar, transform.position) < unit_Status.maxGatherDist && status == Status.Free && cooldown <= 0f)
		{
			float randAngle = Random.Range(0, Mathf.PI * 2);
			targetPos = Vector3.Lerp(targetPos, tar + new Vector3(Mathf.Cos(randAngle) * unit_Status.maxGatherDist, Mathf.Sin(randAngle) * unit_Status.maxGatherDist, 0), Time.deltaTime * unit_Status.dragSpeed);
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
		return unit_Status.gatherTime;
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
		GameObject blood = Instantiate(bloodFX, transform.position, Quaternion.identity);
		blood.transform.position = new Vector3(blood.transform.position.x, blood.transform.position.y, 1f);
		FindObjectOfType<CameraShake>().Shake();
	}
}