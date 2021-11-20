using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    [SerializeField] Text pontuacaoNoJogo;
    [SerializeField] Text pontuacaoTelaFinal;


    public int pontuacaoDaFase = 0;
    private void Awake()
    {
        pontuacaoDaFase = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PontuacaoUpdate();
    }

    void PontuacaoUpdate()
    {
        pontuacaoNoJogo.text = pontuacaoDaFase.ToString();
        pontuacaoTelaFinal.text = pontuacaoDaFase.ToString();
    }

}
