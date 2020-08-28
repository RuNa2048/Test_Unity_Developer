using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


/// <summary>
///Спавн метеоритов.
/// </summary>
public class Main : MonoBehaviour
{
	static public Main S;

	[Header("Set in Inspector")]
	public GameObject prefabMeteor;
	public float meteorDefaultPadding = 1.5f;
	public float maxLevelDuration = 60;
	public TextMeshProUGUI outputShieldLevel;
	public TextMeshProUGUI outputLevelTime;
	public GameObject victoryWindow;
	public GameObject defeatWindow;

	[Header("Set Dynamically")]
	public float levelTime = 15;
	public float meteorSpawnPerSecond = 0.5f;
	public bool levelComplete = true;

	private BoundsCheck bndCheck;
	private TextMeshProUGUI outSh;
	private TextMeshProUGUI outT;
	public float timeAfterStart = 0;
	

	private void Awake()
	{
		S = this;
		Time.timeScale = 1;

		outSh = outputShieldLevel.GetComponent<TextMeshProUGUI>();
		outT = outputLevelTime.GetComponent<TextMeshProUGUI>();
		bndCheck = GetComponent<BoundsCheck>();

		int cLevel = UI_LevelManager.countUnlockedLevels + 1;
		levelTime = 5;
		//levelTime *= cLevel;

		if (levelTime >= maxLevelDuration)
		{
			levelTime = maxLevelDuration;
		}

		Invoke("SpawnMeteor", 1f/meteorSpawnPerSecond);
	}

	private void Update()
	{
		if (!levelComplete)
		{
			defeatWindow.SetActive(true);
			return;
		}



		timeAfterStart = Time.timeSinceLevelLoad;
		if (timeAfterStart >= levelTime)
		{
			if (SceneManager.GetActiveScene().buildIndex - 2 == UI_LevelManager.countUnlockedLevels)
			{
				UI_LevelManager.countUnlockedLevels++;
			}

			if (levelComplete)
			{
				victoryWindow.SetActive(true);
				Time.timeScale = 0;
			}
		}

		outSh.text = "Уровень щита: " + Hero.S.shieldLevel;
		outT.text = Mathf.Round(maxLevelDuration - timeAfterStart).ToString();
	}

	public void SpawnMeteor()
	{
		GameObject go = Instantiate<GameObject>(prefabMeteor);

		float meteorPadding = meteorDefaultPadding;
		if (go.GetComponent<BoundsCheck>() != null)
		{
			meteorPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
		}

		Vector3 pos = Vector3.zero;
		float xMin = -bndCheck.camWidth + meteorPadding;
		float xMax = bndCheck.camWidth - meteorPadding;
		pos.x = Random.Range(xMin, xMax);
		pos.y = bndCheck.camHeight + meteorPadding;
		go.transform.position = pos;

		Invoke("SpawnMeteor", 1f/meteorSpawnPerSecond);
	}

	public void DelayedRestart(float delay)
	{
		Invoke("Restart", delay);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("LevelsMenu");
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitLevel()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
