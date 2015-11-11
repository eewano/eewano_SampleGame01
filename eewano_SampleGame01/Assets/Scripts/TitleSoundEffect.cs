using UnityEngine;
using System.Collections;

public class TitleSoundEffect : MonoBehaviour {
	
	public AudioClip StartSound;
	public AudioClip DescriptionSound;
	
	public void GameStart() {
		GetComponent<AudioSource>().PlayOneShot(StartSound);
	}

	public void Description() {
		GetComponent<AudioSource>().PlayOneShot(DescriptionSound);
	}
}