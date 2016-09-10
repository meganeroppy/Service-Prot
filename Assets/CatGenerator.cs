using UnityEngine;
using System.Collections;

/* 猫を生成するクラス */
class CatGenerator : MonoBehaviour{

	/* 中心（大きな猫がいる場所） */		
	public Transform center;

	/* 猫が生まれる場所 */
	public Transform[] origin;

	/* 猫 */
	public GameObject catPrefab;

	/* 複数体生成する場合の生成間隔 */
	private const float makeInterval = 0.25f;
	private float timer = 0;

	/* 生成待ちの猫の数 */
	private int cat_pending = 0;

	void Update(){
		if(cat_pending > 0){
			if(timer <= 0){
				ExecuteMake();
				cat_pending--;
				Debug.LogFormat("生成待ちの猫から生成して合計{0}匹", cat_pending);

				timer = makeInterval;
			}else{
				timer -= Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// 生成待ち猫を追加
	/// </summary>
	public void Make(int num, bool isMine=true)
	{
		cat_pending += num;
		Debug.LogFormat("生成待ちの猫に{0}匹追加して合計{1}匹", num, cat_pending);
	}

	/// <summary>
	///  猫を生成
	/// </summary>
	/// <param name="isMine">自分が生成した猫か？</param>
	public void ExecuteMake(bool isMine=false){
		int originPosIdx = isMine ? 0 : Random.Range( 0, origin.Length );

		Transform t = origin[ originPosIdx ];
		GameObject c = Instantiate( catPrefab );
		c.transform.position = t.position;
		c.transform.LookAt( center.position );
	}
}