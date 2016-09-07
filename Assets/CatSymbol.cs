using UnityEngine;
using System.Collections;
using DG.Tweening;
/* 集計数をビジュアライズする大きい猫 */
class CatSymbol : MonoBehaviour{

	/* ねこ吸収時のエフェクト */
	[SerializeField]
	private GameObject efx;

	private Transform model;

	/* 現在のスケール */
	private float scale;

	/* ねこ一匹で増えるスケール */
	private const float SPC = 0.217f;

	/* 情報テキスト */
	[SerializeField]
	private InfoText info;

	void Awake()
	{
		model = transform.FindChild("Model");
		scale = model.transform.localScale.x;
	}	

	/* サイズを変更 */
	public void UpdateSize(float newScale)
	{
		scale = newScale;

		// ちょっとボヨンとした表現
		model.transform.DOScale( scale , 1f);

		// 情報テキスト更新
		float num = scale;
		num = ( Mathf.Round( num * 10) ) / 100;
		string str =  "CAT:" + num.ToString() + " M";
		info.UpDateLabel(str);
	}

	/* 猫追加 */
	public void AddCat(int num=1)
	{
		float newScale = scale + (SPC * num);
		UpdateSize( newScale );
	}

	/* 猫が飛び込んで大きくなる 最初の一回以降のサイズ変更は基本的にこっちを使う*/
	void OnTriggerEnter(Collider col)
	{
		if( !col.tag.Equals("Cat") )
		{
			return;	
		}

		// 吸収エフェクト
		Instantiate(efx, col.gameObject.transform.position, Quaternion.Euler(-90f, 0, 0) );

		// 子猫消滅
		Destroy( col.gameObject );

		AddCat();
	}
}