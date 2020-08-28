using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_LevelManager : MonoBehaviour
{
	public static int countUnlockedLevels = 0;

	public Sprite unlockedIcon;
	public Sprite lockedIcon;
	public Sprite passedIcon;

	private void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			int numLevel = i + 1;
			transform.GetChild(i).gameObject.name = numLevel.ToString();
			transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = numLevel.ToString();

			if (i < countUnlockedLevels)
			{
				transform.GetChild(i).GetComponent<Image>().sprite = unlockedIcon;
				transform.GetChild(i).GetComponent<Button>().interactable = true;
			}
			else if (i > countUnlockedLevels)
			{
				transform.GetChild(i).GetComponent<Image>().sprite = lockedIcon;
				transform.GetChild(i).GetComponent<Button>().interactable = false;
			}
			else
			{
				transform.GetChild(i).GetComponent<Image>().sprite = passedIcon;
				transform.GetChild(i).GetComponent<Button>().interactable = true;
			}
		}
	}
}
