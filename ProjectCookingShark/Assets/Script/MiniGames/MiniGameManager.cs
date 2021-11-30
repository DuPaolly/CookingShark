using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    static MiniGameManager instancia;
    [SerializeField] RaladorMiniGame miniGameRalador;
    [SerializeField] MiniGameFaca miniGameTabua;
    public enum TipoMiniGame{

        Tabua,
        Ralador,
        Pilão
    }

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        instancia = null;
    }

    public static void IniciaMiniGame(TipoMiniGame miniGameParaIniciar, Ingrediente ingredienteParaProduzir)
    {
        Debug.Log($"MiniGame {miniGameParaIniciar}");

        switch (miniGameParaIniciar)
        {
            case TipoMiniGame.Tabua:
                instancia.miniGameTabua.InicializaMiniGame(5, ingredienteParaProduzir);

                //instancia.miniGameRalador.SetActive(false);
                break;
            case TipoMiniGame.Ralador:
                instancia.miniGameRalador.InicializaMiniGame(100, ingredienteParaProduzir);

               // instancia.miniGameTabua.InicializaMiniGame(false);
                break;
            case TipoMiniGame.Pilão:
                //instancia.miniGameRalador.SetActive(false);
                //instancia.miniGameTabua.InicializaMiniGame(false);
                break;
            default:
                break;
        }
    }
}
