using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Peppsson
{
	[RequireComponent(typeof (Character2D))]
	public class MoveScript : MonoBehaviour {

		private Character2D m_Character;
		private bool isNinjaRoping = false;
		private Vector2 mousePosition;
		public PointOfSwirlySwirl swirler;

		// Use this for initialization
		void Start () {
			m_Character = GetComponent<Character2D> ();
		}
	
		// Update is called once per frame
		void Update () {
			if(Input.GetMouseButtonDown(0)) {
				RaycastHit2D pointOfDoom;
				Vector3 mouseclick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouseclick.z = 0;
				Debug.Log("characterpos:"+m_Character.transform.position);
				Debug.Log ("mouseclick:"+mouseclick);
				pointOfDoom = Physics2D.Linecast(new Vector2(m_Character.transform.position.x,m_Character.transform.position.y),new Vector2(mouseclick.x,mouseclick.y));
				if(pointOfDoom.collider != null) {
					swirler.Show(pointOfDoom.point);
					isNinjaRoping = true;
					mousePosition = pointOfDoom.point;
				}

			}
			if(Input.GetMouseButtonUp(0)) {
				isNinjaRoping = false;
				swirler.Hide();
			}
		}
		// Update is called once per physicsframe?
		void FixedUpdate() {
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			//m_Character.Move(h);

			if (isNinjaRoping) {
				m_Character.applyForce(mousePosition);
			}
		}
	}
}