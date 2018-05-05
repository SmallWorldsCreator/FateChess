using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnManager : ManagerBase<PawnManager> {
	[NullAlarm]public Sprite[] numberSprites;
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
		
		List<PawnData> _list = ((p_side == E_PawnSide.Player) ? playerPawnDataList : enemyPawnDataList);

		Vector2 _range = pawnRanges[p_index];
		List<PawnData> _inRangeDataList = new List<PawnData>();

		foreach (PawnData _data in _list) {
			if (IntExtend.isInRange (_data.cost, (int)_range.x, (int)_range.y)) {
				_inRangeDataList.Add (_data);
			}
		}

		return new FateData (_inRangeDataList[Random.Range(0, _inRangeDataList.Count)]);;
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
