using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	const int DefaultLife = 1;	//プレイヤーのライフ
	const float StunDuration = 1.0f;	//被ダメージ時の仰け反り時間

	private CharacterController controller;
	private Animator animator;
	private StageSoundEffect stagesoundEffect;

	Vector3 moveDirection = Vector3.zero;
	private int life = DefaultLife;
	private float recoverTime = 0.0f;

	[SerializeField] float gravity = 0;
	[SerializeField] float speedX = 0;
	[SerializeField] float speedZ = 0;
	[SerializeField] float speedJump = 0;
	[SerializeField] float accelerationZ = 0;
	[SerializeField] float speedPlus = 0;

	private bool LeftButton = false;
	private bool RightButton = false;
	private bool JumpButton = false;

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

	public void PushLeftDown()
	{
		LeftButton = true;
	}
	public void PushLeftUp()
	{
		LeftButton = false;
	}
	public void PushRightDown()
	{
		RightButton = true;
	}
	public void PushRightUp()
	{
		RightButton = false;
	}
	public void PushJumpDown()
	{
		JumpButton = true;
	}

	public void PushJumpUp()
	{
		JumpButton = false;
	}



	void Start()
	{
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator>();
		stagesoundEffect = GameObject.Find("StageSoundController").GetComponent<StageSoundEffect>();
	}

	void Update()
	{
		speedZ += Time.deltaTime * speedPlus;

		if (LeftButton) {
			MoveLeft ();
		} else if (RightButton) {
			MoveRight ();
		} else if (JumpButton) {
			Jump ();
		}

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

	public void MoveLeft()
	{
		this.transform.position += this.transform.right * Time.deltaTime * speedX * -1;
	}

	public void MoveRight()
	{
		this.transform.position += this.transform.right * Time.deltaTime * speedX;
	}
		
	public void Jump()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && JumpButton == true) {
			moveDirection.y = speedJump;
			animator.SetTrigger ("Jump");
			stagesoundEffect.Jump();
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan ())
			return;

		if (hit.gameObject.tag == "Obstacle") {
			//ライフを減らして仰け反り状態に移行
			life--;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
		}

		if (hit.gameObject.tag == "Ball") {
			//ライフを減らして仰け反り状態に移行
			life--;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
			Destroy (hit.gameObject, 1.5f);
		}
	}
}

/*
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	const int MinLane = -6;
	const int MaxLane = 6;
	const float LaneWidth = 0.75f;
	const int DefaultLife = 1;	//プレイヤーのライフ
	const float StunDuration = 1.0f;	//被ダメージ時の仰け反り時間

	private CharacterController controller;
	private Animator animator;
	private StageSoundEffect stagesoundEffect;

	Vector3 moveDirection = Vector3.zero;
	private int targetLane;
	private int life = DefaultLife;
	private float recoverTime = 0.0f;

	[SerializeField] float gravity = 0;
	[SerializeField] float speedZ = 0;
	[SerializeField] float speedX = 0;
	[SerializeField] float speedJump = 0;
	[SerializeField] float accelerationZ = 0;
	[SerializeField] float speedPlus = 0;

	private bool JumpButton = false;

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
			JumpButton = true;
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

	public void MoveToLeft()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && targetLane > MinLane)
			targetLane--;
		
		stagesoundEffect.Move();
	}

	public void MoveToRight()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && targetLane < MaxLane)
			targetLane++;
		
		stagesoundEffect.Move();
	}

	public void PushJumpDown()
	{
		JumpButton = true;
	}

	public void PushJumpUp()
	{
		JumpButton = false;
	}

	public void Jump()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && JumpButton == true) {
			moveDirection.y = speedJump;
			animator.SetTrigger ("Jump");
			stagesoundEffect.Jump();
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan ())
			return;

		if (hit.gameObject.tag == "Obstacle") {
			//ライフを減らして仰け反り状態に移行
			life--;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
		}

		if (hit.gameObject.tag == "Ball") {
			//ライフを減らして仰け反り状態に移行
			life--;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
			Destroy (hit.gameObject, 1.5f);
		}
	}
}
*/