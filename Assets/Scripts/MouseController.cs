using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

	private const float maxDist = 1f;
	private const float attackRange = 2f;

	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
		transform.position = mousePos;
		if (Input.GetMouseButtonDown(0)) {
			gather();
		}
	}

	private void gather() {
		GameObject[] units;
		int cnt = 0;
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject unit in units) {
			bool result = unit.GetComponent<UnitController>().startGather(transform.position);
			if (result) {
				cnt++;
			}
		}
		if (cnt > 0) {
			attack();
		}
		Debug.Log(cnt + " units gathered");
	}

	private void attack() {
		GameObject[] enemies;
		int cnt = 0;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange) {
				Destroy(enemy, 0.3f);
			}
		}
		Debug.Log(cnt + " units gathered");
	}
}
