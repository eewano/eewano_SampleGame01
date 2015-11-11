using UnityEngine;
using System.Collections;

public class StageSoundEffect : MonoBehaviour {
	
	public AudioClip JumpSound;
	public AudioClip MoveSound;
	public AudioClip DownSound;
	public AudioClip GameOverBGM;
	
	public void Jump() {
		GetComponent<AudioSource>().PlayOneShot(JumpSound);
	}
	
	public void Move() {
		GetComponent<AudioSource>().PlayOneShot(MoveSound);
	}

	public void Down() {
		GetComponent<AudioSource>().PlayOneShot(DownSound);
	}

	public void GameOver() {
		GetComponent<AudioSource>().PlayOneShot(GameOverBGM);
	}
}