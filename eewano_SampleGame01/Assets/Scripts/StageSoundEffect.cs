using UnityEngine;
using System.Collections;

//AudioMixerにてグループ化し、各サウンドのバランスを容易に調整したい為、
//ステージBGM以外の音源をStageSoundEffectにまとめてある
public class StageSoundEffect : MonoBehaviour {
	
	private AudioSource downSound;
	private AudioSource fallSound;
	private AudioSource gameOverBGM;
	private AudioSource jumpSound;
	private AudioSource moveSound;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		downSound = audioSources [0];
		fallSound = audioSources [1];
		gameOverBGM = audioSources [2];
		jumpSound = audioSources [3];
		moveSound = audioSources [4];
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

	public void Jump() {
		jumpSound.PlayOneShot (jumpSound.clip);
	}

	public void Move() {
		moveSound.PlayOneShot (moveSound.clip);
	}
}