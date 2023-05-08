using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EstadoDeBatalha{ Iniciar, EscolhaJogador, AcaoJogador, AcaoInimigo, Ocupado, TelaDoBonde}

//LEMBRAR QUE NO CÓDIGO DO THIAGO O "AÇÃOJOGADOR" É "PLAYER ACTION" E "ESCOLHAJOGADOR" É "PLAYERMOVE"   

public class SistemaDeBatalha : MonoBehaviour
{
    [SerializeField] UnidadeDeBatalha unidadePlayer;
    [SerializeField] UnidadeDeBatalha unidadeInimigo;
    [SerializeField] HUDBatalha HUD_inimigo;
    [SerializeField] HUDBatalha HUD_player;
    [SerializeField] CaixaDeDilalogoBatalha caixaDeDilalogo;
    [SerializeField] TelaDoBonde telaDoBonde;

    public event Action<bool> FimBatalha;

    EstadoDeBatalha estado;
    int acaoAtual;
    int EscolhaAtual;
    int PassageiroAtual;
    int TentativaDeFuga;

    Bonde bondePlayer;
    Pikomon PikomonSelvagem;

    public void ComecarBatalha( Bonde bondePlayer, Pikomon PikomonSelvagem)
    {
        this.bondePlayer = bondePlayer;
        this.PikomonSelvagem = PikomonSelvagem;
            
        StartCoroutine(SetarBatalha());
        
    }       

    public IEnumerator SetarBatalha()
    {
        //Debug.Log("executando2");
        unidadePlayer.Setup(bondePlayer.BichosSaudaveis());
        unidadeInimigo.Setup(PikomonSelvagem);
        HUD_player.SetData(unidadePlayer.Pikomon);
        HUD_inimigo.SetData(unidadeInimigo.Pikomon);

        telaDoBonde.Inicializacao();

        caixaDeDilalogo.SetarNomesdeEscolha(unidadePlayer.Pikomon.Movimentos);
    
        yield return caixaDeDilalogo.EscritaDilalogo($"Um Pikomon {unidadeInimigo.Pikomon.Base.Nome} selvagem apareceu!");
        yield return new WaitForSeconds(1f);
        yield return caixaDeDilalogo.EscritaDilalogo("Escolha uma ação");
        yield return new WaitForSeconds(0.5f);
        PlayerAction();
    }

    void PlayerAction()
    {
        estado = EstadoDeBatalha.AcaoJogador;
        caixaDeDilalogo.LigarSelecaoDeAcao(true);
    }

    void PlayerMove()
    {
        estado = EstadoDeBatalha.EscolhaJogador;
        caixaDeDilalogo.LigarSelecaoDeAcao(false);
        caixaDeDilalogo.LigarTextoDeDilalogo(false);
        caixaDeDilalogo.LigarSeletorDeEscolha(true);
    }

    void TelaDoBonde()
    {
        estado = EstadoDeBatalha.TelaDoBonde;
        telaDoBonde.InfoBonde(bondePlayer.Pikomons);
        telaDoBonde.gameObject.SetActive(true);
    }

    IEnumerator RealizarAEscolha()
    {
        estado = EstadoDeBatalha.Ocupado;
        HUD_inimigo.ContadorDeHPparte1();
        var escolha = unidadePlayer.Pikomon.Movimentos[EscolhaAtual];
        escolha.PowerPoint--;
        yield return caixaDeDilalogo.EscritaDilalogo($"{unidadePlayer.Pikomon.Base.Nome} usou {escolha.Base.Nome}");
        yield return new WaitForSeconds(1f);

        unidadePlayer.MostrarAnimacaoATK();
        yield return new WaitForSeconds(0.25f);
        unidadeInimigo.MostrarAnimacaoHit();

        var detalhesDoDano = unidadeInimigo.Pikomon.TomarDano(escolha, unidadePlayer.Pikomon);
        yield return HUD_inimigo.AtualizarHP();
        yield return MostrarDetalhesDoDano(detalhesDoDano);
        yield return new WaitForSeconds(1f);
        if (detalhesDoDano.Desmaiado)
        {
            yield return caixaDeDilalogo.EscritaDilalogo($"{unidadeInimigo.Pikomon.Base.Nome} desmaiou");
            unidadeInimigo.MostrarAnimacaoMorte();

            yield return new WaitForSeconds(1.5f);
            FimBatalha(true);
         
        }
        else
        {
            StartCoroutine(RealizarAEscolhaInimiga());
        }
    }

