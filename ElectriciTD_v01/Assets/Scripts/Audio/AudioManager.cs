using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
	#region Variables
	public Sounds[] sounds;
	public static AudioManager instanceAM;
    #endregion

    #region Unity Methods
    void Awake()
    {
		if (instanceAM == null)
		{
			instanceAM = this;
		}
		else
		{
			Destroy(gameObject);

			return;
		}

		foreach (Sounds s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.mixer;

		}
        
    }
	private void Start()
	{
		Play("Theme");
	}

	void Update()
    {
        
    }
    #endregion

    #region User Methods
	public void Play (string name)
	{
		Sounds s = Array.Find(sounds,sounds => sounds.name == name);
		s.source.Play();
	}
    #endregion	
}
