using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTest : MonoBehaviour
{
    [SerializeField] Canvas telaDeFimDeJogo;
    [SerializeField] Text textoDoTempoDeJogo;

    float tempoReserva;

    int tempoDeJogoNoTimer;

    int tempoDeJogo = 0;

    int tempoTotalDeJogo;

    bool inicioDePartida = false;
    bool finalDePartida = false;

    [SerializeField] int minutosDeJogo = 2;

    void Awake()
    {
        inicioDePartida = true;
        tempoTotalDeJogo = SegundosParaMinuto(minutosDeJogo);
        //telaDeFimDeJogo.gameObject.SetActive(false);
    }

    void Update()
    {
        TempoDeFase();
    }

    void TempoDeFase()
    {
        tempoDeJogoNoTimer = tempoTotalDeJogo - tempoDeJogo;
        textoDoTempoDeJogo.text = tempoDeJogoNoTimer.ToString();
        if (finalDePartida == false)
        {
            tempoReserva += Time.deltaTime;
            tempoDeJogo = (int)tempoReserva;
            //Debug.Log(tempoDeJogo);
            if (tempoDeJogo >= tempoTotalDeJogo)
            {
                finalDePartida = true;
                telaDeFimDeJogo.gameObject.SetActive(true);
            }
        }
    }

    int SegundosParaMinuto(int minutos)
    {
        Debug.Log(minutos);
        return minutos * 60;
    }

    

    //tempoDeCozinhando += Time.deltaTime;

}
