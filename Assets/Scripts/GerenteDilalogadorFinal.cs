using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GerenteDlalogadorFinal : MonoBehaviour
{
    [SerializeField] GameObject _caixadedilalogo;
    [SerializeField] Text textoDilalogo;
    [SerializeField] Text NomeObj;
    [SerializeField] int LetrasPorSegundo;
    public Animator animFala;

    public event Action DialogoEntrando;
    public event Action DialogoSaindo;

    int linhaAtual = 0;
    DilalogoFinal dialogo;
    bool Digitando;

    public static GerenteDlalogadorFinal Instancia { get; private set;}

    private void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        animFala = GameObject.FindGameObjectWithTag("GUIlherme/Dilalogo").GetComponent<Animator>();
    }

}
