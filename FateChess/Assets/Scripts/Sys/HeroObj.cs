using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroObj : MonoBehaviour {
	[LockInInspector]public E_PawnSide side;

	public List<FateData> fateList = new List<FateData>();
	public List<PawnData> pawnList = new List<PawnData>();
	int nowSelectIndex = -1;

	const int maxValue = 10;
	public int hp = 5;
	public int fate{
		get{
			return maxValue - hp;
		}
	}

	public void Init(E_PawnSide p_side){
		side = p_side;
	}

//	public override void Awake () {
//
//	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 probability0 = new Vector3(7, 3, 0);
	Vector3 probability1 = new Vector3(1.5f, 7, 1.5f);
	Vector3 probability2 = new Vector3(0, 3, 7);

	public void SetHp(int p_hp){
		hp = p_hp;

		Vector3 _probability;
		if (fate < 5) {
			_probability = Vector3.MoveTowards (probability0, probability1, fate/5);
		} else {
			_probability = Vector3.MoveTowards (probability1, probability2, (fate-5)/5);
		}

		fateList [0].probability = _probability.x;
		fateList [1].probability = _probability.y;
		fateList [2].probability = _probability.z;


		Refrash ();
	}

	public void AddCard(PawnData p_data){
		if (pawnList.Count < 4) {
			pawnList.Add (p_data);
			Refrash ();
		} else {

		}
	}

	[Button("SelectAndUseCard")]public int selectAndUseCardBut;
	public void SelectAndUseCard(int p_index){
		SelectCard (p_index);
		UseCard ();
	}

	public void SelectCard(int p_index){
		nowSelectIndex = p_index;

		Refrash ();
	}

	public void UseCard(){
		pawnList.RemoveAt (nowSelectIndex);
		Refrash ();
	}

	public void EnemyUseCard(){
		// @@@@@@
		BoardManager.instance.SelectPawn (fateList[0].data);
		BoardManager.instance.AddNowSelectPawn (Random.Range(0,25));
	}

	public void GenerateNewFate(){
		fateList.Clear();

		for (int f = 0; f < 3; f++) {
			FateData _data = PawnManager.instance.GetFateData (side, fate, f);
			fateList.Add (_data);
		}

		Refrash ();
	}

	public void DrawCard(){
		// @@@@@@
//		fateList[f].probability


		AddCard (fateList[0].data);
	}

	public void Refrash(){

	}
}
