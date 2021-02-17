using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
	private const float speed = 3f;
	private const float spriteScale = 2f;
	private SpriteRenderer spriteRenderer;
	private Status status = Status.Idle;
	private float radius = 0f;
	public float currentRadius = 0f;
	private List<GameObject> targets;
	private float damage = 0f;
	// Start is called before the first frame update
	void Start() {
		transform.localScale = new Vector2(0f, 0f);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update() {
		switch (status) {
			case Status.Active:
				move();
				break;
		}
	}

	private void move() {
		currentRadius += Time.deltaTime * speed;
		GameObject[] units;
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject unit in units) {
			if (Vector2.Distance(transform.position, unit.transform.position) < currentRadius) {
				bool exists = false;
				for (int i=0;i<targets.Count;i++) {
					if (unit.GetInstanceID() == targets[i].GetInstanceID()) {
						exists = true;
						break;
					}
				}
				if (!exists) {
					targets.Add(unit);
					unit.GetComponent<UnitController>().getDamaged(damage * (radius - currentRadius) / radius);
				}
			}
		}
		transform.localScale = new Vector2(currentRadius * spriteScale, currentRadius * spriteScale);
		spriteRenderer.color = new Color(1f, 1f, 1f, (radius - currentRadius) / radius / 2f);

		if (currentRadius > radius) {
			currentRadius = 0;
			status = Status.Idle;
		}
	}

	public void activate(float r, float d) {
		r /= transform.parent.localScale.x;
		radius = r;
		damage = d;
		currentRadius = 0f;
		targets = new List<GameObject>();
		status = Status.Active;
	}

	private enum Status {
		Idle,
		Active
	}
}
