using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

public class PlayerInput : MonoBehaviour
{
	public gamepad		playerNumber;
	public GameObject 	gobj;
	public GameObject	deathParts;
	public float		jumpPower			= 20.0f;
	public float		moveSpeed 			= 2.0f;
	public float 		turnSpeed 			= 5.0f;
	public float		attackSpeed			= 1.0f;
	public float		knockbackStrength	= 35.0f;
	public int			maxHP				= 100;
	public int			currentHP			= 100;
	public float		fadeTime			= 3.0f;
	
	
	private GamePadManager buttons;
	private GameObject	socketedWeapon;
	private float		zPlane;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private Vector3 	forward;
	private	Player 		player;
	
	private Vector3 knockback;
	
	void Start(){
		buttons = new GamePadManager(playerNumber);
		zPlane = this.gameObject.transform.position.z;
		controller = this.gobj.GetComponent<CharacterController>();
		attributes = new Dictionary<string, object>();
		forward = new Vector3(1.0f, 0.0f, 0.0f);
		knockback = Vector3.zero;
		
		#region attribute dictionary assignments
		attributes["gameObject"] = gobj;
		attributes["controller"] = controller;
		attributes["deathParts"] = deathParts;
		attributes["socketedWeapon"] = socketedWeapon;
		
		attributes["jumpPower"] = jumpPower;
		attributes["moveSpeed"] = moveSpeed;
		attributes["turnSpeed"] = turnSpeed;
		attributes["targetPosition"] = controller.transform.position;
		attributes["targetRotation"] = controller.transform.rotation;
		attributes["actionTimer"] = 0.0f;
		attributes["attackSpeed"] = attackSpeed;
		attributes["fadeTime"] = fadeTime;
		attributes["maxHP"] = maxHP;
		attributes["currentHP"] = currentHP;
		attributes["animation"] = this.gobj.animation;
		attributes["knockbackStrength"] = knockbackStrength;
		attributes["fadeTime"] = fadeTime;
		attributes["hasAttacked"] = false;
		attributes["forward"] = forward;
		#endregion
		
		player = new Player(attributes); // Pass the dictionary to the enemy
	}
	
	void Update (){
		if (this.gobj != null){
			// Update User Attributes
			this.currentHP 	= this.player.CurrentHP;
			this.maxHP		= this.player.MaxHP;
			player.MoveSpeed = moveSpeed;
			player.TurnSpeed = turnSpeed;
			player.Position  = this.controller.transform.position;
			
			if (knockback.magnitude > 0.1f) { 			//Added knockback to control movement after being hit
				player.Knockback(knockback);
				knockback *= 0.9f;
			}
			else{
				knockback = Vector3.zero;
			}
			
			if (Input.anyKey && !player.isFrozen){
				float direction = Input.GetAxisRaw(buttons.m_MoveAxisX);
				
				if (Mathf.Abs(direction) > 0){
					player.Move(direction);
				}
				else{
					player.Idle();
				}
				if (Input.GetButtonDown(buttons.m_Attack)){
					player.Attack(player.Forward);
				}
				else if (Input.GetButtonDown(buttons.m_jumpButton)){
					player.Jump();
				}
			}
			
			else{
				player.isMoving = false;
			}
			
			player.Update();
			
			if (player.IsGrounded){
				player.AllowJump();
			}
			
			if(this.transform.position.z != this.zPlane){
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zPlane);
			}
		}
	}
	
	public void Idle(){
		player.Idle();
	}
	
	public void Hurt(){
		player.Hurt();
	}
	
    public bool Climbing
    {
		get { return player.isClimbing; }
		set { player.isClimbing = value; }
    }
	
	public bool Frozen{
		get { return player.isFrozen; }
		set { player.isFrozen = value; }
	}
	
	public Vector3 Knockback{
		get { return knockback; }
		set { knockback = value; }
	}
	/*
	public void KillEnemy(){
		this.enemy.CurrentHP = 0;
	}
	
	public void DamageEnemy(int damage){
		this.enemy.CurrentHP -= damage;
		this.enemy.Hurt();
	}
	*/
}