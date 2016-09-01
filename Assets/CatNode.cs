using UnityEngine;
using System.Collections;


/* １クリック毎に画面に登場する猫 */
class CatNode : MonoBehaviour{

	/* 効果音 */
	public AudioClip[] se;

	/* 移動速度 */
	private float spd;
	private const float SPD_MAX = 5f;
	private const float SPD_MIN = 2f;

	// 猫の大きさリスト　0番目は子猫
	private float[] scaleList = new float[5]{0.65f, 0.9f, 1f, 1.1f, 1.2f};

	void Start(){
		// 移動速度を定義
		spd = Random.Range(SPD_MIN, SPD_MAX);

		int idx = Random.Range(0, scaleList.Length);
		float scale = scaleList[ idx ];
		transform.localScale *= scale;

		idx = idx == 0 ? 0 : Random.Range(1, se.Length);

		// 登場時SEを再生
//		GetComponent<AudioSource>().PlayOneShot( se[idx] );

		// 歩きアニメを再生	
		GetComponent<Animator>().SetInteger("Status", 2);
	}

	void Update(){
		// 移動
		transform.Translate( 0, 0, spd * Time.deltaTime );
	}
}