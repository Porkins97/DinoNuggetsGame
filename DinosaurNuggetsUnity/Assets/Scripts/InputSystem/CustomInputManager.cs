using UnityEngine;
using UnityEngine.InputSystem;
namespace DinoNuggets.CustomInputManager{
    public class CustomInputManager : MonoBehaviour
    {
        //public InputActionAsset _asset;
        public Vector2 dinoMove;
        public Vector2 dinoArmMove;
        public bool dinoRightHand;
        public bool dinoLeftHand;

        public void dinoMoveFunc(InputAction.CallbackContext ctx)
        {
            dinoMove = ctx.ReadValue<Vector2>();
        }
    }
}