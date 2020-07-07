using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CartasBosque
{
    public string name;
    public bool mezclar;
    public int[] cartasMezclar;
    public bool bombilla;
    public bool fear;
    public bool startPosition;
    public bool startNewGame;
    public bool isVisited;
    public Sprite imagenBosque;

    

}


[CreateAssetMenu(menuName = "ScriptableObjects/CartasBosque", fileName = "BosqueStats")]
public sealed class BosqueStats : ScriptableObject
{
    [Header("[Cartas Bosque]")]
    public CartasBosque[] cartasBosque;

}