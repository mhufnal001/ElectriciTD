using UnityEngine;
using UnityEngine.UI;

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
		roundsText.text = GameManager.instance.Rounds.ToString();
	}
	#endregion

	#region User Methods
	#endregion
}
