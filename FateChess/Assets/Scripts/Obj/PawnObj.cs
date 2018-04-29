using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnObj : MonoBehaviour {
	[NullAlarm]public Animator anime;
	[LockInInspector]public int hp = -1;
	[LockInInspector]public int index = -1;
	[NullAlarm]public SpriteRenderer imageRender, atkRender, hpRender;
	[LockInInspector]public PawnData data = null;
	[LockInInspector]public bool alreadyMove = false;
	public float moveSpeed = 1;
	Vector3 pos;

	void Start () {
		pos = transform.position;

	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);

	}

	public void SetNewPawnData (PawnData p_data) {
		SetPawnData(p_data);
		SetHp(p_data.hp);
		anime.Play("Idle", -1, 0);
	}

	public void SetPawnData (PawnData p_data) {
		data = p_data;
		imageRender.sprite = data.typeData.image;
		atkRender.enabled = (p_data.atk >= 0);
		hpRender.enabled = (p_data.hp >= 0);

		atkRender.sprite = PawnManager.instance.numberSprites[Mathf.Clamp (data.atk, 0, 10)];

	}

	public void SetHp (int p_hp) {
		hp = p_hp;
		hp = Mathf.Clamp (hp, 0, 10);

		hpRender.sprite = PawnManager.instance.numberSprites[hp];
	}
}
