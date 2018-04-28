using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : ManagerBase<BoardManager> {
	public Vector2 size;
	int pawnCount = 0;
	[NullAlarm]public Transform pawnTop;
	[NullAlarm]public PawnObj[] pawnObjs;
	[NullAlarm]public BoardObj[] boardObjs;
//	public override void Awake () {
//
//	}
	void Start () {
		pawnCount = pawnObjs.Length;

		for (int _y = 0; _y < (int)size.y; _y++) {
			for (int _x = 0; _x < (int)size.x; _x++) {
				int _index = _x + (_y*5);
				pawnObjs [_index].SetPawnData (PawnManager.instance.pawnNull);
				pawnObjs [_index].index = _index;
				boardObjs [_index].index = _index;
			}
		}
	}

	void Update () {
		
	}

	public PawnData nowSelectPawn = null;
	public void SelectPawn (PawnData p_data) {
		nowSelectPawn = p_data;
	}

	public void AddNowSelectPawn (int p_index) {
		if (nowSelectPawn != null) {
			AddPawn (nowSelectPawn, p_index);
		}
	}

	public void AddPawn (PawnData p_data, int p_index) {
		pawnObjs [p_index].SetPawnData (p_data);
	}
}
