using UnityEngine;
using System.Collections;

public class ScreenButton : MonoBehaviour {

	[SerializeField]
	GameManager gm;

	public void Press()
	{
		gm.UserInput();
	}
}
