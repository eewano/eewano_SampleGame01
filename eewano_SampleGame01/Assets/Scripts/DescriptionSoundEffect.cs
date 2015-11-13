using UnityEngine;
using System.Collections;

public class DescriptionSoundEffect : MonoBehaviour {
	
	private AudioSource StartSound;

	void Start()
	{
		StartSound = GetComponent<AudioSource> ();
	}

	public void GameStart() {
		StartSound.PlayOneShot (StartSound.clip);
	}
}