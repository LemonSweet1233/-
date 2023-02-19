using System.Collections;
using UnityEngine;

namespace NewGame
{
    public class PlayerParryState : PlayerAbilityState
    {
        private float parryTime = 0.5f;
        private float time;
        public PlayerParryState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {}
        public override void Enter()
        {
            OnChecks();
            player.Animator.SetTrigger(animBoolName);
            startTime = Time.time;
            Debug.Log(animBoolName);
            isExitingState = false;
            isAnimationFinished = false;
            isAbleilityDone = false;
            time = Time.time + parryTime;
        }

        public override void Exit()
        {
            isExitingState = true;
        }

        public override void LogicUpdate()
        {
            player.SetPlayerFacing(player.FacingDirection != player.PlayerController.NormInputX);

            if (Input.GetMouseButtonDown(1))
            {
                player.Animator.SetTrigger("Parry");
                //击退
            }
            else if(Time.time > time)
            {
                isAbleilityDone = true;
            }

            base.LogicUpdate();
        }
    }
}