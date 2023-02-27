using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotzinhoNaoEspecial : MonoBehaviour
{
    [SerializeField] Transform[] caminhos;
    [SerializeField] float Espera;
    public Animator animDilalogo;
    [SerializeField] Dilalogo Papo;
    
    void Start()
    {
        StartCoroutine(CorrotinadeMovimento());
        animDilalogo = GameObject.FindGameObjectWithTag("GUIlherme/Dilalogo").GetComponent<Animator>();
    }

    #region CoRotinas
    //Corrotina é a mesma coisa que a função, mas ela retorna um tempo de espera ou espera uma ação acabar
    IEnumerator CorrotinadeMovimento()
    {
        for(int i= 0; i <=caminhos.Length; i++)
        {
            Debug.Log(i);
            yield return StartCoroutine(AndaBot(i));
            if(i >= caminhos.Length -1)
            {
                i = -1;
            }
        }
    }

    IEnumerator AndaBot(int daWaenumber)
    {
        GetComponent<NavMeshAgent>().destination = caminhos[daWaenumber].position;
        yield return new WaitForSeconds(Espera);
    }

    #endregion

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButtonDown("Fire3") && FindObjectOfType<GerenteDilalogador>().TaFalando == false)
        {
            animDilalogo.SetTrigger("DilalogoVisivel");
            animDilalogo.ResetTrigger("DilalogoInvisivel");
            FindObjectOfType<GerenteDilalogador>().AcordaFi(Papo);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            animDilalogo.SetTrigger("DilalogoInvisivel");
            animDilalogo.ResetTrigger("DilalogoVisivel");
            FindObjectOfType<GerenteDilalogador>().TaFalando = false;
        }
    }

}
