using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAnimPlay : MonoBehaviour {

	public Transform textTrans;
	private TextMesh texts;
	private int lineCount;
	private float time;

	// Use this for initialization
	void Start () {
		texts = textTrans.GetComponent<TextMesh> ();
		lineCount = texts.text.Split ('\n').Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (time < lineCount + 11.5f) {
			if (Input.GetMouseButton (0)) {
				time += Time.deltaTime * 2f;
				textTrans.position += Vector3.up * Time.deltaTime * 1.1f * 2f;
			} else {
				time += Time.deltaTime;
				textTrans.position += Vector3.up * Time.deltaTime * 1.1f;
			}
		} else {
			
		}
	}
}
