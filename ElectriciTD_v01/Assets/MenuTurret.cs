using UnityEngine;
using UnityEngine.UI;

public class MenuTurret : MonoBehaviour
{
	#region Variables
	private Animator anim;

	public Button playB, quitB, optionsB, creditsB;
    #endregion

    #region Unity Methods
    void Start()
    {
		anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    #endregion

    #region User Methods

	public void PlayButton()
	{

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
