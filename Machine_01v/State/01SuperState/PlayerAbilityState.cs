using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool isAbleilityDone;
        public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
        public override void OnChecks()
        {
            base.OnChecks();
        }
        public override void Enter()
        {
            base.Enter();
            isAbleilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAbleilityDone)
            {
                if (player.IsGround)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        protected void SetVelocityY(float velocityY)
        {
            player.SetVelocityY(velocityY);
        }

        protected void SetVelocityX(float velocityX)
        {
            player.SetVelocityX(velocityX);
        }
    }
}

