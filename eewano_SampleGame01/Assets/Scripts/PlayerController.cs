using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	const int MinLane = -4;
	const int MaxLane = 4;
	const float LaneWidth = 1.0f;
	const int DefaultLife = 1;	//プレイヤーのライフ
	const float StunDuration = 1.0f;	//被ダメージ時の仰け反り時間

	CharacterController controller;
	Animator animator;
	StageSoundEffect stagesoundEffect;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float recoverTime = 0.0f;

	public float gravity;
	public float speedZ;
	public float speedX;
	public float speedJump;
	public float accelerationZ;
	public float speedPlus;

	//-----ライフ取得用の関数-----
	public int Life()
	{
		return life;
	}
	//-----ライフ取得用の関数-----

	//-----仰け反り判定-----
	public bool IsStan()
	{
		return recoverTime > 0.0f || life <= 0;
	}
	//-----仰け反り判定-----

	void Start()
	{
		//必要なコンポーネントを自動で取得する
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator>();
		stagesoundEffect = GameObject.Find("StageSoundController").GetComponent<StageSoundEffect>();
	}

	void Update()
	{
		speedZ += Time.deltaTime * speedPlus;

		//キーボードで動作させる
		if (Input.GetKeyDown ("left"))
			MoveToLeft ();
		if (Input.GetKeyDown ("right"))
			MoveToRight ();
		if (Input.GetKeyDown ("space"))
			Jump ();

		//-----仰け反り時の行動-----
		if (IsStan ()) {
			//動きを止め仰け反り状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		} else {
			//徐々に加速しZ方向に前進させる
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
			moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);

			//X方向は目標のポジションまでの差分の割合で速度を計算
			float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
			moveDirection.x = ratioX * speedX;
		}
		//-----仰け反り時の行動-----

		//重力分の力を毎フレーム追加する
		moveDirection.y -= gravity * Time.deltaTime;

		//移動を実行する
		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

		//移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded)
			moveDirection.y = 0;

		//速度が０以上なら走っているフラグをtrueにする
		animator.SetBool ("Run", moveDirection.z > 0.0f);
	}

	//1レーン左に移動する
	public void MoveToLeft()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && targetLane > MinLane)
			targetLane--;

		//移動サウンドを再生する
		stagesoundEffect.Move();
	}

	//1レーン右に移動する
	public void MoveToRight()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && targetLane < MaxLane)
			targetLane++;

		//移動サウンドを再生する
		stagesoundEffect.Move();
	}

	//ジャンプする
	public void Jump()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded) {
			moveDirection.y = speedJump;

			//ジャンプトリガーを設定
			animator.SetTrigger ("Jump");

			//ジャンプサウンドを再生する
			stagesoundEffect.Jump();
		}
	}

	//CharacterControllerにコリジョンが生じた時の処理
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan ())
			return;

		if (hit.gameObject.tag == "Obstacle") {
			//ライフを減らして仰け反り状態に移行
			life--;
			recoverTime = StunDuration;

			//ダメージトリガーを設定
			animator.SetTrigger ("Down");
			
			//ダウンサウンドを再生する
			stagesoundEffect.Down();
		}
	}
}