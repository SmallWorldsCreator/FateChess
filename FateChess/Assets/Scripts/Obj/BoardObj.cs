using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObj : MonoBehaviour {
	[LockInInspector]public int index = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown () {
		BoardManager.instance.AddNowSelectPawn (index);
		GameManager.instance.nowHero.UseCard();
	}
}
