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
		PawnObj _pawn = BoardManager.instance.pawnObjs [index];
		if (_pawn.data.typeData.side == E_PawnSide.None) {
			if (BoardManager.instance.nowSelectPawn != null) {
				StartCoroutine(BoardManager.instance.AddNowSelectPawn (index));
				GameManager.instance.nowHero.UseCard ();
			}
		}
	}
}
