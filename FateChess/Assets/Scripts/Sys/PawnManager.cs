using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnManager : ManagerBase<PawnManager> {
	public PawnObj pawnPrefab;
	public PawnData pawnNull;
	public List<PawnData> playerPawnDataList = new List<PawnData>();
	public List<PawnData> enemyPawnDataList = new List<PawnData>();
	[V2Lable("min", "max")]public Vector2 weekPawnRange;
	[V2Lable("min", "max")]public Vector2 middlePawnRange;
	[V2Lable("min", "max")]public Vector2 strongPawnRange;
//	public override void Awake () {
//
//	}

	void Start () {
		
	}

	void Update () {
		
	}
}
