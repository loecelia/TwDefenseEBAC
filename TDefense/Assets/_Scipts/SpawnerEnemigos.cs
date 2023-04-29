using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{

    public List<GameObject> prefabsEnemigos;
    public int oleada;
    public List<int> enemigosPorOleada;

    private int enemigosDuranteEStaOleada;

    public delegate void OleadaTerminada();
    public event OleadaTerminada EnOleadaTerminada;

    // Start is called before the first frame update
    void Start()
    {
        oleada = 0;
        ConfigurarCantidadDeEnemigos();
        InstanciarEnemigo();
    }

   public void TerminaOla()
    {
        if(EnOleadaTerminada != null)
        {
            EnOleadaTerminada();
        }
    }

    public void ConfigurarCantidadDeEnemigos()
    {
        enemigosDuranteEStaOleada = enemigosPorOleada[oleada];
    }

    public void InstanciarEnemigo()
    {
        int IndiceAleatorio = Random.Range(0, prefabsEnemigos.Count);
        Instantiate<GameObject>(prefabsEnemigos[IndiceAleatorio], transform.position, Quaternion.identity);
        enemigosDuranteEStaOleada--;
        if(enemigosDuranteEStaOleada < 0)
        {
            oleada++;
            ConfigurarCantidadDeEnemigos();
            TerminaOla();
            return; 
        }
        Invoke("InstanciarEnemigo", 2); 
    }
}
