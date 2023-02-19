using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerState 
    {
        protected Player player;
        protected PlayerStateMachine stateMachine;
        protected PlayerData playerData;
        protected float startTime;

        protected string animBoolName;

        protected bool isExitingState;
        protected bool isAnimationFinished;

        public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            OnChecks();
            player.Animator.SetBool(animBoolName, true);
            startTime = Time.time;
            Debug.Log(animBoolName);
            isExitingState = false;
            isAnimationFinished = false;
        }
        public virtual void Exit()
        {
            player.Animator.SetBool(animBoolName,false);
            isExitingState = true;
            player.SetPlayerFacing(false);
        }
        public virtual void LogicUpdate()
        {
            player.SetPlayerFacing(true);
        }
        public virtual void PhysicsUpdate()
        {
            OnChecks();
        }
        public virtual void OnChecks()
        {

        }
        public virtual void AnimationTrigger() { }
        public virtual void AnimationFinishTrigger()=> isAnimationFinished = true;
    }

}
