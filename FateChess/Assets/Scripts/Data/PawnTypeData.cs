using System.Collections;
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
	public E_PawnSide side;
	public string name;
	public AtkRangeData[] atkRanges;

}