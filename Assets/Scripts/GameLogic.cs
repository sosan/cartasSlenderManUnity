using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using System;

using GlobalActions;
using UnityEngine.InputSystem;
using UnityEditor;
using System.Linq;
using TMPro;

public class GameLogic : MonoBehaviour
{


    //private InputActions inputActions;

    [Header("Managers")]
    [SerializeField] private BotonesPersonaje botonesPersonaje = null;
    [SerializeField] private GenerateAnimations generateAnimations = null;
    public PersonajesStats personajesStats = null;
    public BosqueStats bosqueStats = null;

    public int currentPositionPlayer = -1;
    public int lastPositionPlayer = 8;

    [Header("Juego")]
    [SerializeField] public Image[] cartas = null;
    [SerializeField] public RectTransform[] cartasRect = null;
    [SerializeField] public Canvas[] canvasCartas = null;
    [SerializeField] private List<Sprite> poolImages = new List<Sprite>();
    [SerializeField] private Sprite[] imagesCartas = null;
    [SerializeField] private CanvasGroup canvasGroupJuego = null;
    [SerializeField] public RectTransform[] posicionPlayer = null;
    [SerializeField] private RectTransform recPlayer = null;
    [SerializeField] private Animation anim = null;
    [SerializeField] private ClickedCardButton[] clickedCardButtons = null;

    [SerializeField] private Animation[] animsCards = null;
    [SerializeField] private Outline[] outlineCards = null;
    [SerializeField] private Sprite malClickSprite = null;
    [SerializeField] private Sprite blackBackgroundCard = null;
    public Sprite whiteBackgroundCard = null;

    [Header("Eleccion Persoanjes")]
    [SerializeField] public Image[] cartasPersonajes = null;
    [SerializeField] public RectTransform[] cartasRectPersonajes = null;
    [SerializeField] private Canvas[] canvasCartasPersonajes = null;
    [SerializeField] private List<Sprite> poolImagesPersonajes = new List<Sprite>();
    [SerializeField] private CanvasGroup canvasGroupEleccionPersonakes = null;
    [SerializeField] public RectTransform[] posicionPersonajes = null;

    [Header("Personaje Seleccionado")]
    [SerializeField] private Image personajeSeleccionado = null;

    [Header("Punto de inicio")]
    [SerializeField] private Sprite spritePuntoInicio = null;

    [Header("pasos")]
    [SerializeField] private TextMeshProUGUI textoPasos = null;

    private ushort countPasos = 0;
    private List<int> listCards = new List<int>();
    private List<int> listCardsPersonajes = new List<int>();
    public bool clickedCard = false;
    public bool estaMezclando = true;

   

