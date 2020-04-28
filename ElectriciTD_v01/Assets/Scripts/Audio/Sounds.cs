using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
	#region Variables
	public string name;

	public AudioClip clip;
	public AudioMixerGroup mixer;
	[HideInInspector]
	public AudioSource source;

	[Range(.1f, 3f)]
	public float pitch;

	public bool loop;
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
    #endregion	
}
