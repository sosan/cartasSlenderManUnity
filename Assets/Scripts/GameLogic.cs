using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using System;

using GlobalActions;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameLogic : MonoBehaviour
{


    //private InputActions inputActions;

    

    [SerializeField] public int currentPositionPlayer = -1;
    [SerializeField] public int lastPositionPlayer = -1;
    
    [Header("Juego")]
    [SerializeField] private Image[] cartas = null;
    [SerializeField] private RectTransform[] cartasRect = null;
    [SerializeField] private Canvas[] canvasCartas = null;
    [SerializeField] private List<Sprite> poolImages = new List<Sprite>();
    [SerializeField] private Sprite[] imagesCartas = null;
    [SerializeField] private CanvasGroup canvasGroupJuego = null;
    [SerializeField] private RectTransform[] posicionPlayer = null;
    [SerializeField] private RectTransform recPlayer = null;
    [SerializeField] private Animation anim = null;

    [SerializeField] private Animation[] animsCards = null;
    [SerializeField] private Outline[] outlineCards = null;
    [SerializeField] private Sprite malClickSprite = null;
    [SerializeField] private Sprite blackBackgroundCard = null;
    [SerializeField] private Sprite whiteBackgroundCard = null;

    [Header("Eleccion Persoanjes")]
    [SerializeField] private Image[] cartasPersonajes = null;
    [SerializeField] private RectTransform[] cartasRectPersonajes = null;
    [SerializeField] private Sprite[] imagesPersonajes = null;
    [SerializeField] private Canvas[] canvasCartasPersonajes = null;
    [SerializeField] private List<Sprite> poolImagesPersonajes = new List<Sprite>();
    [SerializeField] private CanvasGroup canvasGroupEleccionPersonakes = null;
    [SerializeField] private RectTransform[] posicionPersonajes = null;


    private List<int> listCards = new List<int>();
    private List<int> listCardsPersonajes = new List<int>();
    public bool clickedCard = false;
    public bool estaMezclando = true;

    [SerializeField] private short rangoXMin = -400;
    [SerializeField] private short rangoXMax = 400;

    [SerializeField] private short rangoYMin = -240;
    [SerializeField] private short rangoYMax = 240;

    public bool isAugmented = false;



    private void Awake()
    {
        Application.targetFrameRate = 60;

        canvasGroupJuego.alpha = 0;
        canvasGroupEleccionPersonakes.alpha = 0;


        for (ushort i = 0; i < cartas.Length; i++)
        {


            outlineCards[i].enabled = false;

        }

        //GenerateCartasPersonajesMezcla("cartaspersonajes_mezcla_runtime");
        GenerateCartasJuegosMezcla();

    }

    

    private void GenerateCartasJuegosMezcla()
    {
        AnimationClip clip = new AnimationClip
        {
            name = "cartas_mezcla_runtime",
            legacy = true,

            wrapMode = WrapMode.Once
        };

        for (ushort i = 0; i < cartas.Length; i++ )
        { 
            Keyframe[] keysX = new Keyframe[6];
            Keyframe[] keysY = new Keyframe[6];

            float timeRot = 1.05f;
        

            keysX[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoXMin, rangoXMax) );
            keysX[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[4] = new Keyframe(timeRot, 500);
            keysX[5] = new Keyframe(1.40f, posicionPlayer[i].anchoredPosition.x);

            keysY[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoYMin, rangoYMax) );
            keysY[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoYMin, rangoYMax) );
            keysY[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoYMin, rangoYMax) );
            keysY[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoYMin, rangoYMax) );
            keysY[4] = new Keyframe(timeRot, 250 );
            keysY[5] = new Keyframe(1.40f, posicionPlayer[i].anchoredPosition.y);


            Keyframe[] keysRotX = new Keyframe[11];
            Keyframe[] keysRotY = new Keyframe[11];
            Keyframe[] keysRotZ = new Keyframe[11];
            Keyframe[] keysRotW = new Keyframe[11];

        

            Quaternion initialRotation = Quaternion.Euler(0, 0, 0);

            Quaternion rotationCard = Quaternion.identity;
            float angle = 90f;
            Vector3 axisForward = Vector3.forward;
            float timeSumed = 0.03f;

            for(int k = 0; k < 7; k++)
            {
                rotationCard = Quaternion.AngleAxis(angle * k, axisForward);

                keysRotX[k] = new Keyframe(timeRot, rotationCard.x);
                keysRotY[k] = new Keyframe(timeRot, rotationCard.y);
                keysRotZ[k] = new Keyframe(timeRot, rotationCard.z);
                keysRotW[k] = new Keyframe(timeRot, rotationCard.w);

                timeRot += timeSumed;
            }


            keysRotX[10] = new Keyframe(timeRot, initialRotation.x);
            keysRotY[10] = new Keyframe(timeRot, initialRotation.y);
            keysRotZ[10] = new Keyframe(timeRot, initialRotation.z);
            keysRotW[10] = new Keyframe(timeRot, initialRotation.w);

            AnimationCurve curvex = new AnimationCurve(keysX);
            AnimationCurve curvey = new AnimationCurve(keysY);


            AnimationCurve curveRotx = new AnimationCurve(keysRotX);
            AnimationCurve curveRoty = new AnimationCurve(keysRotY);
            AnimationCurve curveRotz = new AnimationCurve(keysRotZ);
            AnimationCurve curveRotw = new AnimationCurve(keysRotW);

            string nombreCarta = "--Juego/carta_" + (i + 1);
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.x", curvex);
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.y", curvey);



            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.x", curveRotx);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.y", curveRoty);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.z", curveRotz);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.w", curveRotw);


        }
        anim.AddClip(clip, clip.name);
            
    }

    private void GenerateCartasPersonajesMezcla(string nombreClip)
    {
        AnimationClip clip = new AnimationClip
        {
            name = nombreClip,
            legacy = true,

            wrapMode = WrapMode.Once
        };

        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {
            Keyframe[] keysX = new Keyframe[6];
            Keyframe[] keysY = new Keyframe[6];

            float timeRot = 1.05f;


            keysX[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[4] = new Keyframe(timeRot, 500);
            keysX[5] = new Keyframe(1.40f, posicionPersonajes[i].anchoredPosition.x);

            keysY[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[4] = new Keyframe(timeRot, 250);
            keysY[5] = new Keyframe(1.40f, posicionPersonajes[i].anchoredPosition.y);


            Keyframe[] keysRotX = new Keyframe[11];
            Keyframe[] keysRotY = new Keyframe[11];
            Keyframe[] keysRotZ = new Keyframe[11];
            Keyframe[] keysRotW = new Keyframe[11];



            Quaternion initialRotation = Quaternion.Euler(0, 0, 0);

            Quaternion rotationCard = Quaternion.identity;
            float angle = 90f;
            Vector3 axisForward = Vector3.forward;
            float timeSumed = 0.03f;

            for (int k = 0; k < 7; k++)
            {
                rotationCard = Quaternion.AngleAxis(angle * k, axisForward);

                keysRotX[k] = new Keyframe(timeRot, rotationCard.x);
                keysRotY[k] = new Keyframe(timeRot, rotationCard.y);
                keysRotZ[k] = new Keyframe(timeRot, rotationCard.z);
                keysRotW[k] = new Keyframe(timeRot, rotationCard.w);

                timeRot += timeSumed;
            }


            keysRotX[10] = new Keyframe(timeRot, initialRotation.x);
            keysRotY[10] = new Keyframe(timeRot, initialRotation.y);
            keysRotZ[10] = new Keyframe(timeRot, initialRotation.z);
            keysRotW[10] = new Keyframe(timeRot, initialRotation.w);

            AnimationCurve curvex = new AnimationCurve(keysX);
            AnimationCurve curvey = new AnimationCurve(keysY);


            AnimationCurve curveRotx = new AnimationCurve(keysRotX);
            AnimationCurve curveRoty = new AnimationCurve(keysRotY);
            AnimationCurve curveRotz = new AnimationCurve(keysRotZ);
            AnimationCurve curveRotw = new AnimationCurve(keysRotW);

            string nombreCarta = "--Personajes/personaje_" + (i + 1);
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.x", curvex);
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.y", curvey);



            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.x", curveRotx);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.y", curveRoty);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.z", curveRotz);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.w", curveRotw);


        }
        anim.AddClip(clip, clip.name);

    }

    // Start is called before the first frame update
    private async void Start()
    {
        //estaMezclando = true;
        //await UniTask.Delay(TimeSpan.FromSeconds(1));

        //Click_NewGame();
    }


    public async void Click_Carta(int posicion)
    {

# if UNITY_EDITOR
        print("clicked"+ " position=" + posicion + " lastPositionPlayer=" + lastPositionPlayer);
#endif
        if (estaMezclando == true || posicion < 0 || posicion > 9) return;


        if (isAugmented == true)
        {
# if UNITY_EDITOR
            print("augmented");
#endif
            currentPositionPlayer = posicion - 1;
            ProcessCardAugmeted(currentPositionPlayer, "desaugmentar_carta");

            return;
        }

        if (lastPositionPlayer == -1 )
        {
# if UNITY_EDITOR
            print("lastposition");
#endif
            lastPositionPlayer = posicion - 1;

        }
        else
        {
# if UNITY_EDITOR
            print("lastposition 1");
#endif
            if (lastPositionPlayer == posicion - 1) return;
# if UNITY_EDITOR
            print("lastposition 2");
#endif
            switch (lastPositionPlayer)
            {
                case 0: if (posicion == 5 || posicion == 6 || posicion == 8 || posicion == 9) { MalClick(posicion); return; } break;
                case 1: if (posicion == 4 || posicion == 6 || posicion == 7 || posicion == 9) { MalClick(posicion); return; } break;
                case 2: if (posicion == 4 || posicion == 5 || posicion == 7 || posicion == 8) { MalClick(posicion); return; } break;
                case 3: if (posicion == 2 || posicion == 3 || posicion == 8 || posicion == 9) { MalClick(posicion); return; } break;
                case 4: if (posicion == 1 || posicion == 3 || posicion == 7 || posicion == 9) { MalClick(posicion); return; } break;
                case 5: if (posicion == 1 || posicion == 2 || posicion == 7 || posicion == 8) { MalClick(posicion); return; } break;
                case 6: if (posicion == 2 || posicion == 3 || posicion == 5 || posicion == 6) { MalClick(posicion); return; } break;
                case 7: if (posicion == 1 || posicion == 3 || posicion == 4 || posicion == 6) { MalClick(posicion); return; } break;
                case 8: if (posicion == 1 || posicion == 2 || posicion == 4 || posicion == 5) { MalClick(posicion); return; } break;
            
            }
# if UNITY_EDITOR
            print("lastposition 3");
#endif

            outlineCards[lastPositionPlayer].GetComponent<Image>().enabled = false;

            //string nombreclipLastPosition = "carta_giro_" + (lastPositionPlayer + 1);

            AnimationClip clipTemp = GenerateClipAnimation(lastPositionPlayer, "giro_carta_lastposition", false);

            anim.AddClip(clipTemp, clipTemp.name);
            anim.Play(clipTemp.name);

            //anim.Play(nombreclipLastPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip(clipTemp.name).length));
            cartasRect[lastPositionPlayer].rotation = Quaternion.Euler(0,0,0);
            cartas[lastPositionPlayer].sprite = blackBackgroundCard;
            anim.RemoveClip(clipTemp);

        }

        isAugmented = true;

        //clickedCard = true;

        DisableRaycastTarget();

        canvasCartas[posicion - 1].sortingOrder = 1;

        AnimationClip clip = GenerateClipAnimation(posicion - 1, "giro_carta_siguiente", true);
        
        float fullDurationClip = clip.length * 1000; //anim.GetClip("carta_giro").length * 1000;
        float restDurationClip = fullDurationClip - 100;
        
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
        await UniTask.Delay(TimeSpan.FromMilliseconds(fullDurationClip - 100));
        
        currentPositionPlayer = posicion - 1;
        cartas[currentPositionPlayer].sprite = poolImages[currentPositionPlayer];

        await UniTask.Delay(TimeSpan.FromMilliseconds(restDurationClip + 200));

        //comentario temporal
        anim.RemoveClip(clip);

        cartasRect[currentPositionPlayer].rotation = Quaternion.Euler(0, 0, 0);
        animsCards[lastPositionPlayer].Stop();
        outlineCards[lastPositionPlayer].enabled = false;
        outlineCards[lastPositionPlayer].GetComponent<Image>().enabled = false;

        
        lastPositionPlayer = currentPositionPlayer;

        cartas[currentPositionPlayer].raycastTarget = true;
        cartas[currentPositionPlayer].color = Color.white;
        outlineCards[currentPositionPlayer].enabled = true;
        outlineCards[currentPositionPlayer].GetComponent<Image>().enabled = true;

        animsCards[currentPositionPlayer].Play("carta_outline");
        await UniTask.Delay(TimeSpan.FromSeconds(animsCards[currentPositionPlayer].GetClip("carta_outline").length));
        //clickedCard = false;

        


    }


    public async void ProcessCardAugmeted(int posicion, string nombreClip)
    {

        isAugmented = false;
        AnimationClip clip = new AnimationClip
        {
            name = nombreClip,
            legacy = true,
            wrapMode = WrapMode.Once
        };

        Keyframe[] keysX = new Keyframe[2];
        Keyframe[] keysY = new Keyframe[2];
        Keyframe[] keysZ = new Keyframe[2];

        keysX[0] = new Keyframe(0f, cartasRect[posicion].anchoredPosition.x);
        keysY[0] = new Keyframe(0f, cartasRect[posicion].anchoredPosition.y);
        keysZ[0] = new Keyframe(0f, 0);
        
        keysX[1] = new Keyframe(0.3f, posicionPlayer[posicion].anchoredPosition.x);
        keysY[1] = new Keyframe(0.3f, posicionPlayer[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.3f, 0);
        
        Keyframe[] keysScaleX = new Keyframe[2];
        Keyframe[] keysScaleY = new Keyframe[2];
        Keyframe[] keysScaleZ = new Keyframe[2];
        
        keysScaleX[0] = new Keyframe(0f, cartasRect[posicion].localScale.x);
        keysScaleY[0] = new Keyframe(0f, cartasRect[posicion].localScale.y);
        keysScaleZ[0] = new Keyframe(0f, cartasRect[posicion].localScale.z);
        
        keysScaleX[1] = new Keyframe(0.15f, 1);
        keysScaleY[1] = new Keyframe(0.15f, 1);
        keysScaleZ[1] = new Keyframe(0.15f, 1);

        AnimationCurve curvex = new AnimationCurve(keysX);
        AnimationCurve curvey = new AnimationCurve(keysY);
        AnimationCurve curvez = new AnimationCurve(keysZ);

        AnimationCurve curveScalex = new AnimationCurve(keysScaleX);
        AnimationCurve curveScaley = new AnimationCurve(keysScaleY);
        AnimationCurve curveScalez = new AnimationCurve(keysScaleZ);

        string nombreCarta = "--Juego/carta_" + (posicion + 1);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.x", curvex);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.y", curvey);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.z", curvez);

        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.x", curveScalex);
        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.y", curveScaley);
        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.z", curveScalez);

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip(clip.name).length * 1000 ));
        //anim.RemoveClip(clip);

        isAugmented = false;
        EnableRaycastTarget();
        canvasCartas[posicion].sortingOrder = 0;


    }

    private AnimationClip GenerateClipAnimation(int posicion, string nombreClip, bool isCurrentCard)
    {

        AnimationClip clip = new AnimationClip
        {
            name = nombreClip,
            legacy = true,
            wrapMode = WrapMode.Once
        };
        string nombreCarta = "--Juego/carta_" + (posicion + 1);

        Keyframe[] keysX = new Keyframe[4];
        Keyframe[] keysY = new Keyframe[4];
        Keyframe[] keysZ = new Keyframe[4];

        float x = cartasRect[posicion].anchoredPosition.x;

        keysX[0] = new Keyframe(0f, x);
        keysY[0] = new Keyframe(0f, cartasRect[posicion].anchoredPosition.y);
        keysZ[0] = new Keyframe(0f, 0);

        keysX[1] = new Keyframe(0.05f, x -= 44);
        keysY[1] = new Keyframe(0.05f, cartasRect[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.05f, 0);

        keysX[2] = new Keyframe(0.15f, x += 44);
        keysY[2] = new Keyframe(0.15f, cartasRect[posicion].anchoredPosition.y);
        keysZ[2] = new Keyframe(0.15f, 0);


        if (isCurrentCard == true)
        {

            keysX[3] = new Keyframe(0.20f, 0);
            keysY[3] = new Keyframe(0.20f, 0);
            keysZ[3] = new Keyframe(0.20f, 0);

        }
        else
        {

            keysX[3] = new Keyframe(0.20f, x);
            keysY[3] = new Keyframe(0.20f, cartasRect[posicion].anchoredPosition.y);
            keysZ[3] = new Keyframe(0.20f, 0);

        }


        //keysY[0] = new Keyframe(0f, cartasRect[posicion].anchoredPosition.y );
        //keysZ[0] = new Keyframe(0f, 0);


        Keyframe[] keysRotX = new Keyframe[4];
        Keyframe[] keysRotY = new Keyframe[4];
        Keyframe[] keysRotZ = new Keyframe[4];
        Keyframe[] keysRotW = new Keyframe[4];

        //float rotX = cartasRect[posicion - 1].localRotation.x;

        Quaternion angle0 = Quaternion.Euler(0, 0, 0);
        Quaternion angle90 = Quaternion.Euler(0, 92, 0);

        keysRotX[0] = new Keyframe(0f, angle0.x );
        keysRotY[0] = new Keyframe(0f, angle0.y );
        keysRotZ[0] = new Keyframe(0f, angle0.z );
        keysRotW[0] = new Keyframe(0f, angle0.w );


        keysRotX[1] = new Keyframe(0.03f, angle0.x);
        keysRotY[1] = new Keyframe(0.03f, angle0.y);
        keysRotZ[1] = new Keyframe(0.03f, angle0.z);
        keysRotW[1] = new Keyframe(0.03f, angle0.w);

        keysRotX[2] = new Keyframe(0.10f, angle90.x);
        keysRotY[2] = new Keyframe(0.10f, angle90.y);
        keysRotZ[2] = new Keyframe(0.10f, angle90.z);
        keysRotW[2] = new Keyframe(0.10f, angle90.w);

        keysRotX[3] = new Keyframe(0.20f, angle0.x);
        keysRotY[3] = new Keyframe(0.20f, angle0.y);
        keysRotZ[3] = new Keyframe(0.20f, angle0.z);
        keysRotW[3] = new Keyframe(0.20f, angle0.w);

        //keysRotY[1] = new Keyframe(0.03f, angle0.y);
        //keysRotY[2] = new Keyframe(0.10f, angle90.y);
        //keysRotY[3] = new Keyframe(0.20f, angle0.y );
        if (isCurrentCard == true)
        {
            Keyframe[] keysScaleX = new Keyframe[2];
            Keyframe[] keysScaleY = new Keyframe[2];
            Keyframe[] keysScaleZ = new Keyframe[1];

            keysScaleX[0] = new Keyframe(0.15f, 1);
            keysScaleX[1] = new Keyframe(0.20f, 4);

            keysScaleY[0] = new Keyframe(0.15f, 1);
            keysScaleY[1] = new Keyframe(0.20f, 4);

            keysScaleZ[0] = new Keyframe(0.15f, 1);

            AnimationCurve curveScalex = new AnimationCurve(keysScaleX);
            AnimationCurve curveScaley = new AnimationCurve(keysScaleY);
            AnimationCurve curveScalez = new AnimationCurve(keysScaleZ);

            clip.SetCurve(nombreCarta, typeof(Transform), "localScale.x", curveScalex);
            clip.SetCurve(nombreCarta, typeof(Transform), "localScale.y", curveScaley);
            clip.SetCurve(nombreCarta, typeof(Transform), "localScale.z", curveScalez);


        }



        AnimationCurve curvex = new AnimationCurve(keysX);
        AnimationCurve curvey = new AnimationCurve(keysY);
        AnimationCurve curvez = new AnimationCurve(keysZ);

        AnimationCurve curveRotx = new AnimationCurve(keysRotX);
        AnimationCurve curveRoty = new AnimationCurve(keysRotY);
        AnimationCurve curveRotz = new AnimationCurve(keysRotZ);
        AnimationCurve curveRotw = new AnimationCurve(keysRotW);

        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.x", curvex);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.y", curvey);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.z", curvez);

        clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.x", curveRotx);
        clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.y", curveRoty);
        clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.z", curveRotz);
        clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.w", curveRotw);

        
        clip.EnsureQuaternionContinuity();

        return clip;
    
    
    
    }

    private void DisableRaycastTarget()
    {
        for (ushort i = 0; i < cartas.Length; i++)
        {
            cartas[i].raycastTarget = false;
        
        }
    
    }

    private void EnableRaycastTarget()
    {
        for (ushort i = 0; i < cartas.Length; i++)
        {
            cartas[i].raycastTarget = true;

        }

    }

    private async void MalClick(int posicion)
    { 
    
        int pos = posicion - 1;
        var tempSprite = cartas[pos].sprite;
        cartas[pos].sprite = malClickSprite;
        await UniTask.Delay(TimeSpan.FromMilliseconds(500));
        cartas[pos].sprite = tempSprite;




    }

    private async void SeccionElegirPersonaje()
    {
        poolImagesPersonajes.Clear();

        anim.Play("aparecerCanvasPersonajes");
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip("aparecerCanvasPersonajes").length * 1000  ));

        estaMezclando = true;
        currentPositionPlayer = -1;
        isAugmented = false;

        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {
            ////outlineCardsPersonajes[i].enabled = false;
            cartasPersonajes[i].sprite = whiteBackgroundCard;
            canvasCartasPersonajes[i].sortingOrder = 0;

        }

        ScaleAllPersonajesCards(1);
        //anim.RemoveClip("cartaspersonajes_mezcla_runtime");
        GenerateCartasPersonajesMezcla("cartaspersonajes_mezcla_runtime");

        anim.Play("cartaspersonajes_mezcla_runtime");
        await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip("cartaspersonajes_mezcla_runtime").length));

        listCardsPersonajes.Clear();
        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {
            cartasRectPersonajes[i].rotation = Quaternion.Euler(0, 0, 0);
            InsertarCartaPersonaje();


        }
        estaMezclando = false;
    }

    private async void SeccionJuego()
    {

        poolImages.Clear();

        anim.Play("aparecerCanvasJuego");
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip("aparecerCanvasJuego").length * 1000));


        estaMezclando = true;
        lastPositionPlayer = -1;
        currentPositionPlayer = -1;
        isAugmented = false;

        EnableRaycastTarget();

        for (ushort i = 0; i < cartas.Length; i++)
        {
            outlineCards[i].enabled = false;
            cartas[i].sprite = blackBackgroundCard;
            canvasCartas[i].sortingOrder = 0;

        }


        ScaleAllGameCards(1);

        anim.RemoveClip("cartas_mezcla_runtime");
        GenerateCartasJuegosMezcla();

        anim.Play("cartas_mezcla_runtime");
        await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip("cartas_mezcla_runtime").length));

        listCards.Clear();
        for (ushort i = 0; i < cartas.Length; i++)
        {
            cartasRect[i].rotation = Quaternion.Euler(0, 0, 0);
            InsertarCarta();


        }
        estaMezclando = false;
    }


    public void Click_NewGame()
    {
        canvasGroupJuego.alpha = 0;
        canvasGroupEleccionPersonakes.alpha = 0;

        //seccion elegir player
        SeccionElegirPersonaje();

        //SeccionJuego();

    }


    private void InsertarCartaPersonaje()
    {

        bool insertado = false;

        while (insertado == false)
        {
            int rnd = UnityEngine.Random.Range(0, imagesPersonajes.Length);
            if (listCardsPersonajes.Contains(rnd) == false)
            {

                listCardsPersonajes.Add(rnd);
                poolImagesPersonajes.Add(imagesPersonajes[rnd]);

                insertado = true;
            }

        }

    }

    private void InsertarCarta()
    { 
        bool insertado = false;

        while(insertado == false)
        { 
            int rnd = UnityEngine.Random.Range(0, imagesCartas.Length);
            if (listCards.Contains(rnd) == false)
            { 
            
                listCards.Add(rnd);
                //cartas[i].color = Color.black;
                poolImages.Add(imagesCartas[rnd]);
                //cartas[i].sprite = imagesCartas[rnd];
                
                insertado = true;
            }
                       
        }

    
    }

    private void ScaleAllGameCards(int scale)
    {
        for (ushort i = 0; i < cartasRect.Length; i++)
        {

            cartasRect[i].localScale = new Vector3(scale, scale, scale);
        
        }
    
    
    }

    private void ScaleAllPersonajesCards(int scale)
    {
        for (ushort i = 0; i < cartasRectPersonajes.Length; i++)
        {

            cartasRectPersonajes[i].localScale = new Vector3(scale, scale, scale);

        }


    }


    public void QuitGame()
    { 
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    
    
    }

}
