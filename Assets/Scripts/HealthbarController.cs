using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
	private float hp = 1f;
	private float scaleBase = 0.5f;
	// Start is called before the first frame update
	void Start() {
		 
	}

	// Update is called once per frame
	void Update() {
		if (hp <= 0) {
			Destroy(transform.parent.gameObject, 0f);
		}
		transform.localScale = new Vector2(hp, transform.localScale.y);
		transform.localPosition = new Vector2((hp / 2f - 0.5f) * scaleBase, transform.localPosition.y);
	}

	public void getDamaged(float damage) {
		hp -= damage;
	}
}
