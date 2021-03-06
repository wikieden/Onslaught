using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBullet : MonoBehaviour
{
    [Tooltip("in seconds")]
    public float cooldown = 0.25f;
    [Tooltip("in seconds")]
    public float hapticTime = 0.15f;
    public GameObject bulletPrefab = null;
    public GameObject specialBulletPrefab = null;
    public AudioClip fireClip = null;
    public AudioClip specialAmmoFireClip = null;

    private float m_Timer = 0f;
    private Transform m_Transform = null;
    private AudioSource m_audioSource = null;
    private int m_NumSpecialAmmo = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        if (fireClip != null || specialAmmoFireClip != null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.clip = fireClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Timer > 0)
            m_Timer -= Time.deltaTime;

        if (m_Timer <= 0 && InputAbstraction.FireControlActive(InputAbstraction.PreferedHand()))
        {
            if (m_NumSpecialAmmo > 0 && specialBulletPrefab != null)
            {
                Instantiate(specialBulletPrefab, m_Transform.position, m_Transform.rotation);
                if (m_audioSource != null)
                    m_audioSource.PlayOneShot(specialAmmoFireClip);
                HapticAbstraction.BuzzBothHands(hapticTime);

                m_NumSpecialAmmo--;
            }
            else if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, m_Transform.position, m_Transform.rotation);
                if (m_audioSource != null)
                    m_audioSource.Play();
                HapticAbstraction.BuzzBothHands(hapticTime);
            }

            m_Timer = cooldown;
        }
    }

    public void AddSpecialAmmo(int ammoToAdd)
    {
        m_NumSpecialAmmo += ammoToAdd;
    }
}
