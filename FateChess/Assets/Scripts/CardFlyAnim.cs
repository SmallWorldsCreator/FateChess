using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlyAnim : MonoBehaviour {

	public GameObject[] Cards = new GameObject[4];

	private Vector3 StartFlyPosition = new Vector3 (1100, 300, 0);
	private Vector3 EndFlyPosition = new Vector3 (128-50, -128+50, 0);
	private int cardnum;
	private bool TurnStarting=false;
	private float step=0;
	private int usedcardNum=0;

	void Update(){
		if (TurnStarting) {
			step += 2*Time.deltaTime;
			Cards [cardnum].GetComponent<RectTransform> ().localPosition
			= Vector3.Lerp (StartFlyPosition-new Vector3(cardnum*256,0,0), EndFlyPosition, step);
			if (step>=1) {
				step = 0;
				TurnStarting = false;
			}
		}
	}

	public void CardFlyIn(int CardNum){
//		Cards [CardNum].transform.parent.gameObject.SetActive (true);
		Cards [CardNum].GetComponent<RectTransform> ().position = StartFlyPosition;
		cardnum = CardNum;
		TurnStarting = true;
	}

	public void CardFlyOut(int CardNum){
		//Cards [cardnum].GetComponent<RectTransform> ().position = EndFlyPosition;
		step = 0;
		TurnStarting = false;
//		Cards [CardNum].transform.parent.gameObject.SetActive (false);
		GetComponent<Animator>().Play ("CardDrop", -1, 0);
	}
	public void nowSelected(int cardnum){
		GameManager.instance.player.SelectCard (cardnum);
		Debug.Log ("selecting:" + cardnum);
	}
}
