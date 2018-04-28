using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/PawnData")]
public class PawnData : ScriptableObject {
	public int level;
	public int cost;
	public int hp;
	public int atk;
	public PawnTypeData typeData;
}