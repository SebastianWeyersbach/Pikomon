using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenteDilalogador : MonoBehaviour
{
    public Text denominacaoTexto;
    public Text dilalogoTexto;
    public Queue<string> frases;
    public Animator animDilalogo;
    PenaDoEscrivao penaDoEscrivao;
    public bool TaFalando = false;

    #region Voids
    void Start()
    {
        frases = new Queue<string>();
        animDilalogo = GameObject.FindGameObjectWithTag("GUIlherme/Dilalogo").GetComponent<Animator>();
        dilalogoTexto.text = "";
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && TaFalando == true)
        {
            StartCoroutine(MostrarProximaFrase());
        }
    }

    public void AcordaFi(Dilalogo Conversinha)
    {
        StartCoroutine(ComecarDilalogo(Conversinha));
    }
    #endregion

    #region Corrotinas
    IEnumerator ComecarDilalogo(Dilalogo Conversinha)
    {
        denominacaoTexto.text = Conversinha.nome;
        frases.Clear();
        foreach (string Frase in Conversinha.sentencas)
        {
            frases.Enqueue(Frase);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(MostrarProximaFrase());
    }

    IEnumerator MostrarProximaFrase()
    {
        if(frases.Count == 0)
        {
            StartCoroutine(CessarDilalogo());
            yield return null;
        }
        string frase2 = frases.Dequeue();
        //dilalogoTexto.text = frase2;
        FindObjectOfType<PenaDoEscrivao>().MaisLetras(dilalogoTexto, frase2);
        TaFalando = true;
    }

    IEnumerator CessarDilalogo()
    {
        animDilalogo.SetTrigger("DilalogoInvisivel");
        animDilalogo.ResetTrigger("DilalogoVisivel");
        TaFalando = false;
        yield return null;
    }
    #endregion
}
