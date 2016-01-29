using UnityEngine;
using System.Collections;

public class TitleSoundEffect : MonoBehaviour {
	
	private AudioSource StartSound;
	private AudioSource DescriptionSound;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		StartSound = audioSources [0];
		DescriptionSound = audioSources [1];
	}

	public void GameStart() {
		StartSound.PlayOneShot (StartSound.clip);
	}

	public void Description() {
		DescriptionSound.PlayOneShot (DescriptionSound.clip);
	}
}