using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	Vector3 diff;

	public GameObject target;
	public float followSpeed;

	void Start()
	{
		//プレイヤーを追従する距離を計算する
		diff = target.transform.position - transform.position;
	}

	void LateUpdate()
	{
		//プレイヤーとの距離が離れている程、追従速度が上がる
		transform.position = Vector3.Lerp (
			transform.position,
			target.transform.position - diff,
			Time.deltaTime * followSpeed
		);
	}
}
