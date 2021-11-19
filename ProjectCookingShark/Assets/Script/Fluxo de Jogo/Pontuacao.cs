using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuacao : MonoBehaviour
{

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
        Debug.Log(pontuacaoDaFase);
    }

}
