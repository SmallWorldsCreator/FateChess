using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfKill : MonoBehaviour
{
	public float Time=5.0f;

	// Use this for initialization
	void Start ()
	{
		

		StartCoroutine (MyCoroutine ());



	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator MyCoroutine ()
	{
		

		yield return new WaitForSeconds (Time);  //Wait one frame

		Destroy (gameObject);
	}
}
