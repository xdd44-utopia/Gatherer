using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private Vector2 targetPos;
	private const float followSpeed = 0.2f;
	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		move1();
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
			if (minDist < 0.5f) {
				Destroy(target, 0f);
			}
		}
		else {
			targetPos = transform.position;
		}
		transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
	}
}
