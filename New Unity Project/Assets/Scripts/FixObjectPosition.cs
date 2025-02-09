using UnityEngine;

public class FixObjectPosition : MonoBehaviour
{
    // Coloque o objeto que você quer manter a posição aqui
    public Vector3 fixedPosition;
    public Quaternion fixedRotation;

    void Start()
    {
        // Salvar a posição e rotação original
        fixedPosition = transform.position;
        fixedRotation = transform.rotation;
    }

    void Update()
    {
        // Manter a posição e rotação fixa
        transform.position = fixedPosition;
        transform.rotation = fixedRotation;
    }
}