using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuStartMoviment : MonoBehaviour
{

    [SerializeField] float rotacaoDoBackground = 1;

    [SerializeField] float velocidadeY = 1;

    [SerializeField] float alturaLimite = 1;

    [SerializeField] private thisObjectIs tipoDeObjeto;

    [SerializeField] private Positions posicaoDeSpawnDasBolhas;

    private float escalaInicial = 2;

    private bool paraCima = true;

    private Vector3 _originalPosition;

    float rotacaoAtual = 0;

    enum thisObjectIs
    {
        Boat,    
        BackGround,
        Bubble
    }

    void Awake()
    {
        ParametrosIniciaisDoObjeto();
    }
    void Update()
    {
        RotacioneEsteObjeto();
    }

    private void RotacioneEsteObjeto()
    {
        if (tipoDeObjeto == thisObjectIs.BackGround)
        {
            rotacaoDoObjeto(rotacaoDoBackground);
        }
        if (tipoDeObjeto == thisObjectIs.Boat)
        {
            if (paraCima)
            {
                if(transform.position.y >= _originalPosition.y + alturaLimite)
                {
                    paraCima = false;
                }
                MoverParaCima(velocidadeY);
            }
            else if (!paraCima)
            {
                if (transform.position.y <= _originalPosition.y - alturaLimite)
                {
                    paraCima = true;
                }
                MoverParaBaixo(velocidadeY);
            }
        }
        if (tipoDeObjeto == thisObjectIs.Bubble)
        {
            MoverBolha();
        }
    }
    
    private void rotacaoDoObjeto(float velocidadeDeGiro)
    {
        rotacaoAtual += velocidadeDeGiro;

        transform.rotation = Quaternion.Euler(0, 0, rotacaoAtual);
    }

    private void MoverBolha()
    {
        MoverParaEsquerda(velocidadeY);
        MoverParaCima(velocidadeY);
    }


    private void MoverParaCima(float velocidade)
    {
        transform.position += Vector3.up * velocidade * Time.deltaTime;
    }
    private void MoverParaEsquerda(float velocidade)
    {
        transform.position += Vector3.left * velocidade * Time.deltaTime;
    }
    private void MoverParaBaixo(float velocidade)
    {
        transform.position += Vector3.down * velocidade * Time.deltaTime;
    }

    private void ParametrosIniciaisDoObjeto()
    {
        rotacaoDoBackground = rotacaoDoBackground / 100;
        rotacaoAtual = 0;
        _originalPosition = transform.position;
        alturaLimite = alturaLimite / 100;
        startBolha();
    }

    private void startBolha()
    {
        if(tipoDeObjeto == thisObjectIs.Bubble)
        {
            escalaInicial = escalaInicial / 100;
            transform.position = posicaoDeSpawnDasBolhas.transform.position;
            _originalPosition = posicaoDeSpawnDasBolhas.transform.position;
            transform.localScale = new Vector3(escalaInicial, escalaInicial, escalaInicial);
        }
    }
    public int SortearNumero(float[] numeros)
    {
        return Random.Range(0, numeros.Length);
    }
}
