using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject vida;
    [SerializeField] GameObject vidaAtrasada;
    void Start()
    {
        //vida.transform.localScale = new Vector3(0.5f, 1f);
    }

    //Hp que o bixo vai ter
    public void DefinirVida(float VidaRegulada)
    {
        vida.transform.localScale = new Vector3(VidaRegulada, 1f);
    }

    public IEnumerator SuavizacaoDeHP(float novoHP)
    {
        float HPatual = vida.transform.localScale.x;
        float DiferencaHP = HPatual - novoHP;

        vida.transform.localScale = new Vector3(novoHP, 1f);
        
        while(HPatual - novoHP > Mathf.Epsilon)
        {
            HPatual -= DiferencaHP * Time.deltaTime;
            vidaAtrasada.transform.localScale = new Vector3(HPatual, 1f);
            yield return null;
        }
        vidaAtrasada.transform.localScale = new Vector3(novoHP, 1f);  
    }

}
