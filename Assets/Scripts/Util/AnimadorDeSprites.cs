using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimadorDeSprites
{
    SpriteRenderer renderizadorDeSprites;
    List<Sprite> frames;
    float TaxaDeQuadros;

    int FrameAtual;
    float timer;
    public AnimadorDeSprites(List<Sprite> frames, SpriteRenderer renderizadorDeSprites, float TaxaDeQuadros=0.30f)
    {
        this.frames = frames;
        this.renderizadorDeSprites = renderizadorDeSprites;
        this.TaxaDeQuadros = TaxaDeQuadros;
    }

    public void Start()
    {
        FrameAtual = 0;
        timer = 0f;
        renderizadorDeSprites.sprite = frames[0];
    }

    public void UpdateManual()
    {
        timer += Time.deltaTime;
        if (timer > TaxaDeQuadros)
        {
            FrameAtual = (FrameAtual + 1) % frames.Count;
            renderizadorDeSprites.sprite = frames[FrameAtual];
            //timer -= TaxaDeQuadros;
        }
    }

}
