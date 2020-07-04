using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalActions;
using UnityEngine.InputSystem;
using UnityEditor;

public class MouseManager : MonoBehaviour
{


    private InputActions inputActions;
    [SerializeField] private GameLogic gameLogic = null;
    [SerializeField] private Camera camara = null;
    [SerializeField] private ParticleSystem clickParticleJuego = null;
    [SerializeField] private RectTransform particleRecJuego = null;


    [SerializeField] private ParticleSystem clickParticleElegirPersonaje = null;
    [SerializeField] private RectTransform particleRecElegirPersonaje = null;

    private Vector3 vectorMousePos = Vector3.zero;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.PlayerMovement.clicked.started += Clicked;
#if UNITY_STANDALONE || UNITY_EDITOR
        inputActions.PlayerMovement.mousemovement.performed += MouseMovement;
        inputActions.PlayerMovement.mousemovement.Enable();
#endif
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

        

#if UNITY_EDITOR
        print("clicked");
#endif

#if UNITY_ANDROID && !(UNITY_STANDALONE || UNITY_EDITOR)
        vectorMousePos = Touchscreen.current.position.ReadValue();
        vectorMousePos.z = 10;
#endif


        if (gameLogic.isAugmented == true)
        {
            
            gameLogic.ProcessCardAugmeted(gameLogic.currentPositionPlayer, "desaugmentar_carta");


        }


        Vector3 currentMousePos = camara.ScreenToWorldPoint(vectorMousePos);

        if (gameLogic.isCanvasElegirPersonajeActive == true)
        { 
            particleRecElegirPersonaje.position = currentMousePos;
            clickParticleElegirPersonaje.Play();
            return;
        
        }

        if (gameLogic.isCanvasJuegoActive == true)
        {
            particleRecJuego.position = currentMousePos;
            clickParticleJuego.Play();
            return;

        }


    }


}
