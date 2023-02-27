using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mover", menuName = "Pikomon/Novo ataque")]
public class BaseMovimento : ScriptableObject
{
    #region CamposSerializados
    [SerializeField] string nome;
    [TextArea] [SerializeField] string descricao;
    [SerializeField] TipoPokemon tipo;
    [SerializeField] int poder;
    [SerializeField] int precisao;
    [SerializeField] int MicrosoftApresentacoes;
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
    public TipoPokemon Tipo
    {
        get { return tipo; }
    }
    public int Poder
    {
        get { return poder; }
    }
    public int Precisao
    {
        get { return precisao; }
    }
    public int microsoft_apresentacoes
    {
        get { return MicrosoftApresentacoes; }
    }
    #endregion

    public bool Especial
    {
        get
        {
            if (tipo == TipoPokemon.Fogo ||
                tipo == TipoPokemon.Agua ||
                tipo == TipoPokemon.Grama ||
                tipo == TipoPokemon.Gelo ||
                tipo == TipoPokemon.Eletrico ||
                tipo == TipoPokemon.Dragao ||
                tipo == TipoPokemon.Fantasma)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

}