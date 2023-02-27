using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camadas : MonoBehaviour
{

    [SerializeField] LayerMask CamadaObjSolidos;
    [SerializeField] LayerMask CamadaInteracao;
    [SerializeField] LayerMask CamadaGramaBatalha;
    [SerializeField] LayerMask CamadaJogador;
    [SerializeField] LayerMask CamadaPortal;

    public LayerMask CamadaSolidos { get => CamadaObjSolidos; }
    public LayerMask CamadaInteragivel { get => CamadaInteracao; }
    public LayerMask CamadaGrama { get => CamadaGramaBatalha; }
    public LayerMask Camadajogador { get => CamadaJogador; }
    public LayerMask Camadaportal { get => CamadaPortal; }
    public LayerMask CamadasGatilhos { get => CamadaGramaBatalha | CamadaPortal; }

    public static Camadas i { get; set; }

    private void Awake()
    {
        i = this;
    }
}
