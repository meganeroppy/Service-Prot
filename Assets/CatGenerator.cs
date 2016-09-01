﻿using UnityEngine;
using System.Collections;

/* 猫を生成するクラス */
class CatGenerator : MonoBehaviour{

	/* 中心（大きな猫がいる場所） */		
	public Transform center;

	/* 猫が生まれる場所 */
	public Transform[] origin;

	/* 猫 */
	public GameObject catPrefab;

	/// <summary>
	///  猫を生成
	/// </summary>
	/// <param name="isMine">自分が生成した猫か？</param>
	public void Make(bool isMine=true){
		int idx = isMine ? 0 : Random.Range( 1, origin.Length );
		Transform t = origin[ idx ];
		GameObject c = Instantiate( catPrefab );
		c.transform.position = t.position;
		c.transform.LookAt( center.position );
	}
}