﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DoBonde : MonoBehaviour
{
    [SerializeField] Text textoNome;
    [SerializeField] Text textoNivel;
    [SerializeField] Text textoVida;
    [SerializeField] HpBar hpBar;
    [SerializeField] Image seletor;
    [SerializeField] Image imagemPassageiro;

    [SerializeField] Color CorDestaque;
    [SerializeField] Color CorNormal;

    Pikomon _pikomon;

    public void SetData(Pikomon pikomon)
    {
        _pikomon = pikomon;
        imagemPassageiro.sprite = pikomon.Base.Spritefrente;
        textoNome.text = pikomon.Base.Nome;
        textoNivel.text = $"Nv {pikomon.nivel}";
        textoVida.text = pikomon.HP.ToString();
        if(pikomon.HP <= 9)
        {
            textoVida.text = $"00{pikomon.HP}";
        }
        else if(pikomon.HP <= 99) 
        {
            textoVida.text = $"0{pikomon.HP}";
        }
        else
        {
            textoVida.text = pikomon.HP.ToString();
        }
        hpBar.DefinirVida((float)pikomon.HP / pikomon.MaxHP);
    }

    public void SetarSelecionado(bool selecionado)
    {
        if (selecionado)
        {
            seletor.color = CorDestaque;
        }
        else
        { 
            seletor.color = CorNormal;
        }
    }

}
