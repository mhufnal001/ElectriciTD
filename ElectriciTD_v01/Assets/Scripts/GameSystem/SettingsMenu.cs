using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
	#region Variables
	[Header("Game Settings")]
	public AudioMixer masterMix;
	public TextMeshProUGUI volumeText;
	public TMP_Dropdown resDropdown;
	Resolution[] res;

	bool isPaused = false;
	[Header("Pause Game")]
	public GameObject pText;
	public GameObject rText;


	#endregion

	#region Unity Methods
	void Start()
    {
		res = Screen.resolutions;
		isPaused = false;

		resDropdown.ClearOptions();

		List<string> options = new List<string>();
		int currentResIndex = 0;

		for (int i = 0; i < res.Length; i++)
		{
			string option = res[i].width + " x " + res[i].height;
			options.Add(option);

			if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
			{
				currentResIndex = i;
			}
		}

		resDropdown.AddOptions(options);
		resDropdown.value = currentResIndex;
		resDropdown.RefreshShownValue();
    }

    void Update()
    {
		if (isPaused == true)
		{
			Time.timeScale = 0f;
			pText.SetActive(false);
			rText.SetActive(true);
		}
		else
		{
			Time.timeScale = 1f;
			pText.SetActive(true);
			rText.SetActive(false);
		}
	}
    #endregion

    #region User Methods

	public void SetVolume (float volume)
	{
		masterMix.SetFloat("MasterVolume", volume);
		float _volume = Mathf.InverseLerp(-80, 0, volume);
		float result = Mathf.Lerp(0, 100, _volume);

		volumeText.text = result.ToString("0");
	}

	public void SetQuality(int qIndex)
	{
		QualitySettings.SetQualityLevel(qIndex);
	}

	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetRes(int resIndex)
	{
		Resolution _res = res[resIndex];
		Screen.SetResolution(_res.width, _res.height, Screen.fullScreen);
	}

	public void TogglePause()
	{
		isPaused = !isPaused;
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

#endregion
}
