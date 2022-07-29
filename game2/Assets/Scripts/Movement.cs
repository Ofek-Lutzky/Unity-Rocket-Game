using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] ParticleSystem rocketMainBoostParcticle;
    [SerializeField] ParticleSystem rocketRightBoostParcticle;
    [SerializeField] ParticleSystem rocketLeftBoostParcticle;
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateSpeed = 100f;     
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource audioSource; 

    // public Renderer Fire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // Fire = GetComponent<Renderer>();
    } 

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrust();

        }
        else
        {
            StopThrust();
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        // Fire.enabled = true;
        if (!rocketMainBoostParcticle.isPlaying)
        {
            rocketMainBoostParcticle.Play();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        // Fire.enabled = false;
        rocketMainBoostParcticle.Stop();
    }

    

    private void RotateRight()
    {
        if (!rocketRightBoostParcticle.isPlaying)
        {
            rocketRightBoostParcticle.Play();
        }
        ApplyRotation(-rotateSpeed);
    }

    private void RotateLeft()
    {
        if (!rocketLeftBoostParcticle.isPlaying)
        {
            rocketLeftBoostParcticle.Play();
        }
        ApplyRotation(rotateSpeed);
    }
    private void StopRotate()
    {
        rocketLeftBoostParcticle.Stop();
        rocketRightBoostParcticle.Stop();
    }

    public void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
