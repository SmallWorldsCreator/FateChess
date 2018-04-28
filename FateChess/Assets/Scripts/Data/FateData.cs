using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FateData {
	public int probability = 0;
	public PawnData data;

	public FateData(PawnData p_data){
		data = p_data;
	}
}
