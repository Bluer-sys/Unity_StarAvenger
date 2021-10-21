// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""4d0270be-0425-483e-9034-f498761dc21f"",
            ""actions"": [
                {
                    ""name"": ""KeyboardMove"",
                    ""type"": ""Value"",
                    ""id"": ""7196ba65-785e-4770-b1da-384f9aa67a35"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""e96b27fe-e2b9-40cf-ba34-34023563e20f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b1c5ba32-2c54-4c76-b40d-f562ca7ec19f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""239d8b38-e301-4a32-901d-b67832b0c029"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bed0f1f5-4608-4ce1-9154-c265819f7db8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0d4d3ea5-2d28-4786-87e4-69d7276a4fcc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b1f0b780-9346-46f5-9465-cdda947dd111"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""488dae44-d0a7-4011-b7f0-2844cf2b6a9e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Shooting"",
            ""id"": ""3a878a98-c247-400f-a2f5-e52ae7db779a"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""03a1d731-bad6-4ba5-995c-6b431e720480"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""2dc6ae8f-1123-43f6-b0c2-aaf160a275c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Button"",
                    ""id"": ""60e07771-60ca-42ec-a95a-2d9a0ae30f95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon3"",
                    ""type"": ""Button"",
                    ""id"": ""2309aa4f-8dfa-4370-b1e0-e57c6f0e7cf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon4"",
                    ""type"": ""Button"",
                    ""id"": ""4a8d740f-9476-4199-bfb4-8c8e641089fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""754fb8db-5224-4862-968e-16fae780f76e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd3b4cf0-078f-4a0e-800b-572b116e199e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c082abd-1ab6-442b-8cba-b32850b1aaca"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec1b39d0-8b48-4f81-a407-a15c7ee12fdc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""209d65f0-4099-4a2a-878e-bbf7308db425"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Weapon3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""977d89e5-737f-4015-bb7b-e6cf40d4261c"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Weapon4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""0e651891-65f5-4f24-bc53-dcd1fceea9f9"",
            ""actions"": [
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""039f5749-1c14-4a31-b1dc-11dd6ce5cd97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7420c08e-8c26-4243-b747-a31ea2a9e36d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_KeyboardMove = m_Movement.FindAction("KeyboardMove", throwIfNotFound: true);
        m_Movement_MouseMove = m_Movement.FindAction("MouseMove", throwIfNotFound: true);
        // Shooting
        m_Shooting = asset.FindActionMap("Shooting", throwIfNotFound: true);
        m_Shooting_Shoot = m_Shooting.FindAction("Shoot", throwIfNotFound: true);
        m_Shooting_Weapon1 = m_Shooting.FindAction("Weapon1", throwIfNotFound: true);
        m_Shooting_Weapon2 = m_Shooting.FindAction("Weapon2", throwIfNotFound: true);
        m_Shooting_Weapon3 = m_Shooting.FindAction("Weapon3", throwIfNotFound: true);
        m_Shooting_Weapon4 = m_Shooting.FindAction("Weapon4", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Menu = m_UI.FindAction("Menu", throwIfNotFound: true);
    }

    public void Dispose()
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_KeyboardMove;
    private readonly InputAction m_Movement_MouseMove;
    public struct MovementActions
    {
        private @PlayerInput m_Wrapper;
        public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @KeyboardMove => m_Wrapper.m_Movement_KeyboardMove;
        public InputAction @MouseMove => m_Wrapper.m_Movement_MouseMove;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @KeyboardMove.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnKeyboardMove;
                @KeyboardMove.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnKeyboardMove;
                @KeyboardMove.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnKeyboardMove;
                @MouseMove.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseMove;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @KeyboardMove.started += instance.OnKeyboardMove;
                @KeyboardMove.performed += instance.OnKeyboardMove;
                @KeyboardMove.canceled += instance.OnKeyboardMove;
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Shooting
    private readonly InputActionMap m_Shooting;
    private IShootingActions m_ShootingActionsCallbackInterface;
    private readonly InputAction m_Shooting_Shoot;
    private readonly InputAction m_Shooting_Weapon1;
    private readonly InputAction m_Shooting_Weapon2;
    private readonly InputAction m_Shooting_Weapon3;
    private readonly InputAction m_Shooting_Weapon4;
    public struct ShootingActions
    {
        private @PlayerInput m_Wrapper;
        public ShootingActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Shooting_Shoot;
        public InputAction @Weapon1 => m_Wrapper.m_Shooting_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_Shooting_Weapon2;
        public InputAction @Weapon3 => m_Wrapper.m_Shooting_Weapon3;
        public InputAction @Weapon4 => m_Wrapper.m_Shooting_Weapon4;
        public InputActionMap Get() { return m_Wrapper.m_Shooting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShootingActions set) { return set.Get(); }
        public void SetCallbacks(IShootingActions instance)
        {
            if (m_Wrapper.m_ShootingActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                @Weapon1.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon1;
                @Weapon1.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon1;
                @Weapon1.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon1;
                @Weapon2.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon2;
                @Weapon2.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon2;
                @Weapon2.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon2;
                @Weapon3.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon3;
                @Weapon3.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon3;
                @Weapon3.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon3;
                @Weapon4.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon4;
                @Weapon4.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon4;
                @Weapon4.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnWeapon4;
            }
            m_Wrapper.m_ShootingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Weapon1.started += instance.OnWeapon1;
                @Weapon1.performed += instance.OnWeapon1;
                @Weapon1.canceled += instance.OnWeapon1;
                @Weapon2.started += instance.OnWeapon2;
                @Weapon2.performed += instance.OnWeapon2;
                @Weapon2.canceled += instance.OnWeapon2;
                @Weapon3.started += instance.OnWeapon3;
                @Weapon3.performed += instance.OnWeapon3;
                @Weapon3.canceled += instance.OnWeapon3;
                @Weapon4.started += instance.OnWeapon4;
                @Weapon4.performed += instance.OnWeapon4;
                @Weapon4.canceled += instance.OnWeapon4;
            }
        }
    }
    public ShootingActions @Shooting => new ShootingActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Menu;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_UI_Menu;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Menu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnKeyboardMove(InputAction.CallbackContext context);
        void OnMouseMove(InputAction.CallbackContext context);
    }
    public interface IShootingActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
        void OnWeapon3(InputAction.CallbackContext context);
        void OnWeapon4(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMenu(InputAction.CallbackContext context);
    }
}
