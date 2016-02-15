using UnityEngine;
using System.Collections;

//単純にMainCameraをプレイヤーオブジェクトの子オブジェクトにして映す方法を取らないのは、
//プレイヤーを隠したい時にカメラを追跡させない為（障害物に衝突した時や落下した時等）
public class CameraFollow : MonoBehaviour {

	[SerializeField] private PlayerController player;
	[SerializeField] private Transform target;
	private Vector3 offset;

	void Start()
	{
		offset = GetComponent<Transform>().position - target.position * Time.deltaTime;
	}

	void Update ()
	{
		if (player.Life () <= 0) {
			Invoke ("CameraStop", 1.5f);
		}
		if (PlayerController.Fall) {
			return;
		}
		// 自分の座標にtargetの座標を代入する     
		GetComponent<Transform>().position = target.position + offset;
	}

	void CameraStop()
	{
		return;
	}
}