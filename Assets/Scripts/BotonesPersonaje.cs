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

    public bool isMaxMovementUsed;
    public bool isMaxMovementCanBeUsed;
    public short movimientoOriginal;
    public bool canRemoveToken;
    public bool isPosibleShowNextCard;
    public short movimientoMaximoCuadros;
    public bool mezclarEncuentroSlimmer;

    public StatsJugador() { }

    public StatsJugador(short tokensMax, short tokensCurrent, short bombillaMax, short bombillasCurrent, short movimientoMax)
    {
        this.tokensMax = tokensMax;
        this.tokensCurrent = tokensCurrent;
        this.bombillasMax = bombillaMax;
        this.bombillasCurrent = bombillasCurrent;
        this.movimientoMaximoCuadros = movimientoMax;
    
    }




}

public class BotonesPersonaje : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField] private GameLogic gameLogic = null;

    [SerializeField] private TextMeshProUGUI tokensText = null;
    [SerializeField] private TextMeshProUGUI bombillasText = null;
    [SerializeField] private Image personajeSeleccionado = null;

    [Header("Texto Rondas")]
    [SerializeField] private TextMeshProUGUI textoRondas = null;

    public StatsJugador statsJugador = new StatsJugador(tokensMax: 2, tokensCurrent: 0, bombillaMax: 2, bombillasCurrent: 0, movimientoMax: 1);

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
        statsJugador.tokensMax = personajePrincipalStats.tokensNeededToLose;
        statsJugador.tokensCurrent = 0;
        statsJugador.bombillasCurrent = 0;
        statsJugador.isMaxMovementCanBeUsed = personajePrincipalStats.isMaxMovementCanBeUsed;
        statsJugador.isPosibleShowNextCard = personajePrincipalStats.isPosibleShowNextCard;
        statsJugador.mezclarEncuentroSlimmer = personajePrincipalStats.mezclarEncuentroSlimmer;
        statsJugador.movimientoMaximoCuadros = personajePrincipalStats.movimientoMax;
        statsJugador.movimientoOriginal = personajePrincipalStats.movimientoOriginal;
        statsJugador.canRemoveToken = personajePrincipalStats.canRemoveToken;
        

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


    //public void SumarFear()
    //{

    //    if (statsJugador.tokensCurrent < statsJugador.tokensMax)
    //    { 
        
    //        statsJugador.tokensCurrent++;
    //        ShowStatsPersonajePrincipal();
    
        
    //    }

    //}


    //public void ShowRondas()
    //{

    //    textoRondas.text = Localization.Get("rondas") + " " + statsJugador.tokensCurrent;

    //}

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
