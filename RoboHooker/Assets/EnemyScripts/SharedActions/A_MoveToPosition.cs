using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_MoveToPosition:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			Vector3 direction = actor.TargetPosition - actor.Position;
			
			//Debug.Log("Moving to: " + actor.TargetPosition);
			Debug.DrawLine(actor.Position, actor.TargetPosition);
			
			// Quick Rotation
			//actor.controller.transform.LookAt(actor.TargetPosition);
			
			// Slow Rotation
			if (Quaternion.Angle(actor.Rotation, actor.TargetRotation) > 5.0f){
				actor.Rotation = Quaternion.Slerp(actor.Rotation, actor.TargetRotation, Time.deltaTime * 
					(actor.TurnSpeed));
			}
			
			Debug.DrawRay(actor.Position, actor.controller.transform.forward);
			
			if (direction.magnitude > 0.2f){
				actor.controller.Move(new Vector3((direction.normalized * (actor.MoveSpeed * 0.01f)).x, 0, 
					(direction.normalized * (actor.MoveSpeed * 0.01f)).z));
			}
			
			if (actor.Animation){
				if (actor.Animation["Walk"]){
					actor.Animation.CrossFade("Walk");
				}
			}
        }
    }
}