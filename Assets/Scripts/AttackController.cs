﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackController : MonoBehaviour
{
	public GameObject brustFX;
	public Light spotLight;
	private const float speed = 7.5f;
	private const float spriteScale = 4f;
	private SpriteRenderer spriteRenderer;
	private Status status = Status.Idle;
	private float radius = 0f;
	public float currentRadius = 0f;
	private List<GameObject> targets;
	private float damage = 0f;
	public float rangeMutiplier;
	public float brustMaxRange = 1.5f;
	private bool freeze = false;




	// Start is called before the first frame update
	void Start()
	{
		spotLight.intensity = 0;
		transform.localScale = new Vector2(0f, 0f);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (status)
		{
			case Status.Active:
				move();
				check_prop();
				check_splitedwall();
				break;
		}

	}

	private void move()
	{
		currentRadius += Time.deltaTime * speed;
		GameObject[] units;
		units = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject unit in units)
		{

			if (Vector2.Distance(transform.position, unit.transform.position) < currentRadius)
			{
				bool exists = false;
				for (int i = 0; i < targets.Count; i++)
				{
					if (unit.GetInstanceID() == targets[i].GetInstanceID())
					{
						exists = true;
						break;
					}
				}
				if (!exists)
				{
					targets.Add(unit);
					try {
						unit.GetComponent<EnemyController>().getDamaged(damage * (radius - currentRadius) / radius);
					} catch (Exception e) {
						Debug.Log("...");
					}
					try {
						unit.GetComponent<EnemyStatus>().getDamaged(damage * (radius - currentRadius) / radius, freeze);
					} catch (Exception e) {
						Debug.Log("...");
					}
					FindObjectOfType<AudioManager>().Play("UnitAttack", 1);
				}
			}
		}

		spotLight.intensity = currentRadius * 25;
		spotLight.gameObject.transform.localPosition = new Vector3(0, 0, spotLight.intensity / 5);

		transform.localScale = new Vector2(currentRadius * spriteScale, currentRadius * spriteScale);
		spriteRenderer.color = new Color(1f, 1f, 1f, (radius - currentRadius) / radius / 2f);

		if (currentRadius > radius)
		{
			spotLight.intensity = 0;
			currentRadius = 0;
			status = Status.Idle;
		}
	}

	public void check_prop()
	{
		GameObject[] props;
		props = GameObject.FindGameObjectsWithTag("prop");
		foreach (GameObject unit in props)
		{
			if (Vector2.Distance(transform.position, unit.transform.position) < currentRadius)
			{
				bool exists = false;
				for (int i = 0; i < targets.Count; i++)
				{
					if (unit.GetInstanceID() == targets[i].GetInstanceID())
					{
						exists = true;
						break;
					}
				}
				if (!exists)
				{
					targets.Add(unit);
                    if (unit.name == "prop_attract")
                    {
						unit.GetComponent<prop_attract>().execute(radius);
					}else if (unit.name == "prop_heal")
                    {
						unit.GetComponent<prop_heal>().execute(radius);
					}else if (unit.name == "splite_wall")
                    {
						unit.GetComponent<prop_splitedwall>().execute(radius);
					}
					
				}
			}
		}
	}

	public void check_splitedwall()
	{
		GameObject[] units;
		units = GameObject.FindGameObjectsWithTag("Wall");
		foreach (GameObject unit in units)
		{
			if (unit.name== "splited_wall" && Vector2.Distance(transform.position, unit.transform.position) < currentRadius)
			{
				bool exists = false;
				for (int i = 0; i < targets.Count; i++)
				{
					if (unit.GetInstanceID() == targets[i].GetInstanceID())
					{
						exists = true;
						break;
					}
				}
				if (!exists)
				{
					unit.GetComponent<prop_splitedwall>().execute(radius);
				}
			}
		}
	}

	public void activate(float r, float d, bool isFrozenHere)
	{
		if (isFrozenHere)
		{
			freeze = true;
		}
		radius = r;
		damage = d;
		currentRadius = 0f;
		targets = new List<GameObject>();
		status = Status.Active;
		Camera.main.gameObject.GetComponent<CameraController>().triggerShake(radius);
		GatherExplostionEffect(transform.position, Vector3.one * r * rangeMutiplier);
		FindObjectOfType<AudioManager>().Play("Explosion", r / 4f);
	}

	private enum Status
	{
		Idle,
		Active
	}

	//封装所有爆炸后的效果
	private void GatherExplostionEffect(Vector3 pos, Vector3 sc)
	{
		//爆炸特效
		GameObject effectGO = Instantiate(brustFX, pos, Quaternion.identity);
		effectGO.transform.localScale = sc.x * 6 > brustMaxRange ? Vector3.one * brustMaxRange / 6 : sc;

		effectGO.transform.GetChild(0).localScale = sc.x * 6 > brustMaxRange ? Vector3.one * brustMaxRange : sc * 6;
		FindObjectOfType<RipplePostProcesser>().RippleEffect();
	}
}
