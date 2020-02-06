using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	#region Variables

	public Text roundsText;

    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnEnable()
	{
		roundsText.text = GameManager.Rounds.ToString();
	}
	#endregion

	#region User Methods

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu()
	{
		Debug.Log("Go To Menu.");
	}

	#endregion
}
