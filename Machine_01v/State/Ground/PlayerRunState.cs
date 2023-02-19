using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerRunState : PlayerGroundState
    {
        public PlayerRunState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

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

            if (player.IsGround && !isExitingState)
            {
                if (!player.PlayerController.LiftShiftButtenDown) 
                {
                    if (player.PlayerController.NormInputX != 0f)
                    {
                        stateMachine.ChangeState(player.MoveState);
                    }
                    else if (player.PlayerController.NormInputX == 0f)
                    {
                        stateMachine.ChangeState(player.IdleState);
                    }
                }
                else if (player.PlayerController.CKeyButtenDown && player.PlayerController.LiftShiftButtenDown)
                {
                    stateMachine.ChangeState(player.SlidingTackeState);
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

            SetVelocityX(playerData.runSpeed * player.PlayerController.NormInputX);
        }
    }
}

