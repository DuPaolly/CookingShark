using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Cliente : MonoBehaviour
{

    int pontuacao = 0;

    [SerializeField] Pontuacao pontuacaoAtual;

    ClienteManager manager;

    public int sortearNumero;

    [SerializeField] public ListaDePedidos listaDePedidos;

    public Pedido pedidoDoCliente;

    public Receita pratoRecebido = null;

    public Frigideira.IngredientePremium ingredientePremium;

    public Ingrediente saborPremiumIngrediente;

    [SerializeField] public Positions[] posicoesDeEntradaESaida;

    [SerializeField] public Positions posicoesDosClientes;

    Positions localDeSaida;

    Cliente clienteDetectado;

    private Positions localDetectado;

    private bool primeiroIngrediente = false;

    private bool segundoIngrediente = false;

    private bool terceiroIngrediente = false;

    private Vector3 _originalPosition;

    public int saida;

    public bool podeIr;

    private Vector2 offset, _posicaoAtual;

    [SerializeField] [Range(0, 1)] float smoothVelocidade = 1;

    [SerializeField] int[] randomSpawn;

    [SerializeField] Button botaoDaInfoDoPedido;

    float tempoSpawnando = 4;

    float tempoDeSpawn = 0;

    bool podeSpawnar = false;

    private void Awake()
    {
        sortearMesas();
        tempoDeSpawn = 0;
        CreatRandomSpawnTime();
        botaoDaInfoDoPedido.gameObject.SetActive(false);
    }

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
    void FixedUpdate()
    {
        IngredientePremiumParaOCliente();
        Spawner();
        ManagerCliente();
    }

    private void OnTriggerEnter2D(Collider2D areaEmQueEncostou)
    {
        Cliente clienteAchado = areaEmQueEncostou.GetComponent<Cliente>();

        if (clienteAchado != null)
        {
            clienteDetectado = clienteAchado;
        }

        Positions localAchado = areaEmQueEncostou.GetComponent<Positions>();

        if (localAchado != null)
        {
            localDetectado = localAchado;
        }
    }

    private void OnTriggerExit2D(Collider2D areaEmQueSaiu)
    {

        Cliente clienteAchado = areaEmQueSaiu.GetComponent<Cliente>();

        if (clienteAchado != null)
        {
            clienteDetectado = null;
        }

        Positions localAchado = areaEmQueSaiu.GetComponent<Positions>();

        if (localAchado != null)
        {
            localDetectado = null;
        }
    }

    void Spawner()
    {
        if (podeSpawnar == false)
        {
            tempoDeSpawn += Time.deltaTime;
            Debug.Log(tempoDeSpawn);
            if (tempoDeSpawn >= tempoSpawnando)
            {
                podeSpawnar = true;
            }
        }
    }

    void ManagerCliente()
    {
        if (pratoRecebido == null && podeSpawnar == true)
        {
            if (localDetectado != null && !localDetectado.mesa)
            {
                //sortearMesas();
                MoverPersonagemParaMesa();
            }
            else if (localDetectado != null && localDetectado.mesa)
            {
                
                MoverPersonagemParaMesa();
                if (pedidoDoCliente == null)
                {
                    SortearPedidoDoCliente();
                    botaoDaInfoDoPedido.gameObject.SetActive(true);
                }
            }
            else
            {
                MoverPersonagemParaMesa();
            }
        }
        else if(pratoRecebido == null && podeSpawnar == false) 
        {
            VaParaPosicao(posicoesDeEntradaESaida[saida]);
        }
        else
        {
            VaParaPosicao(posicoesDeEntradaESaida[saida]);
            Pontuacao();
            if (localDetectado != null)
            {
                CreatRandomSpawnTime();
                if (localDetectado == posicoesDeEntradaESaida[saida])
                {
                    pratoRecebido = null;
                    pedidoDoCliente = null;
                    sortearMesas();
                    ResetIngredientes();
                }
            }
        }
    }

    private void MoverPersonagemParaMesa ()
    {
        
        VaParaPosicao(posicoesDosClientes); 
    }

    private void IngredientePremiumParaOCliente()
    {
        if (pratoRecebido != null)
        {
            if (ingredientePremium == Frigideira.IngredientePremium.PrimeiroIngredientePremium)
            {
                saborPremiumIngrediente = pratoRecebido.ingredientes01;
            }
            else if (ingredientePremium == Frigideira.IngredientePremium.SegundoIngredientePremium)
            {
                saborPremiumIngrediente = pratoRecebido.ingredientes02;
            }
            else if (ingredientePremium == Frigideira.IngredientePremium.SemIngredientePremium)
            {
                saborPremiumIngrediente = null;
            }
        }
    }

    

    public void SortearPedidoDoCliente()
    {
        sortearNumero = SortearNumeroDoPedido();

        pedidoDoCliente = listaDePedidos.pedidosPossiveis[sortearNumero];

        if (pedidoDoCliente.PremiumIngredienteDoCliente == Pedido.IngredientePremiumDoClente.PrimeiroIngredientePremium)
        {
            pedidoDoCliente.saborPedido03 = pedidoDoCliente.SaborPedido01;
        }
        else if (pedidoDoCliente.PremiumIngredienteDoCliente == Pedido.IngredientePremiumDoClente.SegundoIngredientePremium)
        {
            pedidoDoCliente.saborPedido03 = pedidoDoCliente.SaborPedido02;
        }
        else
        {
            pedidoDoCliente.saborPedido03 = Sabores.SaboresExistentes.nenhum;
        }
        //Pedidos aki!!

        TextoDoPedido(pedidoDoCliente.SaborPedido01, pedidoDoCliente.SaborPedido02, pedidoDoCliente.IngredienteProibidoPedido.NomeDoIngrediente);

        Debug.Log(pedidoDoCliente.SaborPedido01);
        Debug.Log(pedidoDoCliente.SaborPedido02);
        Debug.Log(pedidoDoCliente.IngredienteProibidoPedido);
        Debug.Log(pedidoDoCliente.saborPedido03);

    }

    public int SortearNumeroDoPedido()
    {
        return Random.Range(0, listaDePedidos.pedidosPossiveis.Length);
    }

    void ResetIngredientes()
    {
        primeiroIngrediente = false;
        segundoIngrediente = false;
        terceiroIngrediente = false;
    }

    public int SortearLocal(Positions[] posicao)
    {
        return Random.Range(0, posicao.Length);
    }

    public void sortearMesas()
    {
        saida = SortearLocal(posicoesDeEntradaESaida);
        _originalPosition = posicoesDeEntradaESaida[SortearLocal(posicoesDeEntradaESaida)].transform.position;
        transform.position = _originalPosition;
    }

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

    public bool PodeVoltarAPosiçãoInicial()
    {
        return true;
    }

    public bool JaChegouNoDestino()
    {
        return false;
    }

    void CreatRandomSpawnTime()
    {
        tempoSpawnando = randomSpawn[SortearLocal()];
        Debug.Log(tempoSpawnando);
    }

    public int SortearLocal()
    {
        podeSpawnar = false;
        tempoDeSpawn = 0;
        return Random.Range(0, randomSpawn.Length);
    }

    public void Pontuacao()
    {

        if (pedidoDoCliente.IngredienteProibidoPedido.NomeDoIngrediente != pratoRecebido.ingredientes01.NomeDoIngrediente &&
           pedidoDoCliente.IngredienteProibidoPedido.NomeDoIngrediente != pratoRecebido.ingredientes02.NomeDoIngrediente)
        {

            if (pratoRecebido != null)
            {

                if (primeiroIngrediente == false)
                {
                    Debug.Log(pontuacaoAtual.pontuacaoDaFase);

                    if (pratoRecebido.ingredientes01.Sabor.Equals(pedidoDoCliente.SaborPedido01))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        primeiroIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                    else if (pratoRecebido.ingredientes02.Sabor.Equals(pedidoDoCliente.SaborPedido01))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        primeiroIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                }
                if (segundoIngrediente == false)
                {
                    if (pratoRecebido.ingredientes01.Sabor.Equals(pedidoDoCliente.SaborPedido02))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        segundoIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                    else if (pratoRecebido.ingredientes02.Sabor.Equals(pedidoDoCliente.SaborPedido02))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        segundoIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                }
                if (terceiroIngrediente == false && saborPremiumIngrediente != null)
                {
                    Debug.Log(saborPremiumIngrediente.Sabor);
                    Debug.Log(pedidoDoCliente.saborPedido03);

                    if (saborPremiumIngrediente.Sabor.Equals(pedidoDoCliente.saborPedido03))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        terceiroIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                    else if (saborPremiumIngrediente.Sabor.Equals(pedidoDoCliente.saborPedido03))
                    {
                        pontuacaoAtual.pontuacaoDaFase += 100;
                        terceiroIngrediente = true;
                        Debug.Log(pontuacaoAtual.pontuacaoDaFase);
                    }
                }
            }

        }
    }

    //[SerializeField] Text TextoPrato;
    //TextoPrato.text = "Arroz";
    //TextoPrato.gameObject.SetActive(true);

    [SerializeField] Text saborParaImprimir1;
    [SerializeField] Text saborParaImprimir2;
    [SerializeField] Text ingredienteProibidoParaImprimir;
    [SerializeField] Image ingredientePremium1;
    [SerializeField] Image ingredientePremium2;
    void TextoDoPedido(Sabores.SaboresExistentes sabor1, Sabores.SaboresExistentes sabor2, string ingredienteProib)
    {
        //Enum.GetName(typeof(Sabores.SaboresExistentes), sabor1);
        saborParaImprimir1.text = Enum.GetName(typeof(Sabores.SaboresExistentes), sabor1);
        saborParaImprimir1.gameObject.SetActive(true);

        saborParaImprimir2.text = Enum.GetName(typeof(Sabores.SaboresExistentes), sabor2);
        saborParaImprimir2.gameObject.SetActive(true);

        ingredienteProibidoParaImprimir.text = ingredienteProib;
        ingredienteProibidoParaImprimir.gameObject.SetActive(true);

        if(pedidoDoCliente.saborPedido03 != Sabores.SaboresExistentes.nenhum)
        {
            if (pedidoDoCliente.saborPedido03 == pedidoDoCliente.SaborPedido01)
            {
                ingredientePremium1.gameObject.SetActive(true);
                ingredientePremium2.gameObject.SetActive(false);
            }
            else
            {
                ingredientePremium1.gameObject.SetActive(false);
                ingredientePremium2.gameObject.SetActive(true);
            }
  
        }
        else
        {
            ingredientePremium1.gameObject.SetActive(false);
            ingredientePremium2.gameObject.SetActive(false);
        }
    }

}
