using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UniRx.Async;

public class LanguageManager : MonoBehaviour
{

    [SerializeField] private GameLogic gameLogic = null;
    [SerializeField] private Color selectColor = Color.white;
    [SerializeField] private Color notSelectColor = Color.white;
    [SerializeField] private TextMeshProUGUI[] textos = null;
    [SerializeField] public ParticleSystem[] particulas = null;
    //[SerializeField] private Canvas canvas = null;
    [SerializeField] private CanvasGroup canvasGroupSelectPersonajes = null;
    [SerializeField] private CanvasGroup canvasGroupSelectLanguage = null;

    [SerializeField] private RectTransform[] posiciones = null;
    [SerializeField] private RectTransform cuadro = null;


    private void Awake()
    {

        if (PlayerPrefs.HasKey("idioma") == true)
        {

            string t = PlayerPrefs.GetString("idioma");

            switch (t)
            {

                case "espanol": Localization.language = t; break;
                case "english": Localization.language = t; break;
                default: Debug.LogError("NOT SET IDIOMA"); return;

            }

        }

        canvasGroupSelectLanguage.alpha = 0;
        //canvasGroupSelectPersonajes.alpha = 1;

        gameLogic.Click_NewGame();




    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public async void ClickedSetEnglish()
    {
        Localization.language = "english";
        PlayerPrefs.SetString("idioma", "english");
        particulas[0].Play();
        await UniTask.Delay(300);
        canvasGroupSelectLanguage.alpha = 0;
        //canvasGroupSelectPersonajes.alpha = 1;
        gameLogic.Click_NewGame();
    }


    public async void ClickedSetSpanish()
    {

        Localization.language = "espanol";
        PlayerPrefs.SetString("idioma", "espanol");
        particulas[1].Play();
        await UniTask.Delay(300);

        canvasGroupSelectLanguage.alpha = 0;
        //canvasGroupSelectPersonajes.alpha = 1;
        gameLogic.Click_NewGame();

    }

    

}
