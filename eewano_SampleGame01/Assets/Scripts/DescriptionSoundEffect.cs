using UnityEngine;
using System.Collections;

public class DescriptionSoundEffect : MonoBehaviour {
	
	private AudioSource startSound;

	void Start()
	{
		startSound = GetComponent<AudioSource> ();
	}

	public void GameStart() {
		startSound.PlayOneShot (startSound.clip);
	}
}