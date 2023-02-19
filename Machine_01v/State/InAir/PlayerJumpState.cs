using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int jumpNumber;
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            jumpNumber = playerData.jumpNumber;
        }

        public override void Enter()
        {
            base.Enter();
            SetVelocityY(playerData.jumpVelocityY);
            isAbleilityDone = true;
            jumpNumber--;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void OnChecks()
        {
            base.OnChecks();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        public bool CanJump()
        {
            if(jumpNumber > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetJumpNumber() => jumpNumber = playerData.jumpNumber;

        public void DecreaseJumpNumber() => jumpNumber --;
    }
}

