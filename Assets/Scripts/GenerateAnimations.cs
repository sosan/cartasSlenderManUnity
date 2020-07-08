using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using System;

public class GenerateAnimations : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameLogic gameLogic = null;
    [SerializeField] private Animation anim = null;

    [Header("Configuration")]
    [SerializeField] private short rangoXMin = -400;
    [SerializeField] private short rangoXMax = 400;

    [SerializeField] private short rangoYMin = -240;
    [SerializeField] private short rangoYMax = 240;

    public void GenerateCartasJuegosMezcla()
    {
        AnimationClip clip = new AnimationClip
        {
            name = "cartas_mezcla_runtime",
            legacy = true,

            wrapMode = WrapMode.Once
        };

        for (ushort i = 0; i < gameLogic.cartas.Length; i++)
        {
            Keyframe[] keysX = new Keyframe[6];
            Keyframe[] keysY = new Keyframe[6];

            float timeRot = 1.05f;


            keysX[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[4] = new Keyframe(timeRot, 500);
            keysX[5] = new Keyframe(1.40f, gameLogic.initialCardPosition[i].anchoredPosition.x);

            keysY[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[4] = new Keyframe(timeRot, 250);
            keysY[5] = new Keyframe(1.40f, gameLogic.initialCardPosition[i].anchoredPosition.y);


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

    public void GenerateCartasPersonajesMezcla(string nombreClip)
    {
        AnimationClip clip = new AnimationClip
        {
            name = nombreClip,
            legacy = true,

            wrapMode = WrapMode.Once
        };

        for (ushort i = 0; i < gameLogic.cartasPersonajes.Length; i++)
        {
            Keyframe[] keysX = new Keyframe[6];
            Keyframe[] keysY = new Keyframe[6];

            float timeRot = 1.05f;


            keysX[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[4] = new Keyframe(timeRot, 500);
            keysX[5] = new Keyframe(1.40f, gameLogic.posicionPersonajes[i].anchoredPosition.x);

            keysY[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[4] = new Keyframe(timeRot, 250);
            keysY[5] = new Keyframe(1.40f, gameLogic.posicionPersonajes[i].anchoredPosition.y);


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

    public AnimationClip GenerateAnimationDesAumentar(int posicion, string nombreClip)
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

        keysX[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].anchoredPosition.x);
        keysY[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].anchoredPosition.y);
        keysZ[0] = new Keyframe(0f, 0);

        keysX[1] = new Keyframe(0.3f, gameLogic.initialCardPosition[posicion].anchoredPosition.x);
        keysY[1] = new Keyframe(0.3f, gameLogic.initialCardPosition[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.3f, 0);

        Keyframe[] keysScaleX = new Keyframe[2];
        Keyframe[] keysScaleY = new Keyframe[2];
        Keyframe[] keysScaleZ = new Keyframe[2];

        keysScaleX[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].localScale.x);
        keysScaleY[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].localScale.y);
        keysScaleZ[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].localScale.z);

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


        return clip;
        

    }

    public AnimationClip GenerateClipAnimation(int posicion, string nombreClip, bool isCurrentCard)
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

        float x = gameLogic. cartasRect[posicion].anchoredPosition.x;

        keysX[0] = new Keyframe(0f, x);
        keysY[0] = new Keyframe(0f, gameLogic.cartasRect[posicion].anchoredPosition.y);
        keysZ[0] = new Keyframe(0f, 0);

        keysX[1] = new Keyframe(0.05f, x -= 44);
        keysY[1] = new Keyframe(0.05f, gameLogic.cartasRect[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.05f, 0);

        keysX[2] = new Keyframe(0.15f, x += 44);
        keysY[2] = new Keyframe(0.15f, gameLogic.cartasRect[posicion].anchoredPosition.y);
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
            keysY[3] = new Keyframe(0.20f, gameLogic.cartasRect[posicion].anchoredPosition.y);
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

        keysRotX[0] = new Keyframe(0f, angle0.x);
        keysRotY[0] = new Keyframe(0f, angle0.y);
        keysRotZ[0] = new Keyframe(0f, angle0.z);
        keysRotW[0] = new Keyframe(0f, angle0.w);


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


    public AnimationClip GenerateClipAnimationPersonaje(int posicion, string nombreClip)
    {
        AnimationClip clip = new AnimationClip
        {
            name = nombreClip,
            legacy = true,
            wrapMode = WrapMode.Once
        };

        string nombreCarta = "--Personajes/personaje_" + (posicion + 1);

        Keyframe[] keysX = new Keyframe[4];
        Keyframe[] keysY = new Keyframe[4];
        Keyframe[] keysZ = new Keyframe[4];

        float x = gameLogic.cartasRectPersonajes[posicion].anchoredPosition.x;

        keysX[0] = new Keyframe(0f, x);
        keysY[0] = new Keyframe(0f, gameLogic.cartasRectPersonajes[posicion].anchoredPosition.y);
        keysZ[0] = new Keyframe(0f, 0);

        keysX[1] = new Keyframe(0.05f, x -= 44);
        keysY[1] = new Keyframe(0.05f, gameLogic.cartasRectPersonajes[posicion].anchoredPosition.y);
        keysZ[1] = new Keyframe(0.05f, 0);

        keysX[2] = new Keyframe(0.15f, x += 44);
        keysY[2] = new Keyframe(0.15f, gameLogic.cartasRectPersonajes[posicion].anchoredPosition.y);
        keysZ[2] = new Keyframe(0.15f, 0);

        keysX[3] = new Keyframe(0.20f, 0);
        keysY[3] = new Keyframe(0.20f, 0);
        keysZ[3] = new Keyframe(0.20f, 0);

        Keyframe[] keysRotX = new Keyframe[4];
        Keyframe[] keysRotY = new Keyframe[4];
        Keyframe[] keysRotZ = new Keyframe[4];
        Keyframe[] keysRotW = new Keyframe[4];


        Quaternion angle0 = Quaternion.Euler(0, 0, 0);
        Quaternion angle90 = Quaternion.Euler(0, 92, 0);

        keysRotX[0] = new Keyframe(0f, angle0.x);
        keysRotY[0] = new Keyframe(0f, angle0.y);
        keysRotZ[0] = new Keyframe(0f, angle0.z);
        keysRotW[0] = new Keyframe(0f, angle0.w);


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


    public AnimationClip GenerateAnimationCartasMezclar(int[] posiciones)
    {

        AnimationClip clip = new AnimationClip
        {
            name = "cartas_mezclas_algunas",
            legacy = true,

            wrapMode = WrapMode.Once
        };

        for (ushort i = 0; i < posiciones.Length; i++)
        {

            //if (posiciones[i] - 1 != i) continue;


            Keyframe[] keysX = new Keyframe[6];
            Keyframe[] keysY = new Keyframe[6];

            float timeRot = 1.05f;

            int posCart = posiciones[i] - 1;

            keysX[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoXMin, rangoXMax));
            keysX[4] = new Keyframe(timeRot, 500);
            keysX[5] = new Keyframe(1.40f, gameLogic.initialCardPosition[posCart].anchoredPosition.x);

            keysY[0] = new Keyframe(0f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[1] = new Keyframe(0.18f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[2] = new Keyframe(0.38f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[3] = new Keyframe(0.58f, UnityEngine.Random.Range(rangoYMin, rangoYMax));
            keysY[4] = new Keyframe(timeRot, 250);
            keysY[5] = new Keyframe(1.40f, gameLogic.initialCardPosition[posCart].anchoredPosition.y);


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

            string nombreCarta = "--Juego/carta_" + posiciones[i];
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.x", curvex);
            clip.SetCurve(nombreCarta, typeof(RectTransform), "m_AnchoredPosition.y", curvey);



            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.x", curveRotx);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.y", curveRoty);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.z", curveRotz);
            clip.SetCurve(nombreCarta, typeof(Transform), "localRotation.w", curveRotw);


        }

        return clip;

    }


}
