using UnityEngine;

public class Chave : MonoBehaviour
{
    [SerializeField] private float rX;
    [SerializeField] private float rY;
    [SerializeField] private float rZ;
    [SerializeField] private float velocidade;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(rX * velocidade * Time.deltaTime, rY * velocidade * Time.deltaTime, rZ * velocidade * Time.deltaTime);
    }
}
