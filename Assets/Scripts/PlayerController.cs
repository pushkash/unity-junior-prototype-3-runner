using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerSound;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float jumpForce = 10.0f;
    public float gravityModifier = 1;

    public bool isOnTheGround = true;
    public bool gameOver = false;
    private bool doubleJumped = false;
    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerSound = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            doubleSpeed = true;
            playerAnim.SetFloat("SpeedMult_f", 1.5f);
        } else
        {
            doubleSpeed = false;
            playerAnim.SetFloat("SpeedMult_f", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            doubleJumped = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnTheGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerSound.PlayOneShot(jumpSound);
        } else if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !isOnTheGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerSound.PlayOneShot(jumpSound);
            doubleJumped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerSound.PlayOneShot(crashSound, 1.0f);
        }
    }
}
