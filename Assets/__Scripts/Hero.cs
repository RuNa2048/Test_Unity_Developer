using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	public static Hero S;

	[Header("Set in Inspector")]
	public float speed = 30;
	public float rollMult = -45;
	public float pitchMilt = 30;
	public float gameRestartDelay = 2f;
	public GameObject projectilePrefab;
	public float projectileSpeed = 40;
	public float timeBetweenShots = 0.1f;
	public SimpleTouchPad touchPad;
	public AudioSource shootSound;
	public GameObject explosionPrefab;


	[Header("Set Dynamically")]
	[SerializeField]
	private float _shieldLevel = 2;
	public float counterBetweenShots;

	private void Awake()
	{
		if (S == null)
		{
			S = this;
		}
		else
		{
			Debug.Log("Hero.Awake() - попытка назначить второй Hero.S");
		}
	}

	private void Update()
	{
		Move();

		counterBetweenShots -= Time.deltaTime;
		if (counterBetweenShots <= 0)
		{
			TempFire();
			counterBetweenShots = timeBetweenShots;
		}
	}

	private void Move()
	{
		// WASD перемещение
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");

		//Перемещение с помощью контроллера. При включении активировать в Canvas "MovementZone".
		//float xAxis = touchPad.GetDirection().x;
		//float yAxis = touchPad.GetDirection().y;

		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		transform.rotation = Quaternion.Euler(yAxis * pitchMilt, xAxis * rollMult, 0);
	}

	private void TempFire()
	{
		GameObject projGO = Instantiate<GameObject>(projectilePrefab);
		projGO.transform.position = transform.position;
		Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
		rigidB.velocity = Vector3.up * projectileSpeed;

		shootSound.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		GameObject go = other.gameObject;
		if (go.tag == "Meteor")
		{
			shieldLevel--;
			Destroy(go);
			Instantiate<GameObject>(explosionPrefab);
		}
		else
		{
			print("Столкновение с неизвестным go:" + go.name);
		}
	}

	public float shieldLevel
	{
		get { return _shieldLevel; }
		set
		{
			_shieldLevel = Mathf.Min(value, 2);
			if (value < 0)
			{
				Destroy(gameObject);
				Main.S.levelComplete = false;
			}
		}
	}
}
