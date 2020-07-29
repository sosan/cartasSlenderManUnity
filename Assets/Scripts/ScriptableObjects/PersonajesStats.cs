using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


[System.Serializable]
public struct Personajes
{
    public string name;
    public short tokensNeededToLose;
    public short bombilla;
    public short movimientoOriginal;
    public short movimientoMax;
    public bool isMaxMovementUsed;
    public bool mezclarEncuentroSlimmer;

    public bool canRemoveToken;
    public bool isPosibleShowNextCard;
    public Sprite imagenPersonaje;

}


[CreateAssetMenu(menuName = "ScriptableObjects/PersonajesStats", fileName = "PersonajesStats")]
public sealed class PersonajesStats : ScriptableObject
{
    [Header("[Characters]")]
    public Personajes[] personajes;

}