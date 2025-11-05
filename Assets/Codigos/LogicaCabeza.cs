using UnityEngine;

public class LogicaCabeza : MonoBehaviour
{

    public int contadorDeColision = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        contadorDeColision++;
    }

    private void OnTriggerExit(Collider other)
    {
        contadorDeColision--;
    }
}
