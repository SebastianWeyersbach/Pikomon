using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenaDoEscrivao : MonoBehaviour
{
    Text iuTexto;
    string TextoPrevisto;
    int IndexPersonagem;
    public float PalavrasPorS;
    float timer;

    public void MaisLetras(Text iuTexto, string TextoPrevisto)
    {
        this.iuTexto = iuTexto;
        this.TextoPrevisto = TextoPrevisto;
        IndexPersonagem = 0;
    }

    void Update()
    {
        if(iuTexto != null)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //mostra o próximo caractere
                timer += PalavrasPorS;
                IndexPersonagem++;
                iuTexto.text = TextoPrevisto.Substring(0, IndexPersonagem);
            }
        }
    }
}
