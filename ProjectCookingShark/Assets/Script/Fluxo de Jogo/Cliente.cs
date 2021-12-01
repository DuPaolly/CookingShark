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

    private Vector3 offset, _posicaoAtual;

    [SerializeField] [Range(0, 1)] float smoothVelocidade = 1;

    [SerializeField] int[] randomSpawn;

    [SerializeField] Button botaoDaInfoDoPedido;
    [SerializeField] GameObject menuDoPedido;

    float tempoSpawnando = 4;

    float tempoDeSpawn = 0;

    bool podeSpawnar = false;

    [SerializeField] ListaAnimacoesClientes listaDosClientes;
    int numeroDaAnimacaoEscolida;
    [SerializeField] Animator anim;

    EstadosDoCliente estadoAtual;
    enum EstadosDoCliente
    {
        Inativo,
        Esperando,
        AndandoAteMesa,
        EsperandoPrato,
        AndandoForaDaTela
    }

    private void Awake()
    {
        sortearMesas();
        tempoDeSpawn = 0;
        CreatRandomSpawnTime();
        botaoDaInfoDoPedido.gameObject.SetActive(false);
    }

    void Start()
    {
        menuDoPedido.SetActive(false);
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
    void FixedUpdate()
    {
        IngredientePremiumParaOCliente();
        
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
            //Debug.Log(tempoDeSpawn);
            if (tempoDeSpawn >= tempoSpawnando)
            {
                podeSpawnar = true;
            }
        }
    }


    void ManagerCliente()
    {
        Debug.Log(estadoAtual);

        switch (estadoAtual)
        {
            case EstadosDoCliente.Inativo:
                ExecutaInativo();
                break;
            case EstadosDoCliente.Esperando:
                ExecutaEsperando();
                break;
            case EstadosDoCliente.AndandoAteMesa:
                ExecutaAndandoMesa();
                break;
            case EstadosDoCliente.EsperandoPrato:
                ExecutaEsperandoPedido();
                break;
            case EstadosDoCliente.AndandoForaDaTela:
                ExecutaAndandoForaDaTela();
                break;
            default:
                break;

        }
    }


    void ExecutaInativo()
    {
        CreatRandomSpawnTime();
        pratoRecebido = null;
        pedidoDoCliente = null;
        sortearMesas();
        SortearAnimacaoCliente();
        ResetIngredientes();
        botaoDaInfoDoPedido.gameObject.SetActive(false);
        estadoAtual = EstadosDoCliente.Esperando;

    }
    void ExecutaEsperando()
    {
        anim.SetBool("EstaAndando", false);
        VaParaPosicao(posicoesDeEntradaESaida[saida]);
        Spawner();
        
        if (podeSpawnar == true)
        {
            estadoAtual = EstadosDoCliente.AndandoAteMesa;
        }
    }

    void ExecutaAndandoMesa()
    {
        anim.SetBool("EstaAndando", true);
        MoverPersonagemParaMesa();
        if (localDetectado != null && localDetectado.mesa)
        {
            
            estadoAtual = EstadosDoCliente.EsperandoPrato;
        }
    }

    void ExecutaEsperandoPedido()
    {
        anim.SetBool("EstaAndando", false);

        if (localDetectado != null && localDetectado.mesa && pedidoDoCliente == null)
        {
            SortearPedidoDoCliente();
            botaoDaInfoDoPedido.gameObject.SetActive(true);
        }

        if (pratoRecebido != null)
            {
                Pontuacao();
                estadoAtual = EstadosDoCliente.AndandoForaDaTela;
            }
    }

    void ExecutaAndandoForaDaTela()
    {
        anim.SetBool("EstaAndando", true);

        VaParaPosicao(posicoesDeEntradaESaida[saida]);
        
        if (localDetectado == posicoesDeEntradaESaida[saida])
        {
            estadoAtual = EstadosDoCliente.Inativo;
        }

    }
    void OldManagerCliente()
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

                if (pedidoDoCliente == null)
                {
                    anim.SetBool("EstaAndando", false);
                    Debug.Log("Personagem parou");
                    SortearPedidoDoCliente();
                    botaoDaInfoDoPedido.gameObject.SetActive(true);

                }
                else
                {
                    MoverPersonagemParaMesa();
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
            anim.SetBool("EstaAndando", false);
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

        PedidoPrint(pedidoDoCliente.SaborPedido01, pedidoDoCliente.SaborPedido02, pedidoDoCliente.IngredienteProibidoPedido.NomeDoIngrediente);

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
        anim.SetBool("EstaAndando", true);
        Debug.Log("andando");

        
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

    [SerializeField] Image BGIngredienteProibido;
    [SerializeField] Image BlockIngredienteProibido;
    [SerializeField] GameObject ingredientePremium1;
    [SerializeField] GameObject ingredientePremium2;
    [SerializeField] public Image[] sabor01;
    [SerializeField] public Image[] sabor02;
    [SerializeField] public Image[] ingredientes;


    public void BotaoParaVisualizarOPedido()
    {
        menuDoPedido.SetActive(true);
    }

    public void FecharOVisualizadorDePedidos()
    {
        menuDoPedido.SetActive(false);
    }
    void PedidoPrint(Sabores.SaboresExistentes sabor1, Sabores.SaboresExistentes sabor2, string ingredienteProib)
    {
        if (sabor1 == Sabores.SaboresExistentes.Apimentado)
        {
            sabor01[0].gameObject.SetActive(true);
            sabor01[1].gameObject.SetActive(false);
            sabor01[2].gameObject.SetActive(false);
        }
        else if (sabor1 == Sabores.SaboresExistentes.Azedo)
        {
            sabor01[0].gameObject.SetActive(false);
            sabor01[1].gameObject.SetActive(true);
            sabor01[2].gameObject.SetActive(false);
        }
        else if(sabor1 == Sabores.SaboresExistentes.Salgado)
        {
            sabor01[0].gameObject.SetActive(false);
            sabor01[1].gameObject.SetActive(false);
            sabor01[2].gameObject.SetActive(true);
        }

        if (ingredienteProib == "AnemonaPicada")
        {
            ingredientes[0].gameObject.SetActive(true);
            ingredientes[1].gameObject.SetActive(false);
            ingredientes[2].gameObject.SetActive(false);
            ingredientes[3].gameObject.SetActive(false);
            ingredientes[4].gameObject.SetActive(false);
            ingredientes[5].gameObject.SetActive(false);
        }
        else if (ingredienteProib == "Areia Temperada")
        {
            ingredientes[0].gameObject.SetActive(false);
            ingredientes[1].gameObject.SetActive(true);
            ingredientes[2].gameObject.SetActive(false);
            ingredientes[3].gameObject.SetActive(false);
            ingredientes[4].gameObject.SetActive(false);
            ingredientes[5].gameObject.SetActive(false);
        }
        else if (ingredienteProib == "Conchas Refogadas")
        {
            ingredientes[0].gameObject.SetActive(false);
            ingredientes[1].gameObject.SetActive(false);
            ingredientes[2].gameObject.SetActive(true);
            ingredientes[3].gameObject.SetActive(false);
            ingredientes[4].gameObject.SetActive(false);
            ingredientes[5].gameObject.SetActive(false);
        }
        else if (ingredienteProib == "RaspasDeCoral")
        {
            ingredientes[0].gameObject.SetActive(false);
            ingredientes[1].gameObject.SetActive(false);
            ingredientes[2].gameObject.SetActive(false);
            ingredientes[3].gameObject.SetActive(true);
            ingredientes[4].gameObject.SetActive(false);
            ingredientes[5].gameObject.SetActive(false);
        }
        else if (ingredienteProib == "Pimenta Pacífica")
        {
            ingredientes[0].gameObject.SetActive(false);
            ingredientes[1].gameObject.SetActive(false);
            ingredientes[2].gameObject.SetActive(false);
            ingredientes[3].gameObject.SetActive(false);
            ingredientes[4].gameObject.SetActive(true);
            ingredientes[5].gameObject.SetActive(false);
        }
        else if (ingredienteProib == "Sal Oceânico")
        {
            ingredientes[0].gameObject.SetActive(false);
            ingredientes[1].gameObject.SetActive(false);
            ingredientes[2].gameObject.SetActive(false);
            ingredientes[3].gameObject.SetActive(false);
            ingredientes[4].gameObject.SetActive(false);
            ingredientes[5].gameObject.SetActive(true);
        }
        

        if (pedidoDoCliente.saborPedido03 != Sabores.SaboresExistentes.nenhum)
        {
            if (pedidoDoCliente.saborPedido03 == pedidoDoCliente.SaborPedido01)
            {
                ingredientePremium1.gameObject.SetActive(true);
                ingredientePremium2.gameObject.SetActive(false);
            }
            else if(pedidoDoCliente.saborPedido03 == pedidoDoCliente.SaborPedido02)
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


    void SortearAnimacaoCliente()
    {
        numeroDaAnimacaoEscolida = SortearNumeroDaAnimacao();

        anim.runtimeAnimatorController = listaDosClientes.animacoesPossiveis[numeroDaAnimacaoEscolida];
    }

    public int SortearNumeroDaAnimacao()
    {
        return Random.Range(0, listaDosClientes.animacoesPossiveis.Length);
    }

}
