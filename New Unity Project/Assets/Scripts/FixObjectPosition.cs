using UnityEngine;

public class FixObjectPosition : MonoBehaviour
{
    // Coloque o objeto que voc� quer manter a posi��o aqui
    public Vector3 fixedPosition;
    public Quaternion fixedRotation;

    void Start()
    {
        // Salvar a posi��o e rota��o original
        fixedPosition = transform.position;
        fixedRotation = transform.rotation;
    }

    void Update()
    {
        // Manter a posi��o e rota��o fixa
        transform.position = fixedPosition;
        transform.rotation = fixedRotation;
    }
}