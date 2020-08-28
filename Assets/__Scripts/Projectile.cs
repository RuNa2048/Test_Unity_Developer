using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[Header("Set in Inspector")]
	public float damageOnHit = 1f;

	private BoundsCheck bndCheck;

	private void Awake()
	{
		bndCheck = GetComponent<BoundsCheck>();
	}

	private void Update()
	{
		if (bndCheck.offUp)
		{
			Destroy(gameObject);
		}
	}
}
