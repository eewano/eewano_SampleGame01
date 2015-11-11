using UnityEngine;
using System.Collections;

public class DescriptionSoundEffect : MonoBehaviour {
	
	public AudioClip StartSound;
	
	public void GameStart() {
		GetComponent<AudioSource>().PlayOneShot(StartSound);
	}
}