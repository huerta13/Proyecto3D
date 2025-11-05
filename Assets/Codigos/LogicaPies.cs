using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public LogicaPersonaje1 logicaPersonaje1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        logicaPersonaje1.puedoSaltar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        logicaPersonaje1.puedoSaltar = false;
    }
}

