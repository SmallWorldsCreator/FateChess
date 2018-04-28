using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroObj : MonoBehaviour {
	[LockInInspector]public E_PawnSide side;

	public List<FateData> fateList = new List<FateData>();
	public List<PawnData> pawnList = new List<PawnData>();

	public GameObject[] pawnCards = new GameObject[4];
	public GameObject[] fateCards = new GameObject[3];
	public GameObject usedCard;
	public Sprite[] numbers = new Sprite[11];
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

	public void SelectCard(int p_index){
		nowSelectIndex = p_index;

		Refrash ();
	}

	public void UseCard(){
		if (side == E_PawnSide.Player) {
			GameManager.instance.CanvasAnimator.GetComponent<CardFlyAnim> ().CardFlyOut (nowSelectIndex);
		}
		int usedCount =0;
		for (int i = 0; i < 4; i++) {
			if (!pawnCards [i].transform.parent.gameObject.activeSelf) {
				usedCount += 1;
			}
		}
		Debug.Log (usedCount);
		if (usedCount < nowSelectIndex) {
			pawnList.RemoveAt (nowSelectIndex - usedCount);
		} else {
			pawnList.RemoveAt (nowSelectIndex);
		}
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
		if (GameManager.instance.state != E_GAME_STATE.Init&& side == E_PawnSide.Player) {
			GameManager.instance.CanvasAnimator.gameObject.GetComponent<CardFlyAnim> ().CardFlyIn (pawnList.Count);
			GameManager.instance.CanvasAnimator.Play ("DrawCard");
		}

		AddCard (fateList[0].data);
	}

	public void Refrash(){
		if (GameManager.instance.state != E_GAME_STATE.Init) {
			for (int i = 0; i < 3; i++) {
				fateCards [i].transform.GetChild (0).GetComponent<Image> ().sprite = fateList [i].data.typeData.image;
				fateCards [i].transform.GetChild (1).GetComponent<Text> ().text = fateList [i].probability.ToString () + "%";
				fateCards [i].transform.GetChild (2).GetComponent<Image> ().sprite = numbers [fateList [i].data.hp];
				fateCards [i].transform.GetChild (3).GetComponent<Image> ().sprite = numbers [fateList [i].data.atk];
				fateCards [i].transform.GetChild (4).GetComponent<Image> ().sprite = numbers [fateList [i].data.cost];

			}
		}
		if (GameManager.instance.state != E_GAME_STATE.Init && side == E_PawnSide.Player) {
			int len = pawnList.Count;
			for (int i = 0; i < len; i++) {
				pawnCards [i].transform.GetChild (0).GetComponent<Image> ().sprite = pawnList [i].typeData.image;
				pawnCards [i].transform.GetChild (2).GetComponent<Image> ().sprite = numbers[pawnList[i].cost];
				pawnCards [i].transform.GetChild (3).GetComponent<Image> ().sprite = numbers[pawnList[i].atk];
				pawnCards [i].transform.GetChild (4).GetComponent<Image> ().sprite = numbers[pawnList[i].hp];
				pawnCards [i].transform.GetChild (5).GetComponent<Text> ().text = pawnList [i].typeData.name;
				pawnCards [i].transform.parent.gameObject.SetActive (true);
			}
			for (int i = len; i < 4; i++) {
				pawnCards [i].transform.parent.gameObject.SetActive (false);

			}
		}
		if (nowSelectIndex > -1 && pawnCards[nowSelectIndex].activeSelf && side == E_PawnSide.Player) {
			usedCard.transform.GetChild (0).GetComponent<Image> ().sprite = pawnCards [nowSelectIndex].transform.GetChild (0).GetComponent<Image> ().sprite;
			usedCard.transform.GetChild (2).GetComponent<Image> ().sprite = pawnCards [nowSelectIndex].transform.GetChild (2).GetComponent<Image> ().sprite;
			usedCard.transform.GetChild (3).GetComponent<Image> ().sprite = pawnCards [nowSelectIndex].transform.GetChild (3).GetComponent<Image> ().sprite;
			usedCard.transform.GetChild (4).GetComponent<Image> ().sprite = pawnCards [nowSelectIndex].transform.GetChild (4).GetComponent<Image> ().sprite;
			usedCard.transform.GetChild (5).GetComponent<Text> ().text = pawnCards [nowSelectIndex].transform.GetChild (5).GetComponent<Text> ().text;

		}

	}
}
