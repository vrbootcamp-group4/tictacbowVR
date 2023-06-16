using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;
    
    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    public TrailRenderer trailRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);
        _rigidBody.AddForce(transform.forward * speed * value * 1.5f, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }
    void FixedUpdate()
    {
        if (_inAir)
        {
            _lastPosition = tip.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Rigidbody body))
        {
            _rigidBody.interpolation = RigidbodyInterpolation.None;
            transform.parent =  collision.transform;

            body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Ground")
        {
            if (GameManager.Instance.player1Active)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.Player2Turn);
                StartCoroutine(DelayedDestroy());
            }

            else if (GameManager.Instance.player2Active)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.Player1Turn);
                StartCoroutine(DelayedDestroy());
            }
        }
        Stop();
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
        
    }

}
