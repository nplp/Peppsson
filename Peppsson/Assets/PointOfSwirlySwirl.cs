using UnityEngine;
using System.Collections;

public class PointOfSwirlySwirl : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Show(Vector2 point) {
		this.transform.position = point;
		spriteRenderer.enabled = true;
	}
	public void Hide() {
		spriteRenderer.enabled = false;
	}
}
