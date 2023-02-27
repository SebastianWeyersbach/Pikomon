using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pikomon
{

    [SerializeField] BasePikomon _base;
    [SerializeField] int level;

    public BasePikomon Base { get { return _base; } }
    public int nivel { get { return level; } }
    public List<Mover> Movimentos { get; set; }
    public int HP { get; set; }

    public void Init()
    {
        HP = MaxHP;

        //Movimentos
        Movimentos = new List<Mover>();
        foreach (var movimento in Base.AtaquesEnsinaveis)
        {
            if (movimento.Nivel <= nivel)
            {
                Movimentos.Add(new Mover(movimento.BaseMovimento));
            }
            if(Movimentos.Count >= 4)
            {
                break;
            }
        }
    }

    #region retornos
    public int Ataque
    {
        get { return Mathf.FloorToInt((Base.Ataque * nivel) / 46f) + 5; }
    }
    public int Defesa
    {
        get { return Mathf.FloorToInt((Base.Defesa * nivel) / 46f) + 5; }
    }
    public int ATKespecial
    {
        get { return Mathf.FloorToInt((Base.AtkEspecial * nivel) / 46f) + 5; }
    }
    public int DFespecial
    {
        get { return Mathf.FloorToInt((Base.DfEspecial * nivel) / 46f) + 5; }
    }
    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * nivel) / 46f) + 10; }
    }
    public int Velocidade
    {
        get { return Mathf.FloorToInt((Base.Velocidade * nivel) / 22f) + 5; }
    }
    #endregion

    public DetalhesDoDano TomarDano(Mover mover, Pikomon atacante)
    {
        float critico = 1f;
        if (Random.value * 100f <= 5f)
        {
            critico = 2f;
            Debug.Log("Crítico");
        }

        float type = GraficoDeTipo.SerEfetivo(mover.Base.Tipo, this.Base.Tipo1) * GraficoDeTipo.SerEfetivo(mover.Base.Tipo, this.Base.Tipo2);
        Debug.Log("Deu dano do tipo" + type);
        var detalhesDano = new DetalhesDoDano()
        {
            TipoEfetividade = type, 
            Critico = critico,
            Desmaiado = false
        };
        float ataque = (mover.Base.Especial) ? ATKespecial : Ataque;            //Ternário - Uma forma mais fácil de fazer um "IF" (quando se faz uma bool)
        //                 A condição        Execução caso Verdadeiro   Execução caso falso
        float defesa = (mover.Base.Especial) ? DFespecial : Defesa;

        float modificadores = Random.Range(0.85f, 1f) * type;
        float a = (2 * atacante.nivel * critico)/5f + 2;
        float d = (a * mover.Base.Poder * ((float)Ataque / Defesa))/50f + 2;
        int dano = Mathf.FloorToInt(d * modificadores);

        HP -= dano;

        if(HP <= 0)
        {
            HP = 0;
            detalhesDano.Desmaiado = true;
        }
        return detalhesDano;
    }

    public Mover MovimentoAleatorio()
    {
        int r = Random.Range(0, Movimentos.Count);
        return Movimentos[r];
    }

}

public class DetalhesDoDano
{
    public bool Desmaiado { get; set; }
    public float Critico { get; set; }
    public float TipoEfetividade { get; set; }
}