    IEnumerator RealizarAEscolhaInimiga()
    {
        estado = EstadoDeBatalha.Ocupado;
        var escolha = unidadeInimigo.Pikomon.MovimentoAleatorio();
        escolha.PowerPoint--;
        yield return caixaDeDilalogo.EscritaDilalogo($"{unidadeInimigo.Pikomon.Base.Nome} usou {escolha.Base.Nome}");
        yield return new WaitForSeconds(1f);

        unidadeInimigo.MostrarAnimacaoATK();
        yield return new WaitForSeconds(0.25f);
        unidadePlayer.MostrarAnimacaoHit();

        var detalhesDoDano = unidadePlayer.Pikomon.TomarDano(escolha, unidadeInimigo.Pikomon);
        yield return HUD_player.AtualizarHP();
        yield return MostrarDetalhesDoDano(detalhesDoDano);
        yield return new WaitForSeconds(1f);
        if (detalhesDoDano.Desmaiado)
        {
            yield return caixaDeDilalogo.EscritaDilalogo($"{unidadePlayer.Pikomon.Base.Nome} desmaiou");
            unidadePlayer.MostrarAnimacaoMorte();

            yield return new WaitForSeconds(1.5f);
            var ProximoPikomon = bondePlayer.BichosSaudaveis();
            if (ProximoPikomon != null)
            {
                unidadePlayer.Setup(ProximoPikomon);
                HUD_player.SetData(ProximoPikomon);

                caixaDeDilalogo.SetarNomesdeEscolha(ProximoPikomon.Movimentos);

                yield return caixaDeDilalogo.EscritaDilalogo($"{ProximoPikomon.Base.Nome}, eu escolho você!");
                yield return new WaitForSeconds(1f);
                yield return caixaDeDilalogo.EscritaDilalogo("Escolha uma ação");
                yield return new WaitForSeconds(1f);
                PlayerAction();
            }
            else
            {
                FimBatalha(true);
            }
            
        
        }
        else
        {
            PlayerAction();
            caixaDeDilalogo.LigarTextoDeDilalogo(false);
        }
    }

    IEnumerator MostrarDetalhesDoDano(DetalhesDoDano detalhesDoDano)
    {
        if(detalhesDoDano.Critico == 2f)
        {
            yield return caixaDeDilalogo.EscritaDilalogo("Um acerto crítico!");
        }
        if(detalhesDoDano.TipoEfetividade == 2f)
        {
            yield return caixaDeDilalogo.EscritaDilalogo("É super efetivo!");
        }
        else if(detalhesDoDano.TipoEfetividade == 0.5f)
        {
            yield return caixaDeDilalogo.EscritaDilalogo("Não é muito efetivo");
        }
        
    }

    public void UpdateManual()
    {
        if(estado == EstadoDeBatalha.AcaoJogador)
        {
            SelecionadorDeAcao();
        }
        if (estado == EstadoDeBatalha.EscolhaJogador)
        {
            LigarSeletorDeEscolha();
        }
        if (estado == EstadoDeBatalha.TelaDoBonde)
        {
            LigarInspetorDoBonde();
        }
    }

