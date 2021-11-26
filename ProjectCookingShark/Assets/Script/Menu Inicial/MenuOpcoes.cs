using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpcoes : MonoBehaviour
{
    [SerializeField] GameObject menuOpcoes;
    [SerializeField] AudioMixer mixerVolumeGeral;
    [SerializeField] public Dropdown dropdownResolucoes;
    Resolution[] resolucoes;

    private void Start()
    {
        resolucoes = Screen.resolutions;
        dropdownResolucoes.ClearOptions();
        int resolucaoPadraoIndex = 0;

        List<string> opcoes = new List<string>();

        for (int i = 0; i < resolucoes.Length; i++)
        {
            string opcao = resolucoes[i].width + " x " + resolucoes[i].height;
            opcoes.Add(opcao);

            if (resolucoes[i].width == Screen.currentResolution.width && resolucoes[i].height == Screen.currentResolution.height)
            {
                resolucaoPadraoIndex = i;
            }
        }

        dropdownResolucoes.AddOptions(opcoes);
        dropdownResolucoes.value = resolucaoPadraoIndex;
        dropdownResolucoes.RefreshShownValue();
    }

    public void FecharMenuOpcoes()
    {
        menuOpcoes.SetActive(false);
    }

    public void QueroTelaCheia(bool queroTelaCheia)
    {
        Screen.fullScreen = queroTelaCheia;
        Debug.Log("OLHA A TELA CHEIAAA");
    }

    public void VolumeGeral(float volumeGeral)
    {

    }

    public void SetResolucao(int indexResolucao)
    {
        Resolution resolution = resolucoes[indexResolucao];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
