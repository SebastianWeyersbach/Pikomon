using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pikomon", menuName = "Pikomon/Criar novo Pikomon")]
public class BasePikomon : ScriptableObject
{
    #region CamposSerializados
    [SerializeField] string nome;
    [TextArea] [SerializeField] string descricao;
    [SerializeField] Sprite spriteFrente;
    [SerializeField] Sprite spriteAtras;
    [SerializeField] Animator AnimacaoGlobal;

    //Status Base
    [SerializeField] int maxHp;
    [SerializeField] int ataque;
    [SerializeField] int defesa;
    [SerializeField] int ATKespecial;
    [SerializeField] int DFespecial;
    [SerializeField] int velocidade;
    [SerializeField] TipoPokemon tipo1;
    [SerializeField] TipoPokemon tipo2;

    [SerializeField] List<AtaqueEnsinavel> Ataques_ensinaveis;

    [System.Serializable]
    public class AtaqueEnsinavel
    {
        [SerializeField] BaseMovimento baseMovimento;
        [SerializeField] int nivel;

        public BaseMovimento BaseMovimento
        {
            get { return baseMovimento; }
        }
        public int Nivel
        {
            get { return nivel; }
        }
    }
    #endregion

    #region retornos
    public string Nome
    {
        get { return nome; }
    }
    public string Descricao
    {
        get { return descricao; }
    }
    public Sprite Spritefrente
    {
        get { return spriteFrente; }
    }
    public Sprite Spriteatras
    {
        get { return spriteAtras; }
    }
    public Animator animacaoglobal
    {
        get { return AnimacaoGlobal; }
    }
    public int MaxHP
    {
        get { return maxHp; }
    }
    public int Ataque
    {
        get { return ataque; }
    }
    public int Defesa
    {
        get { return defesa; }
    }
    public int AtkEspecial
    {
        get { return ATKespecial; }
    }
    public int DfEspecial
    {
        get { return DFespecial;  }
    }
    public int Velocidade
    {
        get { return velocidade; }
    }
    public TipoPokemon Tipo1
    {
        get { return tipo1; }
    }
    public TipoPokemon Tipo2
    {
        get { return tipo2; }
    }
    public List<AtaqueEnsinavel> AtaquesEnsinaveis
    {
        get { return Ataques_ensinaveis; }
    }
    #endregion
}

public enum TipoPokemon
{
    Nenhum,
    Normal,
    Fogo,
    Agua,
    Eletrico,
    Grama,
    Gelo,
    Lutador,
    Veneno,
    Chao,
    Voador,
    Psiquico,
    Inseto,
    Pedra,
    Fantasma,
    Dragao
}
public class GraficoDeTipo
{
    static float[][] tabela = 
    {                    //    NORM   FOGO   AGUA   ELET   GRAM   GELO   LUTA   VENE   CHAO   VOAD   PSIC   INSE   PEDR   FANT   DRAG
        /*NORM*/ new float[] { 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 0.5f , 0f   , 1f  },
        /*FOGO*/ new float[] { 1f   , 0.5f , 0.5f , 1f   , 2f   , 2f   , 1f   , 1f   , 1f   , 1f   , 1f   , 2f   , 0.5f , 1f   , 0.5f},
        /*AGUA*/ new float[] { 1f   , 2f   , 0.5f , 1f   , 0.5f , 1f   , 1f   , 1f   , 2f   , 1f   , 1f   , 1f   , 2f   , 1f   , 0.5f},
        /*ELET*/ new float[] { 1f   , 1f   , 2f   , 0.5f , 0.5f , 1f   , 1f   , 1f   , 0f   , 2f   , 1f   , 1f   , 1f   , 1f   , 0.5f},
        /*GRAM*/ new float[] { 1f   , 0.5f , 2f   , 1f   , 0.5f , 1f   , 1f   , 0.5f , 2f   , 0.5f , 1f   , 0.5f , 2f   , 1f   , 0.5f},
        /*GELO*/ new float[] { 1f   , 1f   , 0.5f , 1f   , 2f   , 0.5f , 1f   , 1f   , 2f   , 2f   , 1f   , 1f   , 1f   , 1f   , 2f  },
        /*LUTA*/ new float[] { 2f   , 1f   , 1f   , 1f   , 1f   , 2f   , 1f   , 0.5f , 1f   , 0.5f , 0.5f , 0.5f , 2f   , 0f   , 1f  },
        /*VENE*/ new float[] { 1f   , 1f   , 1f   , 1f   , 2f   , 1f   , 1f   , 0.5f , 0.5f , 1f   , 1f   , 2f   , 0.5f , 0.5f , 1f  },
        /*CHAO*/ new float[] { 1f   , 2f   , 1f   , 2f   , 0.5f , 1f   , 1f   , 2f   , 1f   , 0f   , 1f   , 0.5f , 2f   , 1f   , 1f  },
        /*VOAD*/ new float[] { 1f   , 1f   , 1f   , 0.5f , 2f   , 1f   , 2f   , 1f   , 1f   , 1f   , 1f   , 2f   , 0.5f , 1f   , 1f  },
        /*PSIC*/ new float[] { 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 2f   , 2f   , 1f   , 1f   , 0.5f , 1f   , 1f   , 1f   , 1f  },
        /*INSE*/ new float[] { 1f   , 0.5f , 1f   , 1f   , 2f   , 1f   , 0.5f , 2f   , 1f   , 0.5f , 2f   , 1f   , 1f   , 0.5f , 1f  },
        /*PEDR*/ new float[] { 1f   , 2f   , 1f   , 1f   , 1f   , 2f   , 0.5f , 1f   , 0.5f , 2f   , 1f   , 2f   , 1f   , 1f   , 1f  },
        /*FANT*/ new float[] { 0f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 0f   , 1f   , 1f   , 1f   , 2f   , 1f  },
        /*DRAG*/ new float[] { 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 1f   , 2f  },
    };

    public static float SerEfetivo(TipoPokemon TipoATK, TipoPokemon TipoDFS)
    {
        if(TipoATK == TipoPokemon.Nenhum || TipoDFS == TipoPokemon.Nenhum)
        {
            return 1;
        }

        int linha = (int)TipoATK - 1;
        int coluna = (int)TipoDFS - 1;

        return tabela[linha][coluna];

    }

}