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

    Pikomon _pikomon;

    public void SetData(Pikomon pikomon)
    {
        _pikomon = pikomon;
        textoNome.text = pikomon.Base.Nome;
        textoNivel.text = $"Nv {pikomon.nivel}";
        textoVida.text = pikomon.HP.ToString();
        hpBar.DefinirVida((float)pikomon.HP / pikomon.MaxHP);
    }
}
