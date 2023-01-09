using UnityEngine;

// Here is the commented code: https://gist.github.com/theshaneobrien/a193dea6285200846c4b6f04d685367c
public class Gun : MonoBehaviour
{
    [SerializeField] private LineRenderer gunAimTracer;

    [SerializeField] private AudioSource gunSoundSource;

    [SerializeField] private AudioClip[] gunSounds;

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
            
            gunSoundSource.PlayOneShot(gunSounds[0]);
        }
    }

    private void FireGun()
    {
        RaycastHit hitObject;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitObject, 100))
        {
            if (hitObject.collider != null)
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<Enemy>().Die();
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
