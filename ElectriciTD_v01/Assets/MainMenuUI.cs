using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	#region Variables

	public Button playB, quitB, optionsB, creditsB;

    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }
	#endregion

	#region User Methods

	public void SetQuality(int qIndex)
	{
		QualitySettings.SetQualityLevel(qIndex);
	}

	public void PlayButton(int level)
	{
		SceneManager.LoadScene(level);
	}

	public void QuitButton()
	{

	}

	public void OptionsMenu()
	{

	}

	public void CreditsMenu()
	{

	}

	#endregion
}
