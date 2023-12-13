using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public GameObject debrisPrefab; // Définissez le préfabriqué (Prefab) de vos débris dans l'inspecteur Unity
    public int debrisCount = 3; // Nombre de débris à générer

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DestroyBox()
    {
        animator.SetBool("isDestroy", true);
        // Ajoutez une fonction pour générer des débris
        GenerateDebris();
        Destroy(this.gameObject);
    }

    void GenerateDebris()
    {
        for (int i = 0; i < debrisCount; i++)
        {
            // Créez un débris
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);

            // Obtenez un vecteur de direction aléatoire
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            // Ajoutez une force pour éjecter le débris dans cette direction
            debris.GetComponent<Rigidbody2D>().AddForce(randomDirection * Random.Range(2f, 5f), ForceMode2D.Impulse);
        }
    }
}