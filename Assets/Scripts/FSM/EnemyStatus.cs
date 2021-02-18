using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
	public GameObject bloodFX;
	public float attackDistance = 0.5f;
	public float attackInterval = 0.5f;
	public float atk = 0.5f;
	public CharacterAnimationParameter chPrams;

    public void getDamaged(float damage)
	{
		Instantiate(bloodFX, transform.position, Quaternion.identity);
		transform.GetComponentInChildren<HealthbarController>().getDamaged(damage);
	}

}
