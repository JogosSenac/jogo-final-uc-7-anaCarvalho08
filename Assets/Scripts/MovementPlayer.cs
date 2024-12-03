using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float velocidade = 5.0f;
    [SerializeField] private float forcaPulo = 5.0f;
    [SerializeField] private int chave;
    [SerializeField] private GameObject telaMorte;
    private Vector3 anguloRotacao = new Vector3(0, 90, 0);
    private bool estaVivo = true;

    [Header("Sons")]
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip pegaChave;
    private AudioSource audioPlayer;

    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("TotalChaves").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("Chave").Length.ToString();
    }

    private void Update()
    {
        VerificaObjetivos();
    }

    //Andar
    public void Andar()
    {
        if(estaVivo)
        {
            float moveV = Input.GetAxis("Vertical");
            Vector3 direcao = moveV * transform.forward;
            rb.MovePosition(rb.position + direcao * velocidade * Time.deltaTime);
        }
    }

    //Pular
    public void Pular()
    {
        rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        audioPlayer.PlayOneShot(pulo);
    }

    //Virar
    public void Virar()
    {
        float moveH = Input.GetAxis("Horizontal");
        Quaternion rotacao = Quaternion.Euler(anguloRotacao * moveH * Time.deltaTime);
        rb.MoveRotation(rotacao * rb.rotation);
    }

    public bool Vivo()
    {
        return estaVivo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Espinho"))
        {
            estaVivo = false;
            telaMorte.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chave"))
        {
            Destroy(other.gameObject);
            chave++;
            textoPontos.text = chave.ToString();
            audioPlayer.PlayOneShot(pegaChave);
        }

        if (other.gameObject.CompareTag("PassaFase0") && chave == 1)
        {
            SceneManager.LoadScene("Fase2");
            chave = 0;
        }

        if (other.gameObject.CompareTag("PassaFase1") && chave == 1)
        {
            SceneManager.LoadScene("Final");
            chave = 0;
        }
    }

    private void VerificaObjetivos()
    {
        int totalChaves = Int32.Parse(textoTotal.text);
        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();

        if (chave < totalChaves)
        {
            objetivo.text = "COLETE A CHAVE";
        }

        if (chave == totalChaves)
        {
            objetivo.text = "PASSAGEM LIBERADA";
        }
    }

}
