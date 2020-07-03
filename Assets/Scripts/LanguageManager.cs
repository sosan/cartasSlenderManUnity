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

        gameLogic.Click_NewGame();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
