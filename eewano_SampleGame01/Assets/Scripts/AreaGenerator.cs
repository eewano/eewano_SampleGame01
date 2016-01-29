using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaGenerator : MonoBehaviour {

	const int AreaTipSize = 50;
	private int currentTipIndex;

	[SerializeField] Transform character = null;
	//エリアチッププレファブ配列
	[SerializeField] GameObject[] areaTips = null;
	//自動生成開始インデックス
	[SerializeField] int startTipIndex = 0;
	//生成先読み個数
	[SerializeField] int preInstantiate = 0;
	//生成済みエリアチップ保持リスト
	[SerializeField] List<GameObject> generatedAreaList = new List<GameObject>();

	void Start()
	{
		currentTipIndex = startTipIndex - 1;
		UpdateArea (preInstantiate);
	}

	void Update()
	{
		//キャラクターの位置から現在のエリアチップのインデックスを計算する
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

		//エリア保持上限内になるまで古いエリアを削除する
		while (generatedAreaList.Count > preInstantiate + 1)
			DestroyOldestArea ();

		currentTipIndex = toTipIndex;
	}

	//指定のインデックス位置にAreaオブジェクトをランダムに生成する
	GameObject GenerateArea(int tipIndex)
	{
		int nextAreaTip = Random.Range (0, areaTips.Length);

		GameObject areaObject = (GameObject)Instantiate (
			                        areaTips [nextAreaTip],
			                        new Vector3 (0, 0, tipIndex * AreaTipSize),
			                        Quaternion.identity);
		
		return areaObject;
	}

	//一番古いエリアを削除する
	void DestroyOldestArea()
	{
		GameObject oldArea = generatedAreaList [0];
		generatedAreaList.RemoveAt (0);
		Destroy (oldArea);
	}
}