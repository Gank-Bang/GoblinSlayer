using System.Collections;
using UnityEngine;

public class DiableScript : MonoBehaviour
{
    public GameObject bouleDeFeuPrefab; // Assurez-vous de définir ceci dans l'inspecteur Unity
    public float vitesseBouleDeFeu = 5f; // Vitesse de la boule de feu
    public Transform pointDeDepart; // Point de départ de la boule de feu (la bouche du diable)

    public float coolDown;

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

            yield return new WaitForSeconds(coolDown);
        }
    }

    void Fireing(){
            GameObject bouleDeFeu = Instantiate(bouleDeFeuPrefab, pointDeDepart.position, Quaternion.identity);

            // Donner une vélocité à la boule de feu pour la faire se propulser tout droit
            Rigidbody2D rbBouleDeFeu = bouleDeFeu.GetComponent<Rigidbody2D>();

            // Assurez-vous que le Rigidbody2D est présent sur le prefab
            if (rbBouleDeFeu != null)
            {
               Vector2 direction = Vector2.left;
                // Donner une vélocité à la boule de feu (ici, elle se propulsera vers la droite)
                rbBouleDeFeu.velocity = direction* vitesseBouleDeFeu;
            }
    }
}
