using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaGenerator : MonoBehaviour {

	const int AreaTipSize = 50;

	int currentTipIndex;

	//ターゲットキャラクターの指定
	public Transform character;
	//エリアチッププレファブ配列
	public GameObject[] areaTips;
	//自動生成開始インデックス
	public int startTipIndex;
	//生成先読み個数
	public int preInstantiate;
	//生成済みエリアチップ保持リスト
	public List<GameObject> generatedAreaList = new List<GameObject>();

	void Start()
	{
		currentTipIndex = startTipIndex - 1;
		UpdateArea (preInstantiate);
	}

	void Update()
	{
		//キャラクターの位置から現在のエリアチップのインデックスを計算
		int charaPositionIndex = (int)(character.position.z / AreaTipSize);

		//次のエリアチップに入ったらエリアの更新処理を行なう
		if (charaPositionIndex + preInstantiate > currentTipIndex) {
			UpdateArea (charaPositionIndex + preInstantiate);
		}
	}

	//指定のIndexまでのエリアチップを生成して、管理下に置く
	void UpdateArea(int toTipIndex)
	{
		if (toTipIndex <= currentTipIndex)
			return;

		//指定のエリアチップまでを作成
		for (int i = currentTipIndex + 1; i <= toTipIndex; i++) {
			GameObject areaObject = GenerateArea (i);

			//生成したエリアチップを管理リストに追加する
			generatedAreaList.Add (areaObject);
		}

		//エリア保持上限内になるまで古いエリアを削除
		while (generatedAreaList.Count > preInstantiate + 2)
			DestroyOldestArea ();

		currentTipIndex = toTipIndex;
	}

	//指定のインデックス位置にAreaオブジェクトをランダムに生成
	GameObject GenerateArea(int tipIndex)
	{
		int nextAreaTip = Random.Range (0, areaTips.Length);

		GameObject areaObject = (GameObject)Instantiate (
			                        areaTips [nextAreaTip],
			                        new Vector3 (0, 0, tipIndex * AreaTipSize),
			                        Quaternion.identity
		                        );

		return areaObject;
	}

	//一番古いエリアを削除
	void DestroyOldestArea()
	{
		GameObject oldArea = generatedAreaList [0];
		generatedAreaList.RemoveAt (0);
		Destroy (oldArea);
	}
}
