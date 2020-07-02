using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalActions;
using UnityEngine.InputSystem;
using UnityEditor;

public class MouseManager : MonoBehaviour
{


    private InputActions inputActions;
    [SerializeField] private Camera camara = null;
    [SerializeField] private ParticleSystem clickParticle = null;
    [SerializeField] private RectTransform particleRec = null;

    private Vector3 vectorMousePos = Vector3.zero;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.PlayerMovement.clicked.started += Clicked;
        inputActions.PlayerMovement.mousemovement.performed += MouseMovement;
        inputActions.PlayerMovement.mousemovement.Enable();
        inputActions.PlayerMovement.clicked.Enable();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MouseMovement(InputAction.CallbackContext obj)
    {
        vectorMousePos = obj.ReadValue<Vector2>();
        vectorMousePos.z = 10;
    }

    private void Clicked(InputAction.CallbackContext obj)
    {

        print("clicked");
        

        Vector3 currentMousePos = camara.ScreenToWorldPoint(vectorMousePos);
        particleRec.position = currentMousePos;
        clickParticle.Play();
    }


}
