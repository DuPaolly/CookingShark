using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    [SerializeField] Canvas telaDeFimDeJogo;

    float tempoDeJogo = 0;

    float tempoTotalDeJogo;

    bool inicioDePartida = false;
    bool finalDePartida = false;

    [SerializeField] float minutosDeJogo = 2;

    void Awake()
    {
        inicioDePartida = true;
        tempoTotalDeJogo = SegundosParaMinuto(minutosDeJogo);
        telaDeFimDeJogo.gameObject.SetActive(false);
    }

    void Update()
    {
        TempoDeFase();
    }

    void TempoDeFase()
    {

        if (finalDePartida == false)
        {
            tempoDeJogo += Time.deltaTime;
            Debug.Log(tempoDeJogo);
            if (tempoDeJogo >= tempoTotalDeJogo)
            {
                finalDePartida = true;
                telaDeFimDeJogo.gameObject.SetActive(true);
            }
        }
    }

    float SegundosParaMinuto(float minutos)
    {
        Debug.Log(minutos);
        return minutos * 60;
    }

    //tempoDeCozinhando += Time.deltaTime;

}
