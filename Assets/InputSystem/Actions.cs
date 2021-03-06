// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GlobalActions
{
    public class @InputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""5087d346-6e95-4861-95a6-28188a50cbbb"",
            ""actions"": [
                {
                    ""name"": ""awsd"",
                    ""type"": ""Value"",
                    ""id"": ""053d4aa4-8eb8-44cc-8660-849638707f2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""arrows"",
                    ""type"": ""Value"",
                    ""id"": ""1afb4fd4-1f97-4160-982a-413626b1daee"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""clicked"",
                    ""type"": ""Button"",
                    ""id"": ""816c86ba-4b79-4fa5-8922-3b801f34251f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""mousemovement"",
                    ""type"": ""Value"",
                    ""id"": ""1424bfa5-189a-408c-8f0c-f0927148e002"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""546fb878-3af7-4a34-a9a8-c8fb13ef61b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""awsd"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""876c173c-44b3-4732-aa98-d698f07f3bcf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""awsd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d4d6f0e9-0235-4710-a92e-e09c415c66eb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""awsd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3d97bafb-2394-4b94-8abf-133651d38dac"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""awsd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""065901ee-b413-4951-8109-6ab2381f8224"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""awsd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""03ededc0-b86d-42dc-a57f-ce1ac0ff9444"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""arrows"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fef56bf6-5b5c-41a8-a8a8-f0c92dd99eea"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cc98572e-8a44-4a9b-8920-ae67f4d1f22e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a492b3f7-d949-48f4-b3cf-77204d8f2690"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fff36f67-b817-4a31-b5dd-610f8fd0552e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d41b84a9-bdca-43ac-8971-7d00d4e19eba"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e0bbffb-0c32-4c21-b090-7c9ef2e186bb"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""758a0fad-c27d-455c-bed2-22ba95d84460"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ede9c42a-2a77-4eb5-a809-e5f497141a43"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mousemovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerMovement
            m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
            m_PlayerMovement_awsd = m_PlayerMovement.FindAction("awsd", throwIfNotFound: true);
            m_PlayerMovement_arrows = m_PlayerMovement.FindAction("arrows", throwIfNotFound: true);
            m_PlayerMovement_clicked = m_PlayerMovement.FindAction("clicked", throwIfNotFound: true);
            m_PlayerMovement_mousemovement = m_PlayerMovement.FindAction("mousemovement", throwIfNotFound: true);
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

        // PlayerMovement
        private readonly InputActionMap m_PlayerMovement;
        private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
        private readonly InputAction m_PlayerMovement_awsd;
        private readonly InputAction m_PlayerMovement_arrows;
        private readonly InputAction m_PlayerMovement_clicked;
        private readonly InputAction m_PlayerMovement_mousemovement;
        public struct PlayerMovementActions
        {
            private @InputActions m_Wrapper;
            public PlayerMovementActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @awsd => m_Wrapper.m_PlayerMovement_awsd;
            public InputAction @arrows => m_Wrapper.m_PlayerMovement_arrows;
            public InputAction @clicked => m_Wrapper.m_PlayerMovement_clicked;
            public InputAction @mousemovement => m_Wrapper.m_PlayerMovement_mousemovement;
            public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerMovementActions instance)
            {
                if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
                {
                    @awsd.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAwsd;
                    @awsd.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAwsd;
                    @awsd.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAwsd;
                    @arrows.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArrows;
                    @arrows.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArrows;
                    @arrows.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArrows;
                    @clicked.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnClicked;
                    @clicked.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnClicked;
                    @clicked.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnClicked;
                    @mousemovement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousemovement;
                    @mousemovement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousemovement;
                    @mousemovement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousemovement;
                }
                m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @awsd.started += instance.OnAwsd;
                    @awsd.performed += instance.OnAwsd;
                    @awsd.canceled += instance.OnAwsd;
                    @arrows.started += instance.OnArrows;
                    @arrows.performed += instance.OnArrows;
                    @arrows.canceled += instance.OnArrows;
                    @clicked.started += instance.OnClicked;
                    @clicked.performed += instance.OnClicked;
                    @clicked.canceled += instance.OnClicked;
                    @mousemovement.started += instance.OnMousemovement;
                    @mousemovement.performed += instance.OnMousemovement;
                    @mousemovement.canceled += instance.OnMousemovement;
                }
            }
        }
        public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
        public interface IPlayerMovementActions
        {
            void OnAwsd(InputAction.CallbackContext context);
            void OnArrows(InputAction.CallbackContext context);
            void OnClicked(InputAction.CallbackContext context);
            void OnMousemovement(InputAction.CallbackContext context);
        }
    }
}
