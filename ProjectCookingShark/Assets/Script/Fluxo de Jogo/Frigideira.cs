using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Frigideira : Receita
{
    public Ingrediente saborPremiumIngrediente = null;

    IngredientePremium premiumIngredientes = IngredientePremium.SemIngredientePremium;

    [SerializeField] private Receita Gororoba;

    private Lixeira lixeira;

    private BandejaCliente pratoDoCliente;

    public Receita receitaAtual;

    private bool podeVoltar;

    private Vector3 _originalPosition;

    private Vector2 offset, _posicaoAtual;

    int smoothVelocidade = 20;


    [SerializeField] CheckerPrato inventarioDeReceita;

    [SerializeField] public SpriteRenderer spriteNaFrigideira;

    public enum IngredientePremium
    {
        SemIngredientePremium,
        PrimeiroIngredientePremium,
        SegundoIngredientePremium
    }
    private void Awake()
    {
        ObjectStart();
    }

    private void FixedUpdate()
    {
        VolteParaPosicao();
    }
    private void OnMouseDrag()
    {
        MouseDragUpdate();
    }
    private void OnMouseUp()
    {
        VolteParaPosicao();
        MouseDropObject();
    }

    private void OnTriggerEnter2D(Collider2D areaEmQueEncostou)
    {
        Lixeira lixeiraEncontrada = areaEmQueEncostou.GetComponent<Lixeira>();

        if(lixeiraEncontrada != null)
        {
            lixeira = lixeiraEncontrada;
        }

        BandejaCliente pratoEncontrado = areaEmQueEncostou.GetComponent<BandejaCliente>();

        if (pratoEncontrado != null)
        {
            pratoDoCliente = pratoEncontrado;
        }
    }
    private void OnTriggerExit2D(Collider2D areaEmQueSaiu)
    {
        Lixeira lixeiraEncontrada = areaEmQueSaiu.GetComponent<Lixeira>();

        if (lixeiraEncontrada != null)
        {
            lixeira = null;
        }

        BandejaCliente pratoQuePerdeu = areaEmQueSaiu.GetComponent<BandejaCliente>();

        if (pratoQuePerdeu != null)
        {
            pratoDoCliente = null;
        }
    }
    public bool PodeReceberIngrediente(Ingrediente ingredienteParaAdicionar)
    {
       
        if(receitaAtual == null) { 
            if(ingredientes01 == null && ingredientes02 == null)
            {
                ingredientes01 = ingredienteParaAdicionar;
                return true;
            }
            else if(ingredientes01 != null && ingredientes02 != null)
            {
                if(saborPremiumIngrediente == null)
                {
                    if(ingredienteParaAdicionar.NomeDoIngrediente == ingredientes01.NomeDoIngrediente)
                    { 
                        saborPremiumIngrediente = ingredienteParaAdicionar;
                        premiumIngredientes = Frigideira.IngredientePremium.PrimeiroIngredientePremium;
                        return true;
                    }
                    else if(ingredienteParaAdicionar.NomeDoIngrediente == ingredientes02.NomeDoIngrediente)
                    {
                        saborPremiumIngrediente = ingredienteParaAdicionar;
                        premiumIngredientes = Frigideira.IngredientePremium.SegundoIngredientePremium;
                        return true;
                    }
                }

                return false;
        
            }
            else
            {
                if (ingredientes01 != null)
                {
                    if (ingredientes01.NomeDoIngrediente.Equals(ingredienteParaAdicionar.NomeDoIngrediente))
                    {
                        Debug.Log("tao iguais mano");
                        return false;
                    }
                    else
                    {
                        ingredientes02 = ingredienteParaAdicionar;
                        return true;
                    }
                }
                else
                {
                    if (ingredientes02.NomeDoIngrediente.Equals(ingredienteParaAdicionar.NomeDoIngrediente  ))
                    {
                        return false;
                    }
                    else
                    {
                        ingredientes01 = ingredienteParaAdicionar;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void DescartaIngrediente()
    {
        inventarioDeReceita.id = 0;

        if (saborPremiumIngrediente != null)
        {
            Destroy(saborPremiumIngrediente.gameObject);
            saborPremiumIngrediente = null;
        }
        if (ingredientes01 != null)
        {
            Destroy(ingredientes01.gameObject);
            ingredientes01 = null;
            inventarioDeReceita.ingrediente1Achado = false;
        }
        if (ingredientes02 != null)
        {
            Destroy(ingredientes02.gameObject);
            ingredientes02 = null;
            inventarioDeReceita.ingrediente2Achado = false;
        }
        
        premiumIngredientes = IngredientePremium.SemIngredientePremium;
        receitaAtual = null;
    }
    private void ObjectStart()
    {
        _originalPosition = transform.position;
    }
    private bool PodeVoltarAPosiçãoInicial()
    {
        return true;
        //transform.position = _originalPosition;
    }
    private bool JaChegouNoDestino()
    {
        return false;
    }
    private void MouseDragUpdate()
    {
        podeVoltar = JaChegouNoDestino();
        transform.position = GetMousePos();
    }
    Vector3 GetMousePos()
    {
        podeVoltar = JaChegouNoDestino();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    private void VolteParaPosicao()
    {
        if (podeVoltar)
        {
            _posicaoAtual = transform.position;

            _posicaoAtual = Vector3.Lerp(
                transform.position,
                _originalPosition,
                smoothVelocidade * Time.deltaTime);

            transform.position = _posicaoAtual;

        }
        else if (transform.position == _originalPosition)
        {
            podeVoltar = JaChegouNoDestino();
        }
    }
    private void MouseDropObject()
    {
        if (pratoDoCliente != null && receitaAtual != null && receitaAtual.NomePrato != Gororoba.NomePrato)
        {
            pratoDoCliente.premiumIngrediente = premiumIngredientes;
            pratoDoCliente.pratoServido = receitaAtual;
            //desligar sprite da receita
            //ligar o sprite da bandeja
            DesativarOSprite(receitaAtual.spriteDaReceita);
            podeVoltar = PodeVoltarAPosiçãoInicial();
            DescartaIngrediente();
        }
        else if(lixeira != null)
        {
            DescartaIngrediente();
            DesativarOSprite(receitaAtual.spriteDaReceita);
            podeVoltar = PodeVoltarAPosiçãoInicial();
        }
        
        podeVoltar = PodeVoltarAPosiçãoInicial();
    }

    void DesativarOSprite(Sprite spriteFrigideira)
    {
        spriteNaFrigideira.sprite = spriteFrigideira;
        spriteNaFrigideira.gameObject.SetActive(false);

    }

}
