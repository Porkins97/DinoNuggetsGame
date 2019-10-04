using UnityEngine;
namespace DinoNuggets.CustomInputManager{
    public class CustomInputManager : MonoBehaviour
    {
        //public InputActionAsset _asset;
        public Vector2 dinoMove {get; private set;}
        public Vector2 dinoArmMove {get; private set;}
        public bool dinoRightHand {get; private set;}
        public bool dinoLeftHand {get; private set;}


        [Range(1, 10)]public int joyStick = 1;
        public SOInputs soInputs;

        void Awake()
        {

        }

        void Update()
        {
            dinoMove = new Vector2(
                Input.GetAxis(System.String.Format("Joy_Horizontal_P{0}", joyStick)), 
                Input.GetAxis(System.String.Format("Joy_Vertical_P{0}", joyStick)));
            Debug.Log(System.String.Format("Joy_Horizontal_P{0}", joyStick));

        }
    }
}