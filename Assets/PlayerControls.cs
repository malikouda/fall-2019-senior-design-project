// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

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
            ""id"": ""fbfad7ac-7f89-47d6-8cdd-37517a086dc4"",
            ""actions"": [
                {
                    ""name"": ""move"",
                    ""type"": ""Button"",
                    ""id"": ""d9036dc6-8647-48f2-bb58-3e29d8ad5382"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""x"",
                    ""type"": ""Button"",
                    ""id"": ""504a660e-5d6a-4b9a-ad07-a21fdd6eac8c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""y"",
                    ""type"": ""Button"",
                    ""id"": ""6a22aee9-9269-45ef-b37a-fd2ff5d57a11"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""a"",
                    ""type"": ""Button"",
                    ""id"": ""464983b6-a7fa-49b9-a400-66485c76fb4b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""b"",
                    ""type"": ""Button"",
                    ""id"": ""b1ac0f18-15ed-4297-a76e-4ba8513050ba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ltrigger"",
                    ""type"": ""Button"",
                    ""id"": ""b1315867-4ee0-49be-a0f9-1bc72738fedc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Rtrigger"",
                    ""type"": ""Button"",
                    ""id"": ""e9e0e769-0599-4d59-bdf6-2d490b93bd87"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1c2ca718-ec59-4c29-ac4d-e0d71e557e8a"",
                    ""path"": ""<HID::USB Gamepad >/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cb0a903-4e21-4b78-866d-ea562ee1a7c6"",
                    ""path"": ""<HID::USB Gamepad >/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""x"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48fd75aa-025b-4831-8f3c-c0ab134eb93b"",
                    ""path"": ""<HID::USB Gamepad >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""778e8853-0abe-4d08-8eef-f69c9bb85375"",
                    ""path"": ""<HID::USB Gamepad >/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""a"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77a2f6ab-e7df-4213-9c82-5aab6e9ae163"",
                    ""path"": ""<HID::USB Gamepad >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""b"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63f38d7b-101a-4ea2-9842-cf28b7f7056c"",
                    ""path"": ""<HID::USB Gamepad >/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ltrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c514ebe0-024e-46fc-a3d1-81ea93407e95"",
                    ""path"": ""<HID::USB Gamepad >/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rtrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_move = m_Gameplay.FindAction("move", throwIfNotFound: true);
        m_Gameplay_x = m_Gameplay.FindAction("x", throwIfNotFound: true);
        m_Gameplay_y = m_Gameplay.FindAction("y", throwIfNotFound: true);
        m_Gameplay_a = m_Gameplay.FindAction("a", throwIfNotFound: true);
        m_Gameplay_b = m_Gameplay.FindAction("b", throwIfNotFound: true);
        m_Gameplay_Ltrigger = m_Gameplay.FindAction("Ltrigger", throwIfNotFound: true);
        m_Gameplay_Rtrigger = m_Gameplay.FindAction("Rtrigger", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_move;
    private readonly InputAction m_Gameplay_x;
    private readonly InputAction m_Gameplay_y;
    private readonly InputAction m_Gameplay_a;
    private readonly InputAction m_Gameplay_b;
    private readonly InputAction m_Gameplay_Ltrigger;
    private readonly InputAction m_Gameplay_Rtrigger;
    public struct GameplayActions
    {
        private PlayerControls m_Wrapper;
        public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @move => m_Wrapper.m_Gameplay_move;
        public InputAction @x => m_Wrapper.m_Gameplay_x;
        public InputAction @y => m_Wrapper.m_Gameplay_y;
        public InputAction @a => m_Wrapper.m_Gameplay_a;
        public InputAction @b => m_Wrapper.m_Gameplay_b;
        public InputAction @Ltrigger => m_Wrapper.m_Gameplay_Ltrigger;
        public InputAction @Rtrigger => m_Wrapper.m_Gameplay_Rtrigger;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                x.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                x.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                x.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                y.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                y.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                y.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                a.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                a.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                a.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                b.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                b.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                b.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                Ltrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLtrigger;
                Ltrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLtrigger;
                Ltrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLtrigger;
                Rtrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRtrigger;
                Rtrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRtrigger;
                Rtrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRtrigger;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                move.started += instance.OnMove;
                move.performed += instance.OnMove;
                move.canceled += instance.OnMove;
                x.started += instance.OnX;
                x.performed += instance.OnX;
                x.canceled += instance.OnX;
                y.started += instance.OnY;
                y.performed += instance.OnY;
                y.canceled += instance.OnY;
                a.started += instance.OnA;
                a.performed += instance.OnA;
                a.canceled += instance.OnA;
                b.started += instance.OnB;
                b.performed += instance.OnB;
                b.canceled += instance.OnB;
                Ltrigger.started += instance.OnLtrigger;
                Ltrigger.performed += instance.OnLtrigger;
                Ltrigger.canceled += instance.OnLtrigger;
                Rtrigger.started += instance.OnRtrigger;
                Rtrigger.performed += instance.OnRtrigger;
                Rtrigger.canceled += instance.OnRtrigger;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnLtrigger(InputAction.CallbackContext context);
        void OnRtrigger(InputAction.CallbackContext context);
    }
}
