using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuStartMoviment : MonoBehaviour
{

    [SerializeField] float velocidadeDeRotacao = 1;

    [SerializeField] float VelocidadeDeBoiar = 1;

    [SerializeField] float velocidadeY = 1;

    [SerializeField] float alturaLimite = 1;

    [SerializeField] float rotacaoLimite = 1;

    [SerializeField] private thisObjectIs tipoDeObjeto;

    [SerializeField] private Positions posicaoDeSpawnDaLogo;

    [SerializeField] private Positions posicaoFinalDeSpawnDaLogo;

    [SerializeField] private ParticleSystem particula01;

    [SerializeField] private ParticleSystem particula02;

    [SerializeField] private ParticleSystem particula03;

    //[SerializeField] private Positions boiaDeCima;

    //[SerializeField] private Positions boiaDeBaixo;

    [SerializeField] [Range(0, 1)] float smoothVelocidade = 1;

    private Positions detectarPosition;

    private estadoDaLogo estadoAtualDaLogo = estadoDaLogo.entrando;

    private bool paraCima = true;

    private bool girandoSentidoHorario = false;

    private Vector3 _originalPosition;

    private Vector2 offset, _posicaoAtual;

    float rotacaoAtual = 0;

    float tempoDoEfetoEmTela = 4;

    float tempoAtualDoEfeito = 0;

    bool  pararAContagem = false;

    enum estadoDaLogo
    {
        entrando,
        saindo,
        parado
    }

    enum thisObjectIs
    {
        Boat,    
        BackGround,
        Logo
    }

    void Start()
    {
        ParametrosIniciaisDoObjeto();
    }
    void Update()
    {
        AnimeEsteObjeto();
        MudarEstadoDaLogo();
    }

    private void OnTriggerEnter2D(Collider2D areaEmQueEncostou)
    {
        Positions localAchado = areaEmQueEncostou.GetComponent<Positions>();

        if (localAchado != null)
        {
            detectarPosition = localAchado;
        }
    }

    private void OnTriggerExit2D(Collider2D areaEmQueSaiu)
    {
        Positions localAchado = areaEmQueSaiu.GetComponent<Positions>();

        if (localAchado != null)
        {
            detectarPosition = null;
        }
    }

    private void AnimeEsteObjeto()
    {
        if (tipoDeObjeto == thisObjectIs.BackGround)
        {
            RotacaoDoObjeto(velocidadeDeRotacao);
        }
        if (tipoDeObjeto == thisObjectIs.Boat)
        {
            MoverBarco();
        }
        if (tipoDeObjeto == thisObjectIs.Logo)
        {
            MoverEBoiar();
        }
    }
    
    private void RotacaoDoObjeto(float velocidadeDeGiro)
    {
        rotacaoAtual += velocidadeDeGiro;

        transform.rotation = Quaternion.Euler(0, 0, rotacaoAtual);
    }
    private void MoverEBoiar()
    {
        if (estadoAtualDaLogo == estadoDaLogo.parado)
        {
            PararEfeitoDeParticula();
            //MoverLogo();
            //BoiarObjeto(VelocidadeDeBoiar);
        }
        else if (estadoAtualDaLogo == estadoDaLogo.entrando)
        {
            VaParaPosicao(posicaoFinalDeSpawnDaLogo);
            //BoiarObjeto(VelocidadeDeBoiar);
        }
        else if (estadoAtualDaLogo == estadoDaLogo.saindo)
        {
            VaParaPosicao(posicaoDeSpawnDaLogo);
            //BoiarObjeto(VelocidadeDeBoiar);
        }
    }

    private void PararEfeitoDeParticula()
    {
        if (!pararAContagem)
        {
            tempoAtualDoEfeito += Time.deltaTime;
        }
        if(tempoDoEfetoEmTela <= tempoAtualDoEfeito)
        {
            particula01.Stop();
            particula02.Stop();
            particula03.Stop();
            pararAContagem = true;
        }      
    }

    private void MoverBarco()
    {
        if (paraCima)
        {
            if (transform.position.y >= _originalPosition.y + alturaLimite)
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

    //private void MoverLogo()
    //{
    //    if (paraCima)
    //    {
    //        //Debug.Log("Esta Passando por aqui");
    //        if (transform.position.y > boiaDeCima.transform.position.y - 0.01)
    //        {
    //            paraCima = false;
    //        }
    //        VaParaPosicao(boiaDeCima);
    //    }
    //    else if (!paraCima)
    //    {
    //        if (transform.position.y < boiaDeBaixo.transform.position.y + 0.01)
    //        {
    //            paraCima = true;
    //        }
    //        VaParaPosicao(boiaDeBaixo);
    //    }
    //}

    private void MudarEstadoDaLogo()
    {
        if (estadoAtualDaLogo != estadoDaLogo.saindo)
        {
            if (detectarPosition == posicaoFinalDeSpawnDaLogo)
            {
                estadoAtualDaLogo = estadoDaLogo.parado;
            }else if(posicaoFinalDeSpawnDaLogo == null)
            {
                Debug.Log("ERROR");
            }
        }else if (estadoAtualDaLogo == null)
        {
            Debug.Log("ERROR");
        }
    }

    public void SairLogo()
    {
        estadoAtualDaLogo = estadoDaLogo.saindo;
        particula01.Play();
        particula02.Play();
        particula03.Play();
    }

    //transform.position.y <= posicaoFinalDeSpawnDaLogo.transform.position.y + 0.01
    //            && transform.position.y >= posicaoFinalDeSpawnDaLogo.transform.position.y - 0.01

    //private void BoiarObjeto(float velocidadeDeGiro)
    //{
    //    if (tipoDeObjeto == thisObjectIs.Logo)
    //    {
    //        if (girandoSentidoHorario)
    //        {
    //            if (transform.rotation.z >= 0 + rotacaoLimite)
    //            {
    //                girandoSentidoHorario = false;
    //            }
    //            RotacaoDoObjeto(velocidadeDeGiro);
    //        }
    //        else if (!girandoSentidoHorario)
    //        {
    //            if (transform.rotation.z <= 0 - rotacaoLimite)
    //            {
    //                girandoSentidoHorario = true;
    //            }
    //            RotacaoDoObjeto(-velocidadeDeGiro);
    //        }
    //    }
    //}

    public void VaParaPosicao(Positions posicao)
    {

        _originalPosition = posicao.transform.position;

        _posicaoAtual = transform.position;

        _posicaoAtual = Vector3.Lerp(
            transform.position,
            _originalPosition,
            smoothVelocidade * Time.deltaTime);

        transform.position = _posicaoAtual;

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
        velocidadeDeRotacao = velocidadeDeRotacao / 100;
        rotacaoAtual = 0;
        _originalPosition = transform.position;
        alturaLimite = alturaLimite / 100;
        startLogo();
    }

    private void startLogo()
    {
        if(tipoDeObjeto == thisObjectIs.Logo)
        {
            estadoAtualDaLogo = estadoDaLogo.entrando;
            transform.position = posicaoDeSpawnDaLogo.transform.position;
            _originalPosition = posicaoDeSpawnDaLogo.transform.position;
        }
    }
    public int SortearNumero(float[] numeros)
    {
        return Random.Range(0, numeros.Length);
    }
}
