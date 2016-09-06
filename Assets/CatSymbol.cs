using UnityEngine;
using System.Collections;
using DG.Tweening;
/* 集計数をビジュアライズする大きい猫 */
class CatSymbol : MonoBehaviour{

	/* ねこ吸収時効果音 */
	public AudioClip se_gain;

	/* ねこ吸収時のエフェクト */
	public GameObject efx;

	private AudioSource audio;

	/* 現在のスケール */
	private float scale;

	/* ねこ一匹で増えるスケール */
	private const float spc = 0.25f;

	void Awake(){
		audio = GetComponent<AudioSource>();
		scale = transform.localScale.x;
	}	

	/* サイズを変更 */
	public void UpdateSize(float newScale){
		scale = newScale;

		// ちょっとボヨンとした表現
		transform.DOScale( scale , 1f);
	}

	/* 猫追加 */
	public void AddCat(int num=1){
		float newScale = scale + (spc * num);
		UpdateSize( newScale );
	}

	/* 猫が飛び込んで大きくなる 最初の一回以降のサイズ変更は基本的にこっちを使う*/
	void OnTriggerEnter(Collider col){
		print("!");
		if( !col.tag.Equals("Cat") ){
			return;	
		}

		// 吸収エフェクト
		Instantiate(efx, col.gameObject.transform.position, col.gameObject.transform.rotation);

		// 子猫消滅
		Destroy( col.gameObject );

		AddCat();
	}
}