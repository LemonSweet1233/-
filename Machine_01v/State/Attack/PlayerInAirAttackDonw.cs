using System.Collections;
using UnityEngine;

namespace NewGame
{
    public class PlayerInAirAttackDonw : PlayerAbilityState
    {
        public PlayerInAirAttackDonw(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
        public override void Enter()
        {
            OnChecks();
            player.Animator.SetTrigger(animBoolName);
            startTime = Time.time;
            Debug.Log(animBoolName);
            isExitingState = false;
            isAnimationFinished = false;
            isAbleilityDone = false;
        }

        public override void Exit()
        {
            isExitingState = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.SetPlayerFacing(false);

            SetVelocityY(playerData.downVelocity);

            isAbleilityDone = true;
        }
    }
}