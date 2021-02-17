using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private Vector2 targetPos;

	//Move2
	private const float ka = 0.1f;
	private const float attractRange = 2.5f;
	private const float forceLimit = 0.5f;
	private const float kr = 0.05f;
	private const float repulsiveRange = 1000f;
	private const float repLimit = 0.1f;
	private const float followSpeed = 0.25f;

	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		move2();
	}

	void move1() {
		GameObject[] units;
		GameObject target = null;
		float minDist = 2000f;
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject unit in units) {
			if (Vector2.Distance(transform.position, unit.transform.position) < minDist) {
				minDist = Vector2.Distance(transform.position, unit.transform.position);
				target = unit;
			}
		}
		if (units.Length > 0) {
			targetPos = target.transform.position;
		}
		else {
			targetPos = transform.position;
		}
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
	}

	void move2() {
		GameObject[] units;
		GameObject target = null;
		float minDist = 2000f;
		bool attracted = false;

		Vector2 direction = new Vector2(0, 0);
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject unit in units) {
			if (Vector2.Distance(transform.position, unit.transform.position) < attractRange) {
				float forceBase = Vector2.Distance(new Vector2(unit.transform.position.x, unit.transform.position.y), targetPos);
				forceBase = (forceBase > forceLimit ? forceBase : forceLimit);
				direction += (new Vector2(unit.transform.position.x, unit.transform.position.y) - targetPos).normalized * ka / forceBase / forceBase;
				attracted = true;
			}
			if (Vector2.Distance(transform.position, unit.transform.position) < minDist) {
				minDist = Vector2.Distance(transform.position, unit.transform.position);
				target = unit;
			}
		}
		if (attracted) {
			targetPos += direction;
		}
		else {
			targetPos = target.transform.position;
		}

		Vector2 repulsive = new Vector2(0, 0);
		units = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject unit in units) {
			if (Vector2.Distance(transform.position, unit.transform.position) < repulsiveRange) {
				float forceBase = Vector2.Distance(new Vector2(unit.transform.position.x, unit.transform.position.y), targetPos);
				forceBase = (forceBase > repLimit ? forceBase : forceLimit);
				repulsive -= (new Vector2(unit.transform.position.x, unit.transform.position.y) - targetPos).normalized * kr / forceBase / forceBase;
			}
		}
		targetPos += repulsive;

		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
	}
}
