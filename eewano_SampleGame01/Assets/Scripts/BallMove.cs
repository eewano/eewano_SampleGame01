using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {

	Vector3 startPosition;

	public float amplitude;
	public float speed;
	public GameObject ballBrokenPrefab;

	void Start()
	{
		startPosition = transform.localPosition;
	}

	void Update ()
	{
		//変位を計算する。
		float x = amplitude * Mathf.Cos(Time.time * speed);

		//xを変位させたポジションに再設定する。
		transform.localPosition = startPosition + new Vector3(x, 0, 0);
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		if (col.gameObject.tag == "Player") {
			Instantiate (ballBrokenPrefab, transform.position,
				ballBrokenPrefab.transform.rotation);
			//ボールを削除する。
			Destroy (gameObject);
		}
	}
}