using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnObj : MonoBehaviour {
	[LockInInspector]public int index = -1;
	[NullAlarm]public SpriteRenderer imageRender, atkRender, hpRender;
	PawnData _data = null;
	void Start () {
		
	}

	void Update () {
		
	}

	public void SetPawnData (PawnData p_data) {
		_data = p_data;
		imageRender.sprite = _data.typeData.image;
		atkRender.enabled = (p_data.atk >= 0);
		hpRender.enabled = (p_data.hp >= 0);
	}
}
