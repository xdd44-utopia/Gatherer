using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
	public GameObject[] bloodStains;
	public float hp = 1f;
	private float scaleBase = 0.5f;
	
	void Update() {
		if (hp <= 0)
		{
			Death();
		}
		transform.localScale = new Vector2(hp, transform.localScale.y);
		transform.localPosition = new Vector2((hp / 2f - 0.5f) * scaleBase, transform.localPosition.y);
	}

	public void getDamaged(float damage) {
		hp -= damage;
    }

    private void Death()
    {
		Instantiate(bloodStains[Random.Range(0, bloodStains.Length)], transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject, 0f);
    }
}
