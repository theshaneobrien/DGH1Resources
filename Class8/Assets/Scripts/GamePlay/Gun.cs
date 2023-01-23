using UnityEngine;

// Here is the commented code: https://gist.github.com/theshaneobrien/a193dea6285200846c4b6f04d685367c
public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject reticleImage;
    
    [SerializeField] private AudioSource gunSoundSource;

    [SerializeField] private AudioClip[] gunSounds;

    private void Update()
    {
        if (GameStateManager.Instance.GetPlayerIsReady() == true && GameStateManager.Instance.GetPlayerWon() == false)
        {
            EnableGunReticle();
            DetectInput();
        }
    }

    private void EnableGunReticle()
    {
        if (reticleImage.activeSelf == false)
        {
            reticleImage.SetActive(true);
        }
    }

    private void DetectInput()
    {
        if (Input.GetButtonDown("Fire1"))
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
}
