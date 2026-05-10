using UnityEngine;

public class CollectibleFloat : MonoBehaviour
{
    [Header("Floating")]
    public float floatHeight = 0.3f;   // how high it bobs
    public float floatSpeed = 2f;      // how fast it bobs

    [Header("Spinning")]
    public float spinSpeed = 90f;      // degrees per second

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Floating: moves up and down using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Spinning: rotates around Y axis
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}