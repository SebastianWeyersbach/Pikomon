using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoJogo { Livre, Batalha, Dialogo};

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

        GerenteDlalogadorFinal.Instancia.DialogoEntrando += () =>
        {
            estado = EstadoJogo.Dialogo;
        };
        /*
          Nesse caso precisa de ";" porque no LAMBDA você já está criando e chamando a função ao mesmo tempo. 
          Quando você simplesmente cria a função (tipo esse start ou o update) eles estão simplesmente ali, ele não está necessariamente sendo chamado nesse script. 
          O que o LAMBDA faz seria criar, chamar e apagar essa função em uma só, e por isso precisa do ; , afinal o ; serve justamente para quando se chama uma função.
        
        */    
    GerenteDlalogadorFinal.Instancia.DialogoSaindo += () =>
        {
            if(estado == EstadoJogo.Dialogo)
            {
                estado = EstadoJogo.Livre;
            }
        };

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
