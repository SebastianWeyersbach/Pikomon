using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DilalogoFinal
{
    [SerializeField] string nome;
    [SerializeField][TextArea(10, 10)] List<string> falas;

    public string Nome{ get{ return nome;} }
    public List<string> Falas { get{ return falas;} }
}
