using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediente : Sabores
{
    private Frigideira pratoEmProducao;

    [SerializeField] string nomeDoIngrediente;
    public string NomeDoIngrediente => nomeDoIngrediente;
    [SerializeField] Sabores.SaboresExistentes sabor;
    public Sabores.SaboresExistentes Sabor => sabor;

    [SerializeField] public MiniGameManager.TipoMiniGame minigame;

    [SerializeField] public Sprite spriteDoIngredienteSolo;
    [SerializeField] public Sprite spriteDoIngredientePronto;

    int smoothVelocidade = 20;

    private bool podeVoltar;

    private Vector3 _originalPosition;

    private bool _dragging;

    private Vector2 offset, _posicaoAtual;

    private void Awake()
    {
        ObjectStart();
    }

    private void OnMouseDrag()
    {
        MouseDragUpdate();
    }
    private void OnMouseDown()
    {
        
    }
    private void FixedUpdate()
    {
        VolteParaPosicao();
    }
    private void OnMouseUp()
    {
        MouseDropObject();
    }

    private void OnTriggerEnter2D(Collider2D areaEmQueEncostou)
    {
        Frigideira pratoEncontrado = areaEmQueEncostou.GetComponent<Frigideira>();

        if(pratoEncontrado != null)
        {
            pratoEmProducao = pratoEncontrado;
        }
    }

    private void OnTriggerExit2D(Collider2D areaEmQueSaiu)
    {
        Frigideira pratoQuePerdeu = areaEmQueSaiu.GetComponent<Frigideira>();

        if(pratoQuePerdeu != null)
        {
            pratoEmProducao = null;
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    private void MouseDropObject()
    {
        if(pratoEmProducao != null)
        {
            if (!pratoEmProducao.PodeReceberIngrediente(this))
            {
                podeVoltar = PodeVoltarAPosiçãoInicial();
            }
            else
            {
                AdicionarOIngredienteAoPrato();
                MiniGameManager.IniciaMiniGame(minigame, this);
                //Start Minigame
                podeVoltar = JaChegouNoDestino();
            }
        }
        else
        {
           podeVoltar = PodeVoltarAPosiçãoInicial();
        }
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
        else if(transform.position == _originalPosition)
        {
            podeVoltar = JaChegouNoDestino();
        }
        
    }

    private bool PodeVoltarAPosiçãoInicial()
    {
        return true;
    }

    private bool JaChegouNoDestino()
    {
        return false;
    }

    private void MouseDragUpdate()
    {
        //sprite aki
        //enquanto segura
        //TrocarOSpriteDoPratoNoDrag(spriteDoIngredienteSolo);
        podeVoltar = JaChegouNoDestino();
        transform.position = GetMousePos();
    }

    private void ObjectStart()
    {
        _originalPosition = transform.position;
    }

    void AdicionarOIngredienteAoPrato()
    {
        transform.position = pratoEmProducao.transform.position;
        transform.SetParent(pratoEmProducao.transform);
    }

    //void TrocarOSpriteDoPratoNoDrag(Sprite spriteDoIngredienteSolo)
    //{
    //    //spriteDoIngredienteSoloPraFuncao.gameObject.SetActive(true);
    //    //spriteDoIngredienteSoloPraFuncao.sprite = spriteDoIngredienteSolo;
    //}

}
