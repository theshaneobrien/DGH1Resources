using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private LineRenderer gunAimTracer;
    [SerializeField] private GameStateManager gameState;

    private void Update()
    {
        DetectInput();
        DrawLineRenderer();
    }

    private void DetectInput()
    {
        if (Input.GetAxis("Fire1") == 1)
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        RaycastHit hitObject;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitObject, 100))
        {
            if (hitObject.collider != null)
            {

                if (hitObject.collider.name != "Ground")
                {
                    GameObject.Destroy(hitObject.collider.gameObject);
                    gameState.playerScore += 1;
                }
            }
        }
    }

    private void DrawLineRenderer()
    {
        gunAimTracer.SetPosition(0, this.transform.position);
        gunAimTracer.SetPosition(1, this.transform.forward * 100);
    }
}
