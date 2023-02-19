using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerCrouchMoveState : PlayerGroundState
    {
        public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){}

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExitingState)
            {
                if (player.PlayerController.NormInputY != -1)
                {
                    stateMachine.ChangeState(player.CrouchState);
                }
            }
        }

        public override void OnChecks()
        {
            base.OnChecks();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            SetVelocityX(playerData.crouchSpeed * player.PlayerController.NormInputX);
        }
    }
}

