using System.Collections;
using UnityEngine;

public class DiableScript : MonoBehaviour
{
    public GameObject bouleDeFeuPrefab; // Assurez-vous de définir ceci dans l'inspecteur Unity
    public float vitesseBouleDeFeu = 1f; // Vitesse de la boule de feu

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            animator.SetTrigger("isFire");

            // Instancier la boule de feu à partir du prefab
            GameObject bouleDeFeu = Instantiate(bouleDeFeuPrefab, transform.position, Quaternion.identity);

            // Donner une vélocité à la boule de feu pour la faire se propulser tout droit
            Rigidbody2D rbBouleDeFeu = bouleDeFeu.GetComponent<Rigidbody2D>();

            // Assurez-vous que le Rigidbody2D est présent sur le prefab
            if (rbBouleDeFeu != null)
            {
                // Donner une vélocité à la boule de feu (ici, elle se propulsera vers la droite)
                rbBouleDeFeu.velocity = new Vector2(vitesseBouleDeFeu, 0f);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
