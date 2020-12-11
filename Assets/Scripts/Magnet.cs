using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    [Header("Magnet options")]
    [SerializeField] private float MagnetForce;

    private List<Rigidbody> affectedRigidbodies;
	private Transform magnet;

    private void Start()
    {
        magnet = transform;
        affectedRigidbodies = new List<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!Game.IsGameOver && Game.IsMoving)
		{
			foreach (Rigidbody rb in affectedRigidbodies)
			{
				rb.AddForce((magnet.position - rb.position) * MagnetForce * Time.fixedDeltaTime);
			}
		}
    }

    private void OnTriggerEnter(Collider other)
	{
		var tag = other.tag;

		if (!Game.IsGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")))
			AddToMagnetField(other.attachedRigidbody);
	}

	private void OnTriggerExit(Collider other)
	{
		var tag = other.tag;

		if (!Game.IsGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")))
			RemoveFromMagnetField(other.attachedRigidbody);
	}

    public void AddToMagnetField(Rigidbody rb) => affectedRigidbodies.Add(rb);
	public void RemoveFromMagnetField(Rigidbody rb) => affectedRigidbodies.Remove(rb);
}
