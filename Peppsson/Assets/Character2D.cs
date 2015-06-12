using System;
using UnityEngine;

namespace Peppsson
{
	public class Character2D : MonoBehaviour
	{
		[SerializeField] private float m_MaxSpeedY = 5f; 
		[SerializeField] private float m_MaxSpeedX = 5f; // The fastest the player can travel in the x axis.
		[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
		[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
		[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
		[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
		
		private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
		const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
		private bool m_Grounded;            // Whether or not the player is grounded.
		private Transform m_CeilingCheck;   // A position marking where to check for ceilings
		const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
		private Animator m_Anim;            // Reference to the player's animator component.
		private Rigidbody2D m_Rigidbody2D;
		private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		
		private void Awake()
		{
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		
		private void FixedUpdate()
		{
			float newX = m_Rigidbody2D.velocity.x;
			float newY = m_Rigidbody2D.velocity.y;
			if (Math.Abs (m_Rigidbody2D.velocity.x) > m_MaxSpeedX) {
				if(m_Rigidbody2D.velocity.x > 0) {
					newX = m_MaxSpeedX;
				} else {
					newX = -m_MaxSpeedX;
				}
			}
			if (Math.Abs (m_Rigidbody2D.velocity.y) > m_MaxSpeedY) {
				if(m_Rigidbody2D.velocity.y > 0) {
					newY = m_MaxSpeedY;
				} else {
					newY = -m_MaxSpeedY;
				}
			}
			m_Rigidbody2D.velocity = new Vector2 (newX, newY);
		}

		public void applyForce(Vector3 forcePosition) {
			Vector2 vector = new Vector2 (forcePosition.x, forcePosition.y);
			Vector2 forceToApply = vector - m_Rigidbody2D.position;
			forceToApply.Scale(new Vector2 (1.5f*Math.Abs(Physics2D.gravity.y), 1.5f*Math.Abs(Physics2D.gravity.y)));
			m_Rigidbody2D.AddForce(forceToApply);
		}
		
		public void Move(float move)
		{
				// Move the character
				m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeedX, m_Rigidbody2D.velocity.y);
				
				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}

		}

		
		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;
			
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}
