using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI textoVida; 

    private void Start(){
        slider = GetComponent<Slider>();
    }

    public void CambiarVidaMaxima(float vidaMaxima){
        slider.maxValue = vidaMaxima;
    }

    public void CambiarVidaActual(float cantidadVida){
        slider.value = cantidadVida;
        textoVida.text = cantidadVida.ToString();
    }

    public void InicializarBarraDeVida(float cantidadVida){
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
        textoVida.text = cantidadVida.ToString();
    }
}
