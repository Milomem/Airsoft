
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public GameObject bbPrefab;
    public Transform canoArma;
    public float energiaDisparo = 1.49f;
    public float massaBB = 0.2f;
    public int municao = 15;
    public int municaoReserva = 30;
    public bool recarregando = false;
    private float fireTimer;
    public float reloadDelay = 1f;
    public float timeReload = 0;
    public float timeReloading = 0;
    public float timeToReload = 2.4f;

    [SerializeField] private float fireRate;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanShoot() && !recarregando)
        {
            DispararBB();
        }
        if (municao != 15)
        {
            if (Input.GetKeyDown("r") && Time.time - timeReload > reloadDelay)
            {
                timeReload = Time.time;
                timeReloading = timeReload;
                recarregando = true;
            }
            if (recarregando)
            {
                if (Time.time - timeReloading > timeToReload)
                {
                    if (municaoReserva - municao >= 15)
                    {
                        recarregando = false;
                        setMunicao(15);
                        setMunicaoReserva(municaoReserva - (15 - municao));
                    }
                    else
                    {
                        recarregando = false;
                        setMunicao(municao + municaoReserva);
                        setMunicaoReserva(0);
                    }
                }
            }
        }
    }
    public int getMunicao()
    {
        return municao;
    }

    public void setMunicao(int newMunicao)
    {
        municao = newMunicao;
        //updateMunicaoText();
    }

    public int getMunicaoReserva()
    {
        return municaoReserva;
    }

    public void setMunicaoReserva(int newMunicaoReserva)
    {
        municaoReserva = newMunicaoReserva;
        //updateMunicaoText();
    }

    private void DispararBB()
    {
        fireTimer = Time.time +fireRate;
        if (municao > 0) 
        {
            GameObject novaBB = Instantiate(bbPrefab, canoArma.position, canoArma.rotation);
            Rigidbody rb = novaBB.GetComponent<Rigidbody>();

            Vector3 direcaoDisparo = canoArma.forward;
            rb.AddForce(direcaoDisparo * CalculaVelocidadeInicial(), ForceMode.VelocityChange);
            setMunicao(municao - 1);
        }
        
    }

    private float CalculaVelocidadeInicial()
    {
        float velocidadeInicial = Mathf.Sqrt(energiaDisparo * 2 / massaBB);
        return velocidadeInicial;
    }
    private bool CanShoot()
    {
        return Time.time > fireTimer;
    }
}
