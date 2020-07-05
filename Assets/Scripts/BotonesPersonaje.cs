using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsJugador
{
    public short tokens;
    public short bombillas;

    public StatsJugador() { }

    public StatsJugador(short tokens, short bombillas)
    {
        this.tokens = tokens;
        this.bombillas = bombillas;
    
    }




}

public class BotonesPersonaje : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField] private GameLogic gameLogic = null;

    [SerializeField] private TextMeshProUGUI tokensText = null;
    [SerializeField] private TextMeshProUGUI bombillasText = null;
    [SerializeField] private Image personajeSeleccionado = null;


    public StatsJugador statsJugador = new StatsJugador(3, 3);

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ClickedToken()
    { 
    
    
    }

    public void ClickedBombilla()
    {



    }

    public void ShowStatsPersonajePrincipal(Personajes personajePrincipalStats)
    {

        statsJugador.bombillas = personajePrincipalStats.bombilla;
        statsJugador.tokens = personajePrincipalStats.token;
        
        bombillasText.text = "X" + personajePrincipalStats.bombilla;
        tokensText.text = "X" + personajePrincipalStats.token;

    }

    public void ResetStatsPersonajePrincipal()
    {

        statsJugador.bombillas = 0;
        statsJugador.tokens = 0;

        bombillasText.text = "X0";
        tokensText.text = "X0";

        personajeSeleccionado.sprite = gameLogic.whiteBackgroundCard;



    }


}
