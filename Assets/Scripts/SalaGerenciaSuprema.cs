using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoJogo { Livre, Batalha};

public class SalaGerenciaSuprema : MonoBehaviour
{
    [SerializeField] Joojador ControleJogador;
    [SerializeField] SistemaDeBatalha sistemaBatalha;
    [SerializeField] Camera CameraMundo;

    EstadoJogo estado;

    #region voids

    void Start()
    {

        estado = EstadoJogo.Livre;
        ControleJogador.NoEncontro += ComecarBatalha;
        sistemaBatalha.FimBatalha += TerminarBatalha;
        
    }

    void Update()
    {
        if(estado == EstadoJogo.Livre)
        {
            ControleJogador.UpdateManual();
        }
        else if(estado == EstadoJogo.Batalha)
        {
            sistemaBatalha.UpdateManual();
        }
    }

    void ComecarBatalha()
    {
        estado = EstadoJogo.Batalha;
        ControleJogador.gameObject.SetActive(false);
        sistemaBatalha.gameObject.SetActive(true);
        CameraMundo.gameObject.SetActive(false);

        var bondePlayer = ControleJogador.GetComponent<Bonde>();
        var PikomonsSelvagens = FindObjectOfType<PikomonsLocais>().GetComponent<PikomonsLocais>().EscolherPikomonSelvagemAleatorio();

        sistemaBatalha.ComecarBatalha(bondePlayer, PikomonsSelvagens);
    }

    void TerminarBatalha(bool won)
    {
        estado = EstadoJogo.Livre;
        sistemaBatalha.gameObject.SetActive(false);
        ControleJogador.gameObject.SetActive(true);
        CameraMundo.gameObject.SetActive(true);
    }

    #endregion
}
