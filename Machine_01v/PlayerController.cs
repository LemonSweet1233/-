using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerController :MonoBehaviour
    {
        private Vector2 wasdInput;
        private Vector2 mouseInput;
        private Camera cameraA;
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        public bool LiftShiftButtenDown { get; private set; }
        public bool Roll { get; private set; }
        public bool SpaceButtenDown { get;private set; }
        public bool ControlButtenDown { get; private set; }
        public bool CKeyButtenDown { get; private set; }

        public Vector2 WordPosition { get; private set; }
        public Vector2Int RollDirectionInt { get; private set; }

        /// Attack
        public bool MouseLeftButtenDown { get; private set; }
        public bool QKeyButtenDown { get; private set; }
        public bool[] AttackInput { get; private set; }
        private int attackCont;
        private void Awake()
        {
            cameraA = Camera.main;
        }
        private void Start()
        {
            attackCont = Enum.GetValues(typeof(EnumAttackInput)).Length;
            AttackInput = new bool[attackCont];
        }
        private void Update()
        {
            WASDInput();

            RunInput();

            SpaceInputDown();

            ControInput();

            MouseInput();

            RollInput();

            GetKeyCButten();

            GetAttackButten();
        }

        private void GetAttackButten()
        {
            AttackInput[(int)EnumAttackInput.primary] = Input.GetMouseButtonDown(0);

            AttackInput[(int)EnumAttackInput.secondary] = Input.GetKeyDown(KeyCode.E);

            AttackInput[(int)EnumAttackInput.parryStance] = Input.GetMouseButtonDown(1);
        }

        private void MouseInput()
        {
            mouseInput = Input.mousePosition;

            mouseInput.x = Mathf.Clamp(mouseInput.x,0f,Screen.width);
            mouseInput.y = Mathf.Clamp(mouseInput.y,0f,Screen.height);

            WordPosition = cameraA.ScreenToWorldPoint(mouseInput);
        }

        private void WASDInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            float vertical = Input.GetAxisRaw("Vertical");

            wasdInput = new Vector2(horizontal, vertical);

            NormInputX = Mathf.RoundToInt(wasdInput.x);

            NormInputY = Mathf.RoundToInt(wasdInput.y);
        }
        private void RunInput()
        {
            LiftShiftButtenDown = Input.GetKey(KeyCode.LeftShift);
        }
        private void RollInput()
        {
            Roll = Input.GetKeyDown(KeyCode.LeftAlt);
        }
        private void SpaceInputDown()
        {
            SpaceButtenDown = Input.GetButtonDown("Jump");
        }
        private void ControInput()
        {
            ControlButtenDown = Input.GetKey(KeyCode.LeftControl);
        }
        private void GetKeyCButten()
        {
            CKeyButtenDown = Input.GetKeyDown(KeyCode.C);
        }
    }
    public enum EnumAttackInput
    {
        primary,
        secondary,
        parryStance
    }
}


