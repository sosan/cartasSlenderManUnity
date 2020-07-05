using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


[System.Serializable]
public struct Personajes
{
    public string name;
    public short token;
    public short bombilla;
    public Sprite imagenPersonaje;

}


[CreateAssetMenu(menuName = "ScriptableObjects/PersonajesStats", fileName = "PersonajesStats")]
public sealed class PersonajesStats : ScriptableObject
{
    [Header("[Characters]")]
    public Personajes[] personajes;

}