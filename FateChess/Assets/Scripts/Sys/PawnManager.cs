using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnManager : ManagerBase<PawnManager> {
	[NullAlarm]public PawnObj pawnPrefab;
	[NullAlarm]public PawnData pawnNull;
	[NullAlarm]public PawnData pawnBasePlayer;
	[NullAlarm]public PawnData pawnBaseEnemy;
	[NullAlarm]public List<PawnData> playerPawnDataList = new List<PawnData>();
	[NullAlarm]public List<PawnData> enemyPawnDataList = new List<PawnData>();
	[V2Lable("min", "max")]public Vector2[] pawnRanges;
//	public override void Awake () {
//
//	}
	public PawnData GetRamdonData (E_PawnSide p_side) {
		List<PawnData> _list = ((p_side == E_PawnSide.Player) ? playerPawnDataList : enemyPawnDataList);
		return _list[Random.Range(0, _list.Count)];
	}
	public FateData GetFateData (E_PawnSide p_side ,int p_fate, int p_index) {
		// @@@@@
		List<PawnData> _list = ((p_side == E_PawnSide.Player) ? playerPawnDataList : enemyPawnDataList);

		FateData _data = new FateData (_list[Random.Range(0, _list.Count)]);
		return _data;
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
