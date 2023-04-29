using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public GameObject objetivo;
    public int vida = 70;

    public Animator Anim;

    private void OnEnable()
    {
        objetivo = GameObject.Find("Objetivo");
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objetivo.transform.position);
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsMovingE", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objetivo")
        {
            Anim.SetBool("IsMovingE", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    public void Danar()
    {
        objetivo?.GetComponent<Objetivo>().RecibirDano(5);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }

}
