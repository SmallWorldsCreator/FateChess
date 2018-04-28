using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlyAnim : MonoBehaviour {

	public GameObject[] Cards = new GameObject[4];
	public Animator FlyOutCard;

	private Vector3 StartFlyPosition = new Vector3 (800, 300, 0);
	private Vector3 EndFlyPosition = new Vector3 (128-50, -128+50, 0);
	private int cardnum;
	private bool TurnStarting=false;
	private float step=0;

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			CardFlyIn (3);
		}
		if (TurnStarting) {
			step += 2*Time.deltaTime;
			Cards [cardnum].GetComponent<RectTransform> ().localPosition
			= Vector3.Lerp (StartFlyPosition, EndFlyPosition, step);
			if (step>=1) {
				step = 0;
				TurnStarting = false;
			}
		}
	}

	public void CardFlyIn(int CardNum){
		Cards [CardNum].transform.parent.gameObject.SetActive (true);
		Cards [CardNum].GetComponent<RectTransform> ().position = StartFlyPosition;
		cardnum = CardNum;
		TurnStarting = true;
	}

	public void CardFlyOut(int CardNum){
		Cards [cardnum].GetComponent<RectTransform> ().position = EndFlyPosition;
		step = 0;
		TurnStarting = false;
		Cards [CardNum].transform.parent.gameObject.SetActive (false);
		FlyOutCard.Play ("FlyOut");
	}
}
