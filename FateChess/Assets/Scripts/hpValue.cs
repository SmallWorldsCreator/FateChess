using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpValue : MonoBehaviour {
	public Slider HPbar;

	public void SetHp(float hp){
		HPbar.value += hp;
		GameManager.instance.player.SetHp ((int)Mathf.Floor (HPbar.value * 10F));
	}

	public void SetFinished(){
		GameManager.instance.ChangeState (E_GAME_STATE.EnemyMove);
	}
}
