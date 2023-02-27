using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Joojador : MonoBehaviour
{
    #region Variáveis
    [SerializeField] CharacterController cc;
    [SerializeField] float Velocidade;
    [SerializeField] Animator anim;
    [SerializeField] Vector3 input;
    [SerializeField] bool emMovimento;
    [SerializeField] float[] HoraDeEncontro = new float[2];
    [SerializeField] float timer;

    public event Action NoEncontro;

    #endregion
    //Chamando as variáveis
    void Start()
    {

        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        timer = UnityEngine.Random.Range(HoraDeEncontro[0], HoraDeEncontro[1]);
    }

    
    public void UpdateManual()
    {
        Movimento();
        animacao();
        
    }

    #region Funções

    #region Movimento
    //Fazendo o jogador se mover
    void Movimento()
    {
        cc.SimpleMove(Physics.gravity);
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButton("Vertical"))
        {
            input.z = Input.GetAxisRaw("Vertical");
            input.x = 0;
        } 
         else if (Input.GetButton("Horizontal"))
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.z = 0;
        }
        else
        {
            input.x = 0;
            input.z = 0;
        }
        Vector3 movimento = new Vector3(input.x * -Velocidade, input.y, (input.z * -Velocidade) *2);
        //Vector3 movimento = new Vector3(Input.GetAxisRaw("Horizontal") * -Velocidade, 0, (Input.GetAxisRaw("Vertical") * -Velocidade)*2);
        cc.Move(movimento * Time.deltaTime);
    }
    #endregion

    #region Animação
    //Sinc movimento-animação
    void animacao()
    {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        }
        //Condições para se mover ou estar parado
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("IsMoving", false);
            emMovimento = false;
        }
        else
        {
            anim.SetBool("IsMoving", true);
            emMovimento = true;
        }
    }

    #endregion

    void PassarPorCima()
    {
        var colisores = Physics.OverlapSphere(transform.position, 0.5f, Camadas.i.CamadasGatilhos);

    }

    #endregion  

    #region encontros
    private void OnTriggerStay(Collider other)
    {
        #region if
        if(other.gameObject.tag == "Terreno/Grama" && emMovimento == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                #region Switchcase
                switch (UnityEngine. Random.Range(1, 11))
                {
                    case 11:
                        break;
                    case 10:
                        break;
                    case 9:
                        break;
                    case 8:
                        break;
                    case 7:
                        break;
                    case 6:
                        Debug.Log("Zubat Encontrado");
                        NoEncontro();
                        break;
                    case 5:
                        break;
                    case 4:
                        break;
                    case 3:
                        break;
                    case 2:
                        Debug.Log("Vassoura Encontrada");
                        timer = UnityEngine.Random.Range(HoraDeEncontro[0], HoraDeEncontro[1]);
                        NoEncontro();
                        break;
                    case 1:
                        int pokeRandom = 0;
                        pokeRandom = UnityEngine.Random.Range(1, 3);
                        if(pokeRandom == 1)
                        {
                            Debug.Log("Charmander Encontrado");
                            NoEncontro();
                        }
                        if (pokeRandom == 2)
                        {
                            Debug.Log("Squirtle Encontrado");
                            NoEncontro();
                        }
                        if (pokeRandom == 3)
                        {
                            Debug.Log("Bulbassauro Encontrado");
                            NoEncontro();
                        }
                        timer = UnityEngine.Random.Range(HoraDeEncontro[0], HoraDeEncontro[1]);
                        break;
                }
                #endregion
            }
        }
        #endregion
    }
    #endregion
}
