using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[Header("Set in Inspector")]
	public AudioMixerGroup mixer;
	public GameObject settingsMenu;
	public GameObject mainMenu;

	public void ToogleMusic(bool enabled)
	{
		if (enabled)
		{
			mixer.audioMixer.SetFloat("MusicVolume", 0);
		}
		else
		{
			mixer.audioMixer.SetFloat("MusicVolume", -80);
		}
	}

	public void ChangeVolume(float volume)
	{
		mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
	}

	public void OpenSettingsMenu()
	{
		settingsMenu.SetActive(true);
		mainMenu.SetActive(false);
	}

	public void OpenMainMenu()
	{
		settingsMenu.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void OpenLevelMenu()
	{
		SceneManager.LoadScene("LevelsMenu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
