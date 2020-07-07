using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsJugador
{
    public short tokensMax;
    public short tokensCurrent;

    public short bombillasMax;
    public short bombillasCurrent;

    public StatsJugador() { }

    public StatsJugador(short tokensMax, short tokensCurrent, short bombillaMax, short bombillasCurrent)
    {
        this.tokensMax = tokensMax;
        this.tokensCurrent = tokensCurrent;

        this.bombillasMax = bombillaMax;
        this.bombillasCurrent = bombillasCurrent;
    
    }




}

public class BotonesPersonaje : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField] private GameLogic gameLogic = null;

    [SerializeField] private TextMeshProUGUI tokensText = null;
    [SerializeField] private TextMeshProUGUI bombillasText = null;
    [SerializeField] private Image personajeSeleccionado = null;


    public StatsJugador statsJugador = new StatsJugador(2, 0, 2, 0);

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

    public void SetIniitialStatsPersonajePrincipal(Personajes personajePrincipalStats)
    {

        statsJugador.bombillasMax = personajePrincipalStats.bombilla;
        statsJugador.tokensMax = personajePrincipalStats.token;
        statsJugador.tokensCurrent = 0;
        statsJugador.bombillasCurrent = 0;

        ShowStatsPersonajePrincipal();

    }

    public void SumarBombillas()
    {

        if (statsJugador.bombillasCurrent < statsJugador.bombillasMax)
        { 
        
            statsJugador.bombillasCurrent++;
            ShowStatsPersonajePrincipal();
        
        }

    }


    public void SumarFear()
    {

        if (statsJugador.tokensCurrent < statsJugador.tokensMax)
        { 
        
            statsJugador.tokensCurrent++;
            ShowStatsPersonajePrincipal();
    
        
        }

    }

    public void ShowStatsPersonajePrincipal()
    {

        bombillasText.text = statsJugador.bombillasCurrent + " / " + statsJugador.bombillasMax;
        tokensText.text = statsJugador.tokensCurrent + " / " + statsJugador.tokensMax;


    }

    public void ResetStatsPersonajePrincipal()
    {

        statsJugador.bombillasCurrent = 0;
        statsJugador.tokensCurrent = 0;

        bombillasText.text = statsJugador.bombillasCurrent + " / " + statsJugador.bombillasMax;
        tokensText.text = statsJugador.tokensCurrent + " / " + statsJugador.tokensMax;

        personajeSeleccionado.sprite = gameLogic.whiteBackgroundCard;



    }


}
