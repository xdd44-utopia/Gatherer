using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
	public GameObject bloodFX;
	public GameObject frozenFX;
	private enemyStatus status=enemyStatus.Normal;
	public float attackDistance = 0.5f;
	public float attackInterval = 0.5f;
	public float atk = 0.5f;
	public CharacterAnimationParameter chPrams;

	private enum enemyStatus{
		Normal,
		Frozen
	}
	void Update(){
		switch (status) {
			case enemyStatus.Frozen:
				attackDistance=0f;
				attackInterval=0f;
				atk=0f;
				break;
			// case Status.Normal:
			//     break;
		}
	}

    public void getDamaged(float damage,bool isFrozenHere)
	{
		if(!isFrozenHere){
			FindObjectOfType<AudioManager>().Play("UnitAttack", 1);
		}
		GameObject blood = Instantiate(bloodFX, transform.position, Quaternion.identity);
		blood.transform.position = new Vector3(blood.transform.position.x, blood.transform.position.y, 1f);
		transform.GetComponentInChildren<HealthbarController>().getDamaged(damage);
	}
}