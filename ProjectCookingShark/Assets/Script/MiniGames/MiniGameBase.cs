using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    [SerializeField] GameObject canvasMiniGame;
    protected bool miniGameEmProducao;
    public bool MiniGameEmProdução => miniGameEmProducao;
    [SerializeField] public SpriteRenderer ingredienteNoMiniGame;

    //Sprite ingredienteParaProduzir
    public virtual void InicializaMiniGame(int condicaoDeEncerramento, Ingrediente IngredienteEmProducao)
    {
        ingredienteNoMiniGame.sprite = IngredienteEmProducao.spriteDoIngredienteSolo;
        canvasMiniGame.SetActive(true);
        miniGameEmProducao = true;
    }

    public virtual void EncerraMiniGame()
    {

        canvasMiniGame.SetActive(false);
        miniGameEmProducao = false;
    }

    void ColocarIngredienteNoMiniGame(Sprite spriteIngredienteMiniGame)
    {

    }

}
