using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnidadeDeBatalha : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;

    public Pikomon Pikomon { get; set; }

    Image imagem;
    Vector3 posicaoOG;
    Color CorOG;

    private void Awake()
    {
        imagem = GetComponent<Image>();
        posicaoOG = imagem.transform.localPosition;
        CorOG = imagem.color;
    }

    public void Setup(Pikomon pikomon)
    {
        Pikomon = pikomon;
        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = Pikomon.Base.Spriteatras;
        }
        else
        {
            GetComponent<Image>().sprite = Pikomon.Base.Spritefrente;
        }
        MostrarAnimacaoEntrada();

        imagem.color = CorOG;

    }

    public void MostrarAnimacaoEntrada()
    {
        if (isPlayerUnit)
        {
            imagem.transform.localPosition = new Vector3(-1300f, posicaoOG.y);
        }
        else
        {
            imagem.transform.localPosition = new Vector3(1300f, posicaoOG.y);
        }
        imagem.transform.DOLocalMoveX(posicaoOG.x, 1f);
    }

    public void MostrarAnimacaoATK()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
        {
            sequence.Append(imagem.transform.DOLocalMoveX(posicaoOG.x + 50f, 0.25f));
        }
        else
        {
            sequence.Append(imagem.transform.DOLocalMoveX(posicaoOG.x - 50f, 0.25f));
        }
        sequence.Append(imagem.transform.DOLocalMoveX(posicaoOG.x, 0.25f));
    }

    public void MostrarAnimacaoHit()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(imagem.DOColor(Color.red, 0.1f));
        sequence.Append(imagem.DOColor(CorOG, 0.1f));
    }

    public void MostrarAnimacaoMorte()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(imagem.transform.DOLocalMoveY(posicaoOG.y - 150, 0.5f));
        sequence.Join(imagem.DOFade(0f, 0.5f));
    }
}
