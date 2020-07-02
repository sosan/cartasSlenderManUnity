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

    [SerializeField] private int currentPositionPlayer = -1;
    [SerializeField] private int lastPositionPlayer = -1;
    [SerializeField] private Image[] cartas = null;
    [SerializeField] private RectTransform[] cartasRect = null;
    [SerializeField] private Canvas[] canvasCartas = null;
    [SerializeField] private List<Sprite> poolImages = new List<Sprite>();
    [SerializeField] private Sprite[] imagesCartas = null;

    [SerializeField] private RectTransform[] posicionPlayer = null;
    [SerializeField] private RectTransform recPlayer = null;
    [SerializeField] private Animation anim = null;

    [SerializeField] private Animation[] animsCards = null;
    [SerializeField] private Outline[] outlineCards = null;
    [SerializeField] private Sprite malClickSprite = null;
    [SerializeField] private Sprite backgroundCard = null;

    private List<int> listCards = new List<int>();
    public bool clickedCard = false;
    public bool estaMezclando = true;

    [SerializeField] private short rangoXMin = -400;
    [SerializeField] private short rangoXMax = 400;

    [SerializeField] private short rangoYMin = -240;
    [SerializeField] private short rangoYMax = 240;

    private bool isAugmented = false;



    private void Awake()
    {
        Application.targetFrameRate = 60;

        


        for (ushort i = 0; i < cartas.Length; i++)
        {


            outlineCards[i].enabled = false;

        }

        GenerateCartasMezcla();

    }

    

    private void GenerateCartasMezcla()
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

            string nombreCarta = "carta_" + (i + 1);
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
        estaMezclando = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        Click_NewGame();
    }


    public async void Click_Carta(int posicion)
    {

# if UNITY_EDITOR
        print("clicked"+ " position=" + posicion + " lastPositionPlayer=" + lastPositionPlayer);
#endif
        if (estaMezclando == true || posicion < 0 || posicion > 9) return;


        if (isAugmented == true)
        {
            print("augmented");
            isAugmented = false;
            ProcessCardAugmeted(posicion - 1, "desaugmentar_carta");

            return;
        }

        if (lastPositionPlayer == -1 )
        {
            print("lastposition");
            lastPositionPlayer = posicion - 1;

        }
        else
        {

            print("lastposition 1");
            if (lastPositionPlayer == posicion - 1) return;
            print("lastposition 2");
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
            print("lastposition 3");
            
            outlineCards[lastPositionPlayer].GetComponent<Image>().enabled = false;

            //string nombreclipLastPosition = "carta_giro_" + (lastPositionPlayer + 1);

            AnimationClip clipTemp = GenerateClipAnimation(lastPositionPlayer, "giro_carta_lastposition", false);

            anim.AddClip(clipTemp, clipTemp.name);
            anim.Play(clipTemp.name);

            //anim.Play(nombreclipLastPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip(clipTemp.name).length));
            cartasRect[lastPositionPlayer].rotation = Quaternion.Euler(0,0,0);
            cartas[lastPositionPlayer].sprite = backgroundCard;
            anim.RemoveClip(clipTemp);

        }

        isAugmented = true;

        print("dentro1");
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

        await UniTask.Delay(TimeSpan.FromMilliseconds(restDurationClip));

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


    private async void ProcessCardAugmeted(int posicion, string nombreClip)
    {

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

        keysX[1] = new Keyframe(0.5f, posicionPlayer[posicion].anchoredPosition.x);
        keysY[1] = new Keyframe(0.5f, posicionPlayer[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.5f, 0);

        Keyframe[] keysScaleX = new Keyframe[2];
        Keyframe[] keysScaleY = new Keyframe[2];
        Keyframe[] keysScaleZ = new Keyframe[2];

        keysScaleX[0] = new Keyframe(0f, cartasRect[posicion].localScale.x);
        keysScaleY[0] = new Keyframe(0f, cartasRect[posicion].localScale.y);
        keysScaleZ[0] = new Keyframe(0f, cartasRect[posicion].localScale.z);

        keysScaleX[1] = new Keyframe(0.20f, 1);
        keysScaleY[1] = new Keyframe(0.20f, 1);
        keysScaleZ[1] = new Keyframe(0.20f, 1);

        AnimationCurve curvex = new AnimationCurve(keysX);
        AnimationCurve curvey = new AnimationCurve(keysY);
        AnimationCurve curvez = new AnimationCurve(keysZ);

        AnimationCurve curveScalex = new AnimationCurve(keysScaleX);
        AnimationCurve curveScaley = new AnimationCurve(keysScaleY);
        AnimationCurve curveScalez = new AnimationCurve(keysScaleZ);

        string nombreCarta = "carta_" + (posicion + 1);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.x", curvex);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.y", curvey);
        clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.z", curvez);

        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.x", curveScalex);
        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.y", curveScaley);
        clip.SetCurve(nombreCarta, typeof(Transform), "localScale.z", curveScalez);

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip(clip.name).length * 1000 ));
        anim.RemoveClip(clip);

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
        string nombreCarta = "carta_" + (posicion + 1);

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

    public async void Click_NewGame()
    { 
        poolImages.Clear();
        estaMezclando = true;
        lastPositionPlayer = -1;
        currentPositionPlayer = -1;

        EnableRaycastTarget();

        for (ushort i = 0; i < cartas.Length; i++)
        {
            outlineCards[i].enabled = false;
            cartas[i].sprite = backgroundCard;
            canvasCartas[i].sortingOrder = 0;

        }


        ScaleAllCards(1);

        anim.RemoveClip("cartas_mezcla_runtime");
        GenerateCartasMezcla();

        anim.Play("cartas_mezcla_runtime");
        await UniTask.Delay(TimeSpan.FromSeconds( anim.GetClip("cartas_mezcla_runtime").length ));

        listCards.Clear();
        for(ushort i = 0; i < cartas.Length; i++)
        {
            cartasRect[i].rotation = Quaternion.Euler(0,0,0);
            InsertarCarta();
           
        
        }
        estaMezclando = false;
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

    private void ScaleAllCards(int scale)
    {
        for (ushort i = 0; i < cartasRect.Length; i++)
        {

            cartasRect[i].localScale = new Vector3(scale, scale, scale);
        
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
