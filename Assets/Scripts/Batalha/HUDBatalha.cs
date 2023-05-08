using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBatalha : MonoBehaviour
{
    [SerializeField] Text nomeTexto;
    [SerializeField] Text nivelTexto;
    [SerializeField] HpBar barraVida;
    [SerializeField] Text TextoHP;
    [SerializeField] int HpAnterior;

    Pikomon _pikomon;

    public void SetData(Pikomon pikomon)
    {
        _pikomon = pikomon;
        nomeTexto.text = pikomon.Base.Nome;
        nivelTexto.text = "Nv" + pikomon.nivel;
        if(pikomon.HP <= 9)
        {
            TextoHP.text = $"00{pikomon.HP}";
        }
        else if(pikomon.HP <= 99)
        {
            TextoHP.text = $"0{pikomon.HP}";
        }
        else
        {
            TextoHP.text = pikomon.HP.ToString();
        }
        barraVida.DefinirVida((float)pikomon.HP / pikomon.MaxHP);
       
    }

    public IEnumerator AtualizarHP()
    {
        StartCoroutine(ContadorDeHPparte2());
        yield return barraVida.SuavizacaoDeHP((float)_pikomon.HP / _pikomon.MaxHP);
                                                                                                                    
    }

    public void ContadorDeHPparte1()
    {
        int Hp_Anterior = _pikomon.HP;
        HpAnterior = Hp_Anterior;
    }
    public IEnumerator ContadorDeHPparte2()
    {
        int HPAtual = _pikomon.HP;
        while(HpAnterior > HPAtual)
        {
            HpAnterior -= 1;
            if (HpAnterior <= 9)
            {
                TextoHP.text = $"00{HpAnterior}";
            }
            else if(HpAnterior <= 99)
            {
                TextoHP.text = $"0{HpAnterior}";
            }
            else
            {
                TextoHP.text = HpAnterior.ToString();
            }
            yield return new WaitForSeconds(0.125f);
        }
        HpAnterior = _pikomon.HP;
        TextoHP.text = _pikomon.HP.ToString();

        if (_pikomon.HP <= 9)
        {
            TextoHP.text = $"00{_pikomon.HP}";
        }
        else if (_pikomon.HP <= 99)
        {
            TextoHP.text = $"0{_pikomon.HP}";
        }
        else
        {
            TextoHP.text = _pikomon.HP.ToString();
        }
    }
}

