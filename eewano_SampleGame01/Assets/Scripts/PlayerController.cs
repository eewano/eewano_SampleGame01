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

	[SerializeField] private float gravity;
	[SerializeField] private float speedX;
	[SerializeField] private float speedZ;
	[SerializeField] private float speedJump;
	[SerializeField] private float accelerationZ;
	[SerializeField] private float speedPlus;

	public static bool Fall = false;
	private bool LeftButton = false;
	private bool RightButton = false;
	private bool JumpButton = false;

	//-----ライフ取得用の関数-----
	public int Life()
	{
		return life;
	}

	//-----仰け反り判定-----
	public bool IsStan()
	{
		return recoverTime > 0.0f || life <= 0;
	}

	//-----ボタン長押し用の判定
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
		Fall = false;
	}

	void Update()
	{
		speedZ += Time.deltaTime * speedPlus;

		if (LeftButton && controller.isGrounded) {
			MoveLeft ();
		} else if (RightButton && controller.isGrounded) {
			MoveRight ();
		} else if (JumpButton) {
			Jump ();
		}

		//-----仰け反り時の行動-----
		if (IsStan ()) {
			//動きを止めて仰け反り状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		} else {
			//徐々に加速しながら前進する
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
			moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
		}

		//重力分の力を毎フレーム追加する
		moveDirection.y -= gravity * Time.deltaTime;

		//移動を実行する
		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

		//移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded)
			moveDirection.y = 0;

		//速度が０以上なら走るアニメーションにする
		animator.SetBool ("Run", moveDirection.z > 0.0f);

		if (Life () <= 0) {
			Invoke("Delete", 1.5f);
		}
	}

	//-----横移動及びジャンプ-----
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
	//----------

	//-----ライフが0になったらプレイヤーを消去する
	public void Delete()
	{
		gameObject.SetActive (false);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan ())
			return;

		if (hit.gameObject.tag == "Obstacle") {
			//ライフを減らして仰け反り状態に移行
			life --;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
		}

		if (hit.gameObject.tag == "Ball") {
			//ライフを減らして仰け反り状態に移行
			life --;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stagesoundEffect.Down();
			Destroy (hit.gameObject, 1.5f);
		}

		if (hit.gameObject.tag == "Fall") {
			Fall = true;
			life = 0;
			stagesoundEffect.Falling();
			Destroy (hit.gameObject);
		}
	}
}