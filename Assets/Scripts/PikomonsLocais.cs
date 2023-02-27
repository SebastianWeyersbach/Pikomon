using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikomonsLocais : MonoBehaviour
{
    [SerializeField] List<Pikomon> PikomonsSelvagens;

    public Pikomon EscolherPikomonSelvagemAleatorio()
    {
        var PikomonSelvagem = PikomonsSelvagens[Random.Range(0, PikomonsSelvagens.Count)];
        PikomonSelvagem.Init();
        return PikomonSelvagem;
    }


}
