using UnityEngine;
using System.Collections;

public class StageSoundEffect : MonoBehaviour {

	private AudioSource jumpSound;
	private AudioSource moveSound;
	private AudioSource downSound;
	private AudioSource fallSound;
	private AudioSource gameOverBGM;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		jumpSound = audioSources [0];
		moveSound = audioSources [1];
		downSound = audioSources [2];
		fallSound = audioSources [3];
		gameOverBGM = audioSources [4];
	}

	public void Jump() {
		jumpSound.PlayOneShot (jumpSound.clip);
	}

	public void Move() {
		moveSound.PlayOneShot (moveSound.clip);
	}

	public void Down() {
		downSound.PlayOneShot (downSound.clip);
	}

	public void Falling() {
		fallSound.PlayOneShot (fallSound.clip);
	}

	public void GameIsOver() {
		gameOverBGM.PlayOneShot (gameOverBGM.clip);
	}
}