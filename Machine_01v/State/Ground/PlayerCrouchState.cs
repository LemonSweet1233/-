using UnityEngine;

namespace NewGame
{
    public class PlayerCrouchState : PlayerGroundState
    {
        public PlayerCrouchState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

            if(player.PlayerController.NormInputY ==-1 && player.PlayerController.NormInputX != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(player.PlayerController.NormInputY != -1 && !player.IsHanding)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

