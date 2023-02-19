using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    [CreateAssetMenu(fileName = "PlayerDate_",menuName = "Data/Player Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move")]
        [Range(4, 10)]
        public float walkSpeed = 4f;

        [Range(5, 12)]
        public float runSpeed = 8f;

        [Header("Crouch")]
        public float crouchSpeed = 3f;

        [Header("Sliding Tacke")]
        public float slidingTackeTime = 1f;
        public float slidingTackeVelocityX = 10f;

        [Header("Jump")]
        [Range(6, 12)]
        public float jumpSpeed = 6f;

        public float jumpVelocityY = 15f;

        public int jumpNumber = 2;

        public float jumpTime = 1f;

        public float jumpHeightMultiplier = 0.4f;

        [Header("Roll")]
        public float rollCoolTime = 0.5f;
        public float maxHoldTime = 1f;
        public float holdTimeScale = 0.25f;
        public float rollTime = 0.2f;
        public float rollVelocity = 20f;
        public float drag = 10f;
        public float rollEndYMultiplier = 0.2f;
        public float distVetweenAfterImages = 0.5f;
        public float rollJumpVelocityY = 4f;

        [Header("Check Ground")]
        public float groundCheckRaius;

        public LayerMask isGround;

        [Header("Check Wall")]
        public float wallCheckDistance = 0.2f;

        public float wallSlideVelocityY = 2f;

        public float wallClimbVelocitY = 1.5f;

        public float wallJumpVelocity = 20f;

        public float wallJunpTime = 0.4f;

        [Header("Ledge Climb State")]
        public Vector2 startOffset;
        public Vector2 stopOffset;

        [Header("Attack Down")]
        public float downVelocity = -10f;
    }
}