    void SelecionadorDeAcao()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(acaoAtual == 1)
            {
                --acaoAtual;
            }
            else if(acaoAtual == 0)
            {
                ++acaoAtual;
            }
            else if (acaoAtual == 2)
            {
                ++acaoAtual;
            }
            else if (acaoAtual == 3)
            {
                --acaoAtual;
            }

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (acaoAtual == 1)
            {
                --acaoAtual;
            }
            else if (acaoAtual == 0)
            {
                ++acaoAtual;
            }
            else if (acaoAtual == 2)
            {
                ++acaoAtual;
            }
            else if (acaoAtual == 3)
            {
                --acaoAtual;
            }
          
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(acaoAtual < 2)
            {
                acaoAtual += 2;
            }
            else if (acaoAtual >= 2)
            {
                acaoAtual -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (acaoAtual > 1)
            {
                acaoAtual -= 2;
            }
            else if(acaoAtual < 2)
            {
                acaoAtual += 2;
            }
        }
        caixaDeDilalogo.AtualizarSeletorDeAcao(acaoAtual);
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if(acaoAtual == 0)
            {
                PlayerMove();
            }
            if(acaoAtual == 2)
            {
                TelaDoBonde();
            }
            if(acaoAtual == 1)
            {
                //Mochila
            }
            if(acaoAtual == 3)
            {
                //Correr
            }
        }
    }

    void LigarSeletorDeEscolha()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(EscolhaAtual < unidadePlayer.Pikomon.Movimentos.Count - 1)
            {
                ++EscolhaAtual;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(EscolhaAtual > 0)
            {
                --EscolhaAtual;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(EscolhaAtual < unidadePlayer.Pikomon.Movimentos.Count - 2)
            {
                EscolhaAtual += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(EscolhaAtual > 1)
            {
                EscolhaAtual -= 2;
            }
        }
        caixaDeDilalogo.AtualizarSeletorDeEscolha(EscolhaAtual, unidadePlayer.Pikomon.Movimentos[EscolhaAtual]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            caixaDeDilalogo.LigarSeletorDeEscolha(false);
            caixaDeDilalogo.LigarTextoDeDilalogo(true);
            StartCoroutine(RealizarAEscolha());
        }
    }

    void LigarInspetorDoBonde()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(PassageiroAtual >= 0 && PassageiroAtual < bondePlayer.Pikomons.Count - 1)
            {
                PassageiroAtual++;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(PassageiroAtual > 0)
            {
                PassageiroAtual--;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (PassageiroAtual < 3)
            {
                PassageiroAtual += 3; 
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(PassageiroAtual > 2)
            {
                PassageiroAtual -= 3;
            }
        }

        telaDoBonde.AtualizarBonde(PassageiroAtual);

        if (Input.GetKeyUp(KeyCode.Z))
        {
            var PassageiroEscolhido = bondePlayer.Pikomons[PassageiroAtual];
            if(PassageiroEscolhido.HP <= 0)
            {
                telaDoBonde.MudarMensagemDeTexto($"{PassageiroEscolhido.Base.Nome} está indisponível.");
                return;
            }
            if(PassageiroEscolhido == unidadePlayer.Pikomon)
            {
                telaDoBonde.MudarMensagemDeTexto($"{PassageiroEscolhido.Base.Nome} já está em combate!");
                return;
            }

            telaDoBonde.gameObject.SetActive(false);
            estado = EstadoDeBatalha.Ocupado;
            StartCoroutine(TrocarPikomon(PassageiroEscolhido));

        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
        {
            telaDoBonde.gameObject.SetActive(false);
            PlayerAction();
        }
    }
    
    IEnumerator TrocarPikomon(Pikomon novoPikomon)
    {
        yield return caixaDeDilalogo.EscritaDilalogo($"{unidadePlayer.Pikomon.Base.Nome}, volta!");
        unidadePlayer.MostrarAnimacaoMorte();
        yield return new WaitForSeconds(1.5f);

        unidadePlayer.Setup(novoPikomon);
        HUD_player.SetData(novoPikomon);
        caixaDeDilalogo.SetarNomesdeEscolha(novoPikomon.Movimentos);

        yield return caixaDeDilalogo.EscritaDilalogo($"Vai {novoPikomon.Base.Nome}!");

        StartCoroutine(RealizarAEscolhaInimiga());

    }

    IEnumerator TentativaDeFuga()
    {
        estado = EstadoDeBatalha.Ocupado;
        CaixaDeDilalogo.LigarTextoDeDilalogo(true);
    }
}
