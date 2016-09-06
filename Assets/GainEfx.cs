using UnityEngine;
using System.Collections;

/* 猫が吸収されたときのエフェクト */
class GainEfx : MonoBehaviour{
	void Start(){
		Destroy( gameObject , 2f );
	}
}