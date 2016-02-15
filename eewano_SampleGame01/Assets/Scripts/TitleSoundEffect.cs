using UnityEngine;
using System.Collections;

//AudioMixerにてグループ化し、各サウンドのバランスを容易に調整したい為、
//ステージBGM以外の音源をStageSoundEffectにまとめてある
public class TitleSoundEffect : MonoBehaviour {

	private AudioSource DescriptionSound;
	private AudioSource StartSound;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		DescriptionSound = audioSources [0];
		StartSound = audioSources [1];
	}

	public void Description() {
		DescriptionSound.PlayOneShot (DescriptionSound.clip);
	}

	public void GameStart() {
		StartSound.PlayOneShot (StartSound.clip);
	}
}