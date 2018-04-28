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

	public void SetHp(int p_hp){
		hp = p_hp;

		for (int f = 0; f < 3; f++) {
			// @@@@@@
//			fateList[f].probability = XXXX
		}

		Refrash ();
	}

	public void AddCard(PawnData p_data){
		pawnList.Add (p_data);
		Refrash ();
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
