﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickedCardButton : MonoBehaviour //, IPointerClickHandler
{
    //[SerializeField] private RectTransform viewport = null;

    [SerializeField] private ParticleSystem clickParticle = null;
    [SerializeField] private RectTransform particleRec = null;

    [SerializeField] private GameLogic gameLogic = null;

    //public CartasBosque cartasBosque = new CartasBosque();


    private void Start()
    {
        
    }



    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    print("onpointerclick");
    //    //if (clickParticle == false || gameLogic == false) return;

    //    //if (gameLogic.clickedCard == true || gameLogic.estaMezclando == true || gameLogi.isGameOver == true) return;

    //    //if (clickParticle.isPlaying == false)
    //    //{

    //    //    var pos = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
    //    //    pos.z = 0;
    //    //    particleRec.position = pos;
    //    //    clickParticle.Play();

    //    //}

    //}

   

}
