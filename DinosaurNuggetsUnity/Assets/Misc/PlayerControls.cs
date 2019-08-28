// GENERATED AUTOMATICALLY FROM 'Assets/Misc/PlayerControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""2b5224c5-1e5b-4a87-a3f2-b25fc4a4a43c"",
            ""actions"": [
                {
                    ""name"": ""Grow"",
                    ""type"": ""Button"",
                    ""id"": ""db8c2117-c683-4617-9858-0c7eaa732150"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""98b8a0f7-d89a-4c5c-9fd0-92d3f64ae720"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arms"",
                    ""type"": ""Button"",
                    ""id"": ""0ff17543-f65e-4f7a-9e24-31ef0c8cc021"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be66f344-21ca-4e26-880b-c11bca2e7d34"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25606353-4785-42d2-bf3f-d9e2bac6f4ad"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fc4869f-bf3e-48a1-be18-ed2b77fed8a5"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arms"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.GetActionMap("Gameplay");
        m_Gameplay_Grow = m_Gameplay.GetAction("Grow");
        m_Gameplay_Move = m_Gameplay.GetAction("Move");
        m_Gameplay_Arms = m_Gameplay.GetAction("Arms");
    }

    ~PlayerControls()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Grow;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Arms;
    public struct GameplayActions
    {
        private PlayerControls m_Wrapper;
        public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grow => m_Wrapper.m_Gameplay_Grow;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Arms => m_Wrapper.m_Gameplay_Arms;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Grow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrow;
                Grow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrow;
                Grow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrow;
                Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Arms.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms;
                Arms.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms;
                Arms.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Grow.started += instance.OnGrow;
                Grow.performed += instance.OnGrow;
                Grow.canceled += instance.OnGrow;
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                Arms.started += instance.OnArms;
                Arms.performed += instance.OnArms;
                Arms.canceled += instance.OnArms;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnGrow(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnArms(InputAction.CallbackContext context);
    }
}
