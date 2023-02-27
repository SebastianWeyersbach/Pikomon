using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    public BaseMovimento Base { get; set; }
    public int PowerPoint { get; set; }

    public Mover(BaseMovimento pBase)
    {
        Base = pBase;
        PowerPoint = pBase.microsoft_apresentacoes;
    }
}
