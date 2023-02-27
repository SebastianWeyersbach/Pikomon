using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, InterfaceAtivavelJogador
{
    public void EmAtivacaoDoJogador(Joojador jogador)
    {
        Debug.Log("Entrou no Portal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Entrou no Portal");
        }
    }

}
