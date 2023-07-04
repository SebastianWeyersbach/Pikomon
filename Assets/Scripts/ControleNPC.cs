using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleNPC : MonoBehaviour, Interagiveis
{
   [SerializeField] DilalogoFinal dilalogo;
   public void interagir()
    {
        Debug.Log("Interagindo com um NPC");
    }
}
