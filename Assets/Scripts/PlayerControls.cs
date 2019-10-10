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
        },
        {
            ""name"": ""Gameplay(Keyboard)"",
            ""id"": ""41949ab1-b0cb-45f6-9f2a-88d1b13d9264"",
            ""actions"": [
                {
                    ""name"": ""move"",
                    ""type"": ""Button"",
                    ""id"": ""77fe1f41-7b8a-4f3f-9a6b-94c9a821ab97"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""x"",
                    ""type"": ""Button"",
                    ""id"": ""54bfc992-fb6e-4403-81ea-5b00cdfcbbdb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""y"",
                    ""type"": ""Button"",
                    ""id"": ""83576b09-3814-4785-a1cf-df06a5935296"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""a"",
                    ""type"": ""Button"",
                    ""id"": ""28bafbc2-297a-40b9-9b6f-71c5f95c28b8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""b"",
                    ""type"": ""Button"",
                    ""id"": ""88dfd09a-be26-4235-85dd-492491683fc0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""70f786c0-201e-4afb-9aa9-e3e8db04d82f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c9fd85be-950e-4590-9e4b-19826a8c3bfb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a7c43165-42b9-43a2-891e-3cbc9c7bb046"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f3f27884-0e43-4e4b-8c49-bdba72ef107d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1e077770-a4f0-484f-b995-8a4c9316ee08"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e7ca9163-6c0e-4c5e-89fb-37326c6bb938"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""x"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e0aea55-e99c-4dc7-a0a1-67b153bcb454"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2dbd20a-f79f-4fc1-9de4-1e807d209626"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""a"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3358f9fe-58fd-4c20-8b83-b63acbea7cb9"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""b"",
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
        // Gameplay(Keyboard)
        m_GameplayKeyboard = asset.FindActionMap("Gameplay(Keyboard)", throwIfNotFound: true);
        m_GameplayKeyboard_move = m_GameplayKeyboard.FindAction("move", throwIfNotFound: true);
        m_GameplayKeyboard_x = m_GameplayKeyboard.FindAction("x", throwIfNotFound: true);
        m_GameplayKeyboard_y = m_GameplayKeyboard.FindAction("y", throwIfNotFound: true);
        m_GameplayKeyboard_a = m_GameplayKeyboard.FindAction("a", throwIfNotFound: true);
        m_GameplayKeyboard_b = m_GameplayKeyboard.FindAction("b", throwIfNotFound: true);
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

    // Gameplay(Keyboard)
    private readonly InputActionMap m_GameplayKeyboard;
    private IGameplayKeyboardActions m_GameplayKeyboardActionsCallbackInterface;
    private readonly InputAction m_GameplayKeyboard_move;
    private readonly InputAction m_GameplayKeyboard_x;
    private readonly InputAction m_GameplayKeyboard_y;
    private readonly InputAction m_GameplayKeyboard_a;
    private readonly InputAction m_GameplayKeyboard_b;
    public struct GameplayKeyboardActions
    {
        private PlayerControls m_Wrapper;
        public GameplayKeyboardActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @move => m_Wrapper.m_GameplayKeyboard_move;
        public InputAction @x => m_Wrapper.m_GameplayKeyboard_x;
        public InputAction @y => m_Wrapper.m_GameplayKeyboard_y;
        public InputAction @a => m_Wrapper.m_GameplayKeyboard_a;
        public InputAction @b => m_Wrapper.m_GameplayKeyboard_b;
        public InputActionMap Get() { return m_Wrapper.m_GameplayKeyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayKeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayKeyboardActions instance)
        {
            if (m_Wrapper.m_GameplayKeyboardActionsCallbackInterface != null)
            {
                move.started -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnMove;
                move.performed -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnMove;
                move.canceled -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnMove;
                x.started -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnX;
                x.performed -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnX;
                x.canceled -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnX;
                y.started -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnY;
                y.performed -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnY;
                y.canceled -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnY;
                a.started -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnA;
                a.performed -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnA;
                a.canceled -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnA;
                b.started -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnB;
                b.performed -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnB;
                b.canceled -= m_Wrapper.m_GameplayKeyboardActionsCallbackInterface.OnB;
            }
            m_Wrapper.m_GameplayKeyboardActionsCallbackInterface = instance;
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
            }
        }
    }
    public GameplayKeyboardActions @GameplayKeyboard => new GameplayKeyboardActions(this);
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
    public interface IGameplayKeyboardActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
    }
}
