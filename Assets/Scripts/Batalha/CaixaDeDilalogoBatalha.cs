using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixaDeDilalogoBatalha : MonoBehaviour
{
    [Header("Atributos de diálogo de batalha")]
    [SerializeField] float CPS;

    [SerializeField] Text textoDilalogo;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    [SerializeField] GameObject SeletorDeAcao;
    [SerializeField] GameObject SeletorDeEscolha;
    [SerializeField] GameObject DetalhesEscolha;

    [SerializeField] Color HighlightedColor;
    [SerializeField] Color ActualColor;

    [SerializeField] List<Text> Textosdeacao;
    [SerializeField] List<Text> Textosdeescolha;

    public void SetarDilalogo(string dilalogo)
    {
        textoDilalogo.text = dilalogo;
    }

    public IEnumerator EscritaDilalogo(string dilalogo)
    {
        textoDilalogo.text = "";
        foreach (var letra in dilalogo.ToCharArray())
        {
            textoDilalogo.text += letra;
            yield return new WaitForSeconds(CPS);
        }
    }

    public void LigarSelecaoDeAcao(bool ligado)
    {
        SeletorDeAcao.SetActive(ligado);
    }

    public void LigarTextoDeDilalogo(bool ligado)
    {
        textoDilalogo.enabled = ligado;
    }

    public void LigarSeletorDeEscolha(bool ligado)
    {
        SeletorDeEscolha.SetActive(ligado);
        DetalhesEscolha.SetActive(ligado);
    }

    public void AtualizarSeletorDeAcao(int AcaoSelecionada)
    {
        for (int i = 0; i < Textosdeacao.Count; i++)
        {
            if (i== AcaoSelecionada)
            {
                Textosdeacao[i].color = HighlightedColor;
            }
            else
            {
                Textosdeacao[i].color = ActualColor;
            }
        }
    }

    public void AtualizarSeletorDeEscolha(int EscolhaSelecionada, Mover mover)
    {
        for (int i = 0; i < Textosdeescolha.Count; i++)
        {
            if(i == EscolhaSelecionada)
            {
                Textosdeescolha[i].color = HighlightedColor;
            }
            else
            {
                Textosdeescolha[i].color = ActualColor;
            }

            ppText.text = $"PP {mover.PowerPoint}/{mover.Base.microsoft_apresentacoes}";
            typeText.text = mover.Base.Tipo.ToString();
        }
    }

    public void SetarNomesdeEscolha(List<Mover> Escolhas)
    {
        for(int i = 0; i < Textosdeescolha.Count; i++)
        {
            if(i < Escolhas.Count)
            {
                Textosdeescolha[i].text = Escolhas[i].Base.Nome;
            }
            else
            {
                Textosdeescolha[i].text = "-";
            }
        }
    }

}
