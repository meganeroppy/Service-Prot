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
	private const float spc = 0.5f;

	void Awake(){
		audio = GetComponent<AudioSource>();
	}	

	/* サイズを変更 */
	public void UpdateSize(float newScale){
		scale = newScale;

		// ちょっとボヨンとした表現
		transform.DOScale( scale , 1f);
	}

		/* 猫一匹追加 */
		private void AddCat(){
		float newScale = spc;
		UpdateSize( newScale );
	}

		/* 猫が飛び込んで大きくなる 最初の一回行こうのサイズ変更は基本的にこっちを使う*/
		void OnTriggerEnter(Collider col){
		if( !col.tag.Equals("cat") ){
			return;	
		}

		// 吸収エフェクト
		Instantiate(efx, col.gameObject.transform.position, col.gameObject.transform.rotation);

		// 子猫消滅
		Destroy( col.gameObject );

		AddCat();
	}
}