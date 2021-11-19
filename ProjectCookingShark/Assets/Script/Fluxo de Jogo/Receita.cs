using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Receita : MonoBehaviour
{
    [SerializeField] string nomePrato;
    public string NomePrato => nomePrato;

    [SerializeField] public Ingrediente ingredientes01;
    //public Ingrediente ingredientes01 => ingredientes01;

    [SerializeField] public Ingrediente ingredientes02;
    //public Ingrediente ingredientes02 => ingredientes02;

    [SerializeField] public Sprite spriteDaReceita;

    [SerializeField] public Sprite spriteDaReceitaNaBandeja;


    private void Update()
    {
        TirarIngredienteDuplicado();
    }

    private void TirarIngredienteDuplicado()
    {
        if (ingredientes01 != null)
        {
            if (ingredientes01.Equals(ingredientes02))
            {
                ingredientes02 = null;
            }
        }
    }

}
