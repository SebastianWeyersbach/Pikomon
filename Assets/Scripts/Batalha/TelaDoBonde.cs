using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaDoBonde : MonoBehaviour
{
    [SerializeField] Text MensagemDeTexto;
    [SerializeField] UI_DoBonde[] EspacoMembros;
    List<Pikomon> pikomons;

    public void Inicializacao()
    {
        EspacoMembros = GetComponentsInChildren<UI_DoBonde>();
    }

    public void InfoBonde(List<Pikomon> pikomons)
    {
        this.pikomons = pikomons;
        for (int i = 0; i < EspacoMembros.Length; i++)
        {
            if(i < pikomons.Count)
            {
                EspacoMembros[i].SetData(pikomons[i]);
            }
            else
            {
                EspacoMembros[i].gameObject.SetActive(false);
            }
        }
        MensagemDeTexto.text = "Escolha um Pikomon";
    }

    public void AtualizarBonde(int membroSelecionado)
    {
        for(int i = 0; i < pikomons.Count; i++)
        {
            if(i == membroSelecionado)
            {
                EspacoMembros[i].SetarSelecionado(true);
            }
            else
            {
                EspacoMembros[i].SetarSelecionado(false);
            }
        }
    }

    public void MudarMensagemDeTexto(string mensagem)
    {
        MensagemDeTexto.text = mensagem;
    }

}