    public bool isAugmented = false;
    public bool isCanvasJuegoActive = false;
    public bool isCanvasElegirPersonajeActive = false;


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
        generateAnimations.GenerateCartasJuegosMezcla();

    }



    

    // Start is called before the first frame update
    private void Start()
    {
    }

    public async void Click_NewGame()
    {

        await UniTask.Delay(TimeSpan.FromMilliseconds(300));

        canvasGroupJuego.alpha = 0;
        canvasGroupEleccionPersonakes.alpha = 0;

        DesactivarCanvasJuego();
        SeccionElegirPersonaje();

    }

    private void ProcessAugmentedCard(int posicion)
    {
        isAugmented = false;
        currentPositionPlayer = posicion;
        generateAnimations.GenerateAnimationDesAumentar(currentPositionPlayer, "desaugmentar_carta");
        if (clickedCardButtons[currentPositionPlayer].cartasBosque.mezclar == true)
        {


            print("mezlcar ");

        }
        else if (clickedCardButtons[currentPositionPlayer].cartasBosque.bombilla == true)
        {
            botonesPersonaje.SumarBombillas();

        }
        else if (clickedCardButtons[currentPositionPlayer].cartasBosque.fear == true && clickedCardButtons[currentPositionPlayer].cartasBosque.startNewGame == true)
        {


            print("nueva partida");

        }
        else if (clickedCardButtons[currentPositionPlayer].cartasBosque.startPosition == true)
        {

            print("start position");

        }


    }

    public async void Click_Carta(int posicion)
    {

# if UNITY_EDITOR
        print("clicked" + " position=" + posicion + " lastPositionPlayer=" + lastPositionPlayer);
#endif
        if (estaMezclando == true || posicion < 1 || posicion > 8) return;

        


        if (isAugmented == true)
        {
            ProcessAugmentedCard(posicion - 1);
            return;

        }

        if (lastPositionPlayer == -1)
        {
            lastPositionPlayer = posicion - 1;

        }
        else
        {
            if (lastPositionPlayer == posicion - 1) return;

            if (clickedCardButtons[posicion - 1].cartasBosque.isVisited == true)
            {
                MalClick(posicion - 1);
                return;

            }

            switch (lastPositionPlayer)
            {
                case 0: if (posicion == 5 || posicion == 6 || posicion == 8 || posicion == 9) { MalClick(posicion - 1); return; } break;
                case 1: if (posicion == 4 || posicion == 6 || posicion == 7 || posicion == 9) { MalClick(posicion - 1); return; } break;
                case 2: if (posicion == 4 || posicion == 5 || posicion == 7 || posicion == 8) { MalClick(posicion - 1); return; } break;
                case 3: if (posicion == 2 || posicion == 3 || posicion == 8 || posicion == 9) { MalClick(posicion - 1); return; } break;
                case 4: if (posicion == 1 || posicion == 3 || posicion == 7 || posicion == 9) { MalClick(posicion - 1); return; } break;
                case 5: if (posicion == 1 || posicion == 2 || posicion == 7 || posicion == 8) { MalClick(posicion - 1); return; } break;
                case 6: if (posicion == 2 || posicion == 3 || posicion == 5 || posicion == 6) { MalClick(posicion - 1); return; } break;
                case 7: if (posicion == 1 || posicion == 3 || posicion == 4 || posicion == 6) { MalClick(posicion - 1); return; } break;
                case 8: if (posicion == 1 || posicion == 2 || posicion == 4 || posicion == 5) { MalClick(posicion - 1); return; } break;

            }


            outlineCards[lastPositionPlayer].GetComponent<Image>().enabled = false;

            //string nombreclipLastPosition = "carta_giro_" + (lastPositionPlayer + 1);

            AnimationClip clipTemp = generateAnimations.GenerateClipAnimation(lastPositionPlayer, "giro_carta_lastposition", false);

            anim.AddClip(clipTemp, clipTemp.name);
            anim.Play(clipTemp.name);

            //anim.Play(nombreclipLastPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip(clipTemp.name).length));
            cartasRect[lastPositionPlayer].rotation = Quaternion.Euler(0, 0, 0);
            cartas[lastPositionPlayer].sprite = blackBackgroundCard;
            anim.RemoveClip(clipTemp);

        }

        

        isAugmented = true;
        countPasos++;
        ShowPasos(countPasos);


        outlineCards[outlineCards.Length - 1].enabled = false;
        outlineCards[outlineCards.Length - 1].GetComponent<Image>().enabled = false;
        animsCards[outlineCards.Length - 1].Stop("carta_outline");
        
        DisableRaycastTarget();
        

        canvasCartas[posicion - 1].sortingOrder = 1;

        AnimationClip clip = generateAnimations.GenerateClipAnimation(posicion - 1, "giro_carta_siguiente", true);

        float fullDurationClip = clip.length * 1000; //anim.GetClip("carta_giro").length * 1000;
        float restDurationClip = fullDurationClip - 100;

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
        await UniTask.Delay(TimeSpan.FromMilliseconds(fullDurationClip - 100));

        currentPositionPlayer = posicion - 1;
        
        clickedCardButtons[currentPositionPlayer].cartasBosque.isVisited = true;

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


   

    private async void SeccionElegirPersonaje()
    {


        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {

            cartasRectPersonajes[i].localScale = Vector3.one;
            cartasPersonajes[i].sprite = whiteBackgroundCard;
            canvasCartasPersonajes[i].sortingOrder = 0;
            cartasRectPersonajes[i].anchoredPosition = posicionPersonajes[i].anchoredPosition;
        }



        isCanvasElegirPersonajeActive = true;
        isCanvasJuegoActive = false;
        ActivarCanvasElegirPersonaje();

        


        poolImagesPersonajes.Clear();
        //canvasGroupEleccionPersonakes.blocksRaycasts = true;
        //canvasGroupEleccionPersonakes.interactable = true;

        anim.Play("aparecerCanvasPersonajes");
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip("aparecerCanvasPersonajes").length * 1000));

        estaMezclando = true;
        currentPositionPlayer = -1;
        isAugmented = false;

        

        ScaleAllPersonajesCards(1);
        //anim.RemoveClip("cartaspersonajes_mezcla_runtime");
        generateAnimations.GenerateCartasPersonajesMezcla("cartaspersonajes_mezcla_runtime");

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

    public async void SeccionJuego()
    {

        for (ushort i = 0; i < cartas.Length; i++)
        {
            outlineCards[i].enabled = false;
            outlineCards[i].GetComponent<Image>().enabled = false;
            cartas[i].sprite = blackBackgroundCard;
            canvasCartas[i].sortingOrder = 0;

            

        }

        


        await UniTask.Delay(TimeSpan.FromMilliseconds(150));

        anim.Play("aparecerCanvasJuego");
        await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip("aparecerCanvasJuego").length * 1000));

        ActivarCanvasJuego();

        isCanvasElegirPersonajeActive = false;
        isCanvasJuegoActive = true;

        poolImages.Clear();

        estaMezclando = true;
        lastPositionPlayer = 8;
        currentPositionPlayer = -1;
        isAugmented = false;

        EnableRaycastTarget();


        cartas[cartas.Length - 1].sprite = spritePuntoInicio;


        ScaleAllGameCards(1);

        anim.RemoveClip("cartas_mezcla_runtime");
        generateAnimations.GenerateCartasJuegosMezcla();

        anim.Play("cartas_mezcla_runtime");
        await UniTask.Delay(TimeSpan.FromSeconds(anim.GetClip("cartas_mezcla_runtime").length));

        

        listCards.Clear();


        //for (ushort i = 0; i < cartas.Length; i++)
        //{
        //    cartasRect[i].rotation = Quaternion.Euler(0, 0, 0);
        //    InsertarCarta();


        //}

        //---

        List<Sprite> rndImgaes = new List<Sprite>();

        for (ushort i = 0; i < bosqueStats.cartasBosque.Length; i++)
        {
            rndImgaes.Add(bosqueStats.cartasBosque[i].imagenBosque );
            //rndImgaes.AddRange(imagesCartas);
        
        }


        listCards.AddRange(new int[8] {0, 1, 2, 3, 4, 5, 6, 7 });

        for (ushort i = 0; i < bosqueStats.cartasBosque.Length; i++)
        {
            cartasRect[i].rotation = Quaternion.Euler(0, 0, 0);

            int rnd = UnityEngine.Random.Range(i, bosqueStats.cartasBosque.Length);

            var tempCard = listCards[rnd];
            listCards[rnd] = listCards[i];
            listCards[i] = tempCard;

            var tempSpite = rndImgaes[rnd];
            rndImgaes[rnd] = rndImgaes[i];
            rndImgaes[i] = tempSpite;

            //listCards.Add(rnd);
            //poolImages.Add(imagesCartas[rnd]);

        }


        poolImages.AddRange(rndImgaes);

        outlineCards[outlineCards.Length - 1].enabled = true;
        outlineCards[outlineCards.Length - 1].GetComponent<Image>().enabled = true;

        animsCards[outlineCards.Length - 1].Play("carta_outline");


        for (ushort i = 0; i < listCards.Count; i++)
        {
            int posicionCartasBosque = listCards[i];
            clickedCardButtons[i].cartasBosque = bosqueStats.cartasBosque[posicionCartasBosque];

            clickedCardButtons[i].cartasBosque.isVisited = false;


        }

        clickedCardButtons[clickedCardButtons.Length - 1].cartasBosque.isVisited = false;
        clickedCardButtons[clickedCardButtons.Length - 1].cartasBosque.startPosition = true;


        estaMezclando = false;
    }

   


   


    private void InsertarCartaPersonaje()
    {

        bool insertado = false;

        while (insertado == false)
        {
            //int rnd = UnityEngine.Random.Range(0, imagesPersonajes.Length);
            int rnd = UnityEngine.Random.Range(0, personajesStats.personajes.Length);
            if (listCardsPersonajes.Contains(rnd) == false)
            {

                listCardsPersonajes.Add(rnd);
                //poolImagesPersonajes.Add(imagesPersonajes[rnd]);
                poolImagesPersonajes.Add(personajesStats.personajes[rnd].imagenPersonaje);

                

                insertado = true;
            }

        }

    }

   





    public async void ClickedSelectPersonaje(int posicion)
    {
        print("cliecked seelect personaje");
        if (isAugmented == true)
        {
            currentPositionPlayer = -1;
            DesactivarCanvasElegirPersonaje();
            SeccionJuego();
            //ProcessPersonajeAugmented(currentPositionPlayer, "desaumentar_personaje");
            return;
        }

        DisableRaycastPersonaje();
        isAugmented = true;
        canvasGroupEleccionPersonakes.blocksRaycasts = false;
        canvasGroupEleccionPersonakes.interactable = false;
        canvasCartasPersonajes[posicion - 1].sortingOrder = 1;

        AnimationClip clip = generateAnimations.GenerateClipAnimationPersonaje(posicion - 1, "giro_personaje");

        float fullDurationClip = clip.length * 1000;
        float restDurationClip = fullDurationClip - 100;

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
        await UniTask.Delay(TimeSpan.FromMilliseconds(fullDurationClip - 100));

        currentPositionPlayer = posicion - 1;
        cartasPersonajes[currentPositionPlayer].sprite = poolImagesPersonajes[currentPositionPlayer];
        personajeSeleccionado.sprite = poolImagesPersonajes[currentPositionPlayer];

        int posInRandomArray = listCardsPersonajes[currentPositionPlayer];
        botonesPersonaje.SetIniitialStatsPersonajePrincipal(personajesStats.personajes[posInRandomArray]);


        //botonesPersonaje.statsJugador.bombillas = personajesStats.personajes[posInRandomArray].bombilla;
        //botonesPersonaje.statsJugador.tokens = personajesStats.personajes[posInRandomArray].token;

        


        await UniTask.Delay(TimeSpan.FromMilliseconds(restDurationClip + 200));
        anim.RemoveClip(clip);
        cartasRectPersonajes[currentPositionPlayer].rotation = Quaternion.Euler(0, 0, 0);
        cartasPersonajes[currentPositionPlayer].raycastTarget = true;


    }


    

    //public async void ProcessPersonajeAugmented(int posicion, string nombreClip)
    //{

    //    isAugmented = false;
    //    AnimationClip clip = new AnimationClip
    //    {
    //        name = nombreClip,
    //        legacy = true,
    //        wrapMode = WrapMode.Once
    //    };

    //    Keyframe[] keysX = new Keyframe[2];
    //    Keyframe[] keysY = new Keyframe[2];
    //    Keyframe[] keysZ = new Keyframe[2];

    //    keysX[0] = new Keyframe(0f, cartasRectPersonajes[posicion].anchoredPosition.x);
    //    keysY[0] = new Keyframe(0f, cartasRectPersonajes[posicion].anchoredPosition.y);
    //    keysZ[0] = new Keyframe(0f, 0);

    //    keysX[1] = new Keyframe(0.3f, posicionPersonajes[posicion].anchoredPosition.x);
    //    keysY[1] = new Keyframe(0.3f, posicionPersonajes[posicion].anchoredPosition.y);
    //    keysZ[1] = new Keyframe(0.3f, 0);

    //    Keyframe[] keysScaleX = new Keyframe[2];
    //    Keyframe[] keysScaleY = new Keyframe[2];
    //    Keyframe[] keysScaleZ = new Keyframe[2];

    //    keysScaleX[0] = new Keyframe(0f, cartasRectPersonajes[posicion].localScale.x);
    //    keysScaleY[0] = new Keyframe(0f, cartasRectPersonajes[posicion].localScale.y);
    //    keysScaleZ[0] = new Keyframe(0f, cartasRectPersonajes[posicion].localScale.z);

    //    keysScaleX[1] = new Keyframe(0.15f, 1);
    //    keysScaleY[1] = new Keyframe(0.15f, 1);
    //    keysScaleZ[1] = new Keyframe(0.15f, 1);

    //    AnimationCurve curvex = new AnimationCurve(keysX);
    //    AnimationCurve curvey = new AnimationCurve(keysY);
    //    AnimationCurve curvez = new AnimationCurve(keysZ);

    //    AnimationCurve curveScalex = new AnimationCurve(keysScaleX);
    //    AnimationCurve curveScaley = new AnimationCurve(keysScaleY);
    //    AnimationCurve curveScalez = new AnimationCurve(keysScaleZ);

    //    string nombreCarta = "--Personajes/personaje_" + (posicion + 1);
    //    clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.x", curvex);
    //    clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.y", curvey);
    //    clip.SetCurve(nombreCarta, typeof(Transform), "localPosition.z", curvez);

    //    clip.SetCurve(nombreCarta, typeof(Transform), "localScale.x", curveScalex);
    //    clip.SetCurve(nombreCarta, typeof(Transform), "localScale.y", curveScaley);
    //    clip.SetCurve(nombreCarta, typeof(Transform), "localScale.z", curveScalez);

    //    anim.AddClip(clip, clip.name);
    //    anim.Play(clip.name);
    //    await UniTask.Delay(TimeSpan.FromMilliseconds(anim.GetClip(clip.name).length * 1000));
    //    //anim.RemoveClip(clip);

    //    isAugmented = false;
    //    EnableRaycastPersonaje();
    //    canvasCartasPersonajes[posicion].sortingOrder = 0;

    //}

    private void DisableRaycastPersonaje()
    {

        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {
            cartasPersonajes[i].raycastTarget = false;

        }
    }

    private void EnableRaycastPersonaje()
    {
        for (ushort i = 0; i < cartasPersonajes.Length; i++)
        {
            cartasPersonajes[i].raycastTarget = true;

        }

    }

    private void ShowPasos(ushort contadorPasos)
    {
        textoPasos.text = Localization.Get("pasos") + " " + contadorPasos + " / 10";
    
    
    }


    private void DisableRaycastTarget()
    {
        for (ushort i = 0; i < cartas.Length; i++)
        {
            cartas[i].raycastTarget = false;

        }

    }

    public void EnableRaycastTarget()
    {
        for (ushort i = 0; i < cartas.Length; i++)
        {
            cartas[i].raycastTarget = true;

        }

    }

    private async void MalClick(int posicion)
    {

        //int pos = posicion - 1;
        var tempSprite = cartas[posicion].sprite;
        cartas[posicion].sprite = malClickSprite;
        await UniTask.Delay(TimeSpan.FromMilliseconds(500));
        cartas[posicion].sprite = tempSprite;




    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif


    }

    public void DesactivarCanvasElegirPersonaje()
    {

        isCanvasElegirPersonajeActive = false;
        canvasGroupEleccionPersonakes.alpha = 0;
        canvasGroupEleccionPersonakes.blocksRaycasts = false;
        canvasGroupEleccionPersonakes.interactable = false;

        canvasGroupEleccionPersonakes.gameObject.SetActive(false);

        for (ushort i = 0; i < canvasCartasPersonajes.Length; i++)
        {
            canvasCartasPersonajes[i].GetComponent<GraphicRaycaster>().enabled = false;

        }



    }

    private void ActivarCanvasElegirPersonaje()
    {

        isCanvasElegirPersonajeActive = true;
        canvasGroupEleccionPersonakes.alpha = 1;
        canvasGroupEleccionPersonakes.blocksRaycasts = true;
        canvasGroupEleccionPersonakes.interactable = true;

        canvasGroupEleccionPersonakes.gameObject.SetActive(true);

        for (ushort i = 0; i < canvasCartasPersonajes.Length; i++)
        {
            canvasCartasPersonajes[i].GetComponent<GraphicRaycaster>().enabled = true;
            cartasPersonajes[i].raycastTarget = true;
        }



    }

    private void ActivarCanvasJuego()
    {

        //canvasGroupJuego.gameObject.SetActive(false);
        isCanvasJuegoActive = true;
        canvasGroupJuego.alpha = 1;
        canvasGroupJuego.blocksRaycasts = true;
        canvasGroupJuego.interactable = true;
        canvasGroupJuego.gameObject.SetActive(true);

        ShowPasos(countPasos);

        for (ushort i = 0; i < canvasCartas.Length; i++)
        {
            canvasCartas[i].GetComponent<GraphicRaycaster>().enabled = true;

        }

    }


    private void DesactivarCanvasJuego()
    {

        //canvasGroupJuego.gameObject.SetActive(false);
        isCanvasJuegoActive = false;
        canvasGroupJuego.alpha = 0;
        canvasGroupJuego.blocksRaycasts = false;
        canvasGroupJuego.interactable = false;

        canvasGroupJuego.gameObject.SetActive(false);

        for (ushort i = 0; i < canvasCartas.Length; i++)
        {
            canvasCartas[i].GetComponent<GraphicRaycaster>().enabled = false;

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

    //private void InsertarCarta()
    //{
    //    bool insertado = false;

    //    while (insertado == false)
    //    {
    //        int rnd = UnityEngine.Random.Range(0, imagesCartas.Length);
    //        if (listCards.Contains(rnd) == false)
    //        {

    //            listCards.Add(rnd);
    //            //cartas[i].color = Color.black;
    //            poolImages.Add(imagesCartas[rnd]);
    //            //cartas[i].sprite = imagesCartas[rnd];


    //            insertado = true;
    //        }

    //    }


    //}


}
