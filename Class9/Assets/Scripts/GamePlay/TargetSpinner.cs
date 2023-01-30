using UnityEngine;

// View the commented version here: https://gist.github.com/theshaneobrien/5d697d74b66698a8576d0601399b0fb6
public class TargetSpinner : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;

    private void Update()
    {
        this.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
    }
}
