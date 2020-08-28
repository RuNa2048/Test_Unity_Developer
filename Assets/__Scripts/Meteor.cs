using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	[Header("Set in Inspector")]
	public float health = 10;
	public float speed = 10f;
	public float scrore = 100;
	public GameObject explosionPrefab;

	private BoundsCheck bndCheck;

	public Vector3 pos
	{
		get { return (this.transform.position); }
		set { this.transform.position = value; }
	}

	private void Awake()
	{
		bndCheck = GetComponent<BoundsCheck>();
	}

	private void Update()
	{
		Move();

		if (bndCheck != null && bndCheck.offDown)
		{
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject otherGO = collision.gameObject;
		switch(otherGO.tag)
		{
			case "ProjectileHero":
				Projectile p = otherGO.GetComponent<Projectile>();
				if (!bndCheck.isOnScreen)
				{
					Destroy(otherGO);
					break;
				}
				health -= p.damageOnHit;
				if (health <= 0)
				{
					Instantiate<GameObject>(explosionPrefab);
					Destroy(this.gameObject);
				}
				Destroy(otherGO);
				break;

			default:
				print("Метеор получил урон не от снаряда героя:" + otherGO.name);
				break;
		}

	}
}
