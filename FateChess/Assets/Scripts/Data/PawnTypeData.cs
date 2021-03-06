﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PawnSide{
	None = -1,
	Player = 0,
	Enemy = 1
}

[CreateAssetMenu(menuName="ScriptableObject/PawnTypeData")]
public class PawnTypeData : ScriptableObject {
	public Sprite image;
	public Sprite hitImage;
	public E_PawnSide side;
	public string name;
	public RuntimeAnimatorController animeController;
	public AtkRangeData[] atkRanges;
	public Sprite atkimage;
	public GameObject atkEffect;
	public string atkSoundName = "";
	public bool allRange = false;
}