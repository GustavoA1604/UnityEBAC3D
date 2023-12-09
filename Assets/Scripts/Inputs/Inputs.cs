// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""fae2ec03-b28d-4951-8d12-b75c82363650"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""d946682b-d49a-4771-9397-6eede0b841a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToGun1"",
                    ""type"": ""Button"",
                    ""id"": ""f09b360a-bb02-4a54-b539-40932456a0e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToGun2"",
                    ""type"": ""Button"",
                    ""id"": ""91d65dbd-bf55-464d-b9de-2344569bb586"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""41478f31-0387-4d7f-b46c-74e6182deedb"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c0ed2a7-515e-440d-83a5-4676f8c2a050"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToGun1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0537634-eb9b-4fb6-b04d-820019db42fb"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToGun2"",
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
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_SwitchToGun1 = m_Gameplay.FindAction("SwitchToGun1", throwIfNotFound: true);
        m_Gameplay_SwitchToGun2 = m_Gameplay.FindAction("SwitchToGun2", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_SwitchToGun1;
    private readonly InputAction m_Gameplay_SwitchToGun2;
    public struct GameplayActions
    {
        private @Inputs m_Wrapper;
        public GameplayActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @SwitchToGun1 => m_Wrapper.m_Gameplay_SwitchToGun1;
        public InputAction @SwitchToGun2 => m_Wrapper.m_Gameplay_SwitchToGun2;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @SwitchToGun1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun1;
                @SwitchToGun1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun1;
                @SwitchToGun1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun1;
                @SwitchToGun2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun2;
                @SwitchToGun2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun2;
                @SwitchToGun2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchToGun2;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @SwitchToGun1.started += instance.OnSwitchToGun1;
                @SwitchToGun1.performed += instance.OnSwitchToGun1;
                @SwitchToGun1.canceled += instance.OnSwitchToGun1;
                @SwitchToGun2.started += instance.OnSwitchToGun2;
                @SwitchToGun2.performed += instance.OnSwitchToGun2;
                @SwitchToGun2.canceled += instance.OnSwitchToGun2;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnSwitchToGun1(InputAction.CallbackContext context);
        void OnSwitchToGun2(InputAction.CallbackContext context);
    }
}
