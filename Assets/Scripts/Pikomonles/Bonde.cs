using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bonde : MonoBehaviour
{

    [SerializeField] List<Pikomon> pikomons;

    public List<Pikomon> Pikomons { get { return pikomons; } }

    private void Start()
    {
        foreach(var pikomon in pikomons)
        {
            pikomon.Init();
        }
    }

    public Pikomon BichosSaudaveis()
    {
        return pikomons.Where(x => x.HP > 0).FirstOrDefault();
    }

}
