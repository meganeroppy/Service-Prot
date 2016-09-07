using UnityEngine;
using System.Collections;

/* 全体管理 */
public class GameManager : MonoBehaviour
{
	/* ローカルでの体長 */
	private float height = 0.1f;
	/* ローカルでの吸収した猫の数 */
	private int cat_num = 0;

	/* 前回更新時の猫の数 */
	private int cat_num_prev = 0;

	/* 猫一匹あたりの体長増加量 Height Per Cat */
	const float hpc =0.001f; 

	private CatGenerator generator;

	private CatSymbol cs;

	/* サーバー上の値を取得する頻度 */
	const int RELOAD_INTERVAL = 5;
	float reload_timer = 0;

	APIManager api;
	bool waiting_get = false; // ゲット待機中
	bool waiting_set = false; // セット待機中

	bool reloadOnce = false; // 一度以上リロードしたか？

	void Awake(){
		api = GetComponent<APIManager>();
		generator = GetComponent<CatGenerator>();
		cs = GameObject.Find("CatSymbol").GetComponent<CatSymbol>();
	}

	void Start(){
		StartCoroutine( ReloadCatNum() );
		reload_timer = RELOAD_INTERVAL;
	}

	void Update(){

		if(reload_timer > 0){
			reload_timer -= Time.deltaTime;
			if( reload_timer <= 0 ){
				StartCoroutine( ReloadCatNum() );
				reload_timer = RELOAD_INTERVAL;
			}
		}

		if(Input.GetKeyDown(KeyCode.A)){
			StartCoroutine( AddCat() );
		}	

	}

	public IEnumerator AddCat(){

		if( waiting_set || !reloadOnce){
			yield break;
		}

		cat_num++;

		long data = cat_num;

		waiting_set = true;
		yield return StartCoroutine( api.put_api( data ) );
		waiting_set = false;

		// 直後にgetも行う
		yield return StartCoroutine( ReloadCatNum() );
	}

	IEnumerator ReloadCatNum(){

		if( waiting_get ){
			yield break;
		}

		waiting_get = true;
		yield return StartCoroutine( api.get_api() );
		waiting_get = false;

		if( !int.TryParse( api.GetValue(), out cat_num ) ){
			Debug.LogError("値の取得に失敗");
			yield break;
		}
			
		if( cat_num != cat_num_prev){
			Debug.Log("サーバー上の猫の数に変化がありました。サーバー上の猫の数 = " + cat_num );
			if( reloadOnce ){
				if( cat_num > cat_num_prev ){
					int diff = cat_num - cat_num_prev;
					generator.Make(diff);
				}else{
					Debug.LogWarningFormat("猫の数が減少した時の処理が未定義 猫の数{0}->{1}", cat_num_prev, cat_num);
				}
			}
		}
		cat_num_prev = cat_num;

		if( !reloadOnce ){
			reloadOnce = true;
			cs.AddCat(cat_num);
		}
	}

	/* サイズを更新 */
	public void UpdateSize(int cat_num)
	{
		float add = cat_num * hpc;
		height += add;

		Debug.Log( "猫の体長が" + add + "増加して" + height + " M になった。");
	}
}