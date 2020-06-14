using UnityEngine;

// Este script es el encargado de generar "metralla"
public class BurstSpawner : MonoBehaviour
{
    //El objeto a instanciar
    public GameObject projectile;
    //Cuántos se deben instanciar
    public int amount;
    //El ángulo de desfase
    public float rotateAngle;
    //La rapidez a la que se disparan
    public float spawnSpeed;


    private void OnEnable()
    {
        //Para cada objeto instanciado:
        for (int i = 0; i < amount; i++)
        {
            //creamos el objeto
            GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
            //le damos la velocidad correspondiente
            p.GetComponent<Rigidbody2D>().velocity = spawnSpeed * (new Vector2 (Mathf.Cos(rotateAngle), Mathf.Sin(rotateAngle)));
            //y rotamos el ángulo para el siguiente
            rotateAngle += Mathf.PI / amount * 2;
        }
    }
}
