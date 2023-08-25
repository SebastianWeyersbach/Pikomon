using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControleNPC : MonoBehaviour, Interagiveis
{
    [SerializeField] DilalogoFinal dilalogo;
    [SerializeField] List<Sprite> sprites;

    AnimadorDeSprites animadorDeSprites;

    private void Start()
    {
        animadorDeSprites = new AnimadorDeSprites(sprites,GetComponent<SpriteRenderer>());
        animadorDeSprites.Start();
    }

    public void interagir()
    {
        Debug.Log("Interagindo com um NPC");
    }
}
