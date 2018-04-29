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
	public hpValue hpvalue;
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

	Vector3 probability0 = new Vector3(70, 30, 0 );
	Vector3 probability1 = new Vector3(15, 70, 15);
	Vector3 probability2 = new Vector3( 0, 30, 70);

	public void SetHpForAnim(float p_hp){
		SetHp (Mathf.FloorToInt(p_hp*10));
	}

	public void SetHp(int p_hp){
		hp = p_hp;

		Vector3 _probability;
		if (fate < 5) {
			_probability = Vector3.MoveTowards (probability0, probability1, fate/5);
		} else {
			_probability = Vector3.MoveTowards (probability1, probability2, (fate-5)/5);
		}

		fateList [0].probability = (int)_probability.x;
		fateList [1].probability = (int)_probability.y;
		fateList [2].probability = (int)_probability.z;


		Refrash ();

	}

	public bool AddCard(PawnData p_data){
		if (pawnList.Count < 4) {
			pawnList.Add (p_data);
			Refrash ();
			return true;
		} else {
			return false;
		}
	}

	[Button("SelectAndUseCard")]public int selectAndUseCardBut;
	public void SelectAndUseCard(int p_index){
		SelectCard (p_index);
		UseCard ();
	}

	public void SelectCard(int p_index){
		nowSelectIndex = p_index;
		BoardManager.instance.SelectPawn (pawnList[p_index]);
		Refrash ();
	}

	public void UseCard(){
		if (nowSelectIndex < 0) {
			return;
		}

		if (side == E_PawnSide.Player) {
			GameManager.instance.CanvasAnimator.GetComponent<CardFlyAnim> ().CardFlyOut (nowSelectIndex);
		}

		pawnList.RemoveAt (nowSelectIndex);
		nowSelectIndex = -1;
		Refrash ();
	}

	public IEnumerator EnemyUseCard(){
		// @@@@@@
		BoardManager.instance.SelectPawn (fateList[0].data);
		yield return StartCoroutine(BoardManager.instance.AddNowSelectPawn (Random.Range(0,25)));
		GameManager.instance.ChangeState(E_GAME_STATE.Draw);
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

		if ((GameManager.instance.state != E_GAME_STATE.Init) && (side == E_PawnSide.Player)) {
			if (AddCard (fateList [0].data)) {
				GameManager.instance.CanvasAnimator.gameObject.GetComponent<CardFlyAnim> ().CardFlyIn (pawnList.Count-1);
				GameManager.instance.CanvasAnimator.Play ("DrawCard", -1, 0);
			}
		}

	}

	public void Refrash(){
		if (GameManager.instance.state != E_GAME_STATE.Init) {
			for (int i = 0; i < 3; i++) {
				fateCards [i].transform.GetChild (0).GetComponent<Image> ().sprite = fateList [i].data.typeData.image;
				fateCards [i].transform.GetChild (1).GetComponent<Text> ().text = fateList [i].probability.ToString () + "%";
				fateCards [i].transform.GetChild (2).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites [fateList [i].data.hp];
				fateCards [i].transform.GetChild (3).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites [fateList [i].data.atk];
				fateCards [i].transform.GetChild (4).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites [fateList [i].data.cost];

			}
		}
		if (GameManager.instance.state != E_GAME_STATE.Init && side == E_PawnSide.Player) {
			int len = pawnList.Count;
			for (int i = 0; i < len; i++) {
				pawnCards [i].transform.GetChild (0).GetComponent<Image> ().sprite = pawnList [i].typeData.image;
				pawnCards [i].transform.GetChild (2).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites[pawnList[i].cost];
				pawnCards [i].transform.GetChild (3).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites[pawnList[i].atk];
				pawnCards [i].transform.GetChild (4).GetComponent<Image> ().sprite = PawnManager.instance.numberSprites[pawnList[i].hp];
				pawnCards [i].transform.GetChild (5).GetComponent<Text> ().text = pawnList [i].typeData.name;
				if (pawnList[i].cost>hp) {
					foreach (Image img in pawnCards[i].GetComponentsInChildren<Image>()) {
						img.color = Color.gray;
					}
					pawnCards [i].GetComponent<Image> ().color = Color.clear;
				} else {
					foreach (Image img in pawnCards[i].GetComponentsInChildren<Image>()) {
						img.color = Color.white;
					}	
					pawnCards [i].GetComponent<Image> ().color = Color.clear;
				}
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
		hpvalue.SetBarValue (((float)hp)/10f);
	}
}
