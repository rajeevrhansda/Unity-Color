using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{

	Mesh mesh;
	[SerializeField] Transform holeCenter;
	[SerializeField] float radius;
	List<int> holeVertices;
	List<Vector3> offsets;
	int holeVerticesCount;


	float x;
	float y;
	Vector3 touch;
	Vector3 targetPos;
	[SerializeField] float moveSpeed;
	[SerializeField] Vector2 moveLimits;


	[SerializeField] MeshFilter meshFilter;
	[SerializeField] MeshCollider meshCollider;

	[SerializeField] Transform rotatingCircle;


	void Start()
	{
		RotateCircleAnim();
		Game.isMoving = false;
		Game.isGameover = false;

		holeVertices = new List<int>();
		offsets = new List<Vector3>();

		mesh = meshFilter.mesh;

		FindHoleVertices();
	}


	void Update()
	{

#if UNITY_EDITOR

		Game.isMoving = Input.GetMouseButton(0);

		if (!Game.isGameover && Game.isMoving)
		{
			MoveHole();

			UpdateHoleVerticesPosition();
		}

#else


		Game.isMoving = Input.touchCount > 0 && 
			Input.GetTouch(0).phase == TouchPhase.Moved;


		if (!Game.isGameover && Game.isMoving)
		{
			MoveHole();

			UpdateHoleVerticesPosition();
		}
#endif

	}

	void MoveHole()
	{
		x = Input.GetAxis("Mouse X");
		y = Input.GetAxis("Mouse Y");
		
		touch = Vector3.Lerp(
			holeCenter.position,
			holeCenter.position + new Vector3(x, 0f, y), 
			moveSpeed * Time.deltaTime
		);

		targetPos = new Vector3
			(Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x)
			,touch.y,Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)
		);
		holeCenter.position = targetPos;
	}

	void UpdateHoleVerticesPosition()
	{
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < holeVerticesCount; i++)
		{
			vertices[holeVertices[i]] = holeCenter.position + offsets[i];
		}

		mesh.vertices = vertices;

		meshFilter.mesh = mesh;

		meshCollider.sharedMesh = mesh;
	}

	void FindHoleVertices()
	{
		for (int i = 0; i < mesh.vertices.Length; i++)
		{
			float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

			if (distance < radius)
			{
				holeVertices.Add(i);

				offsets.Add(mesh.vertices[i] - holeCenter.position);
			}
		}

		holeVerticesCount = holeVertices.Count;
	}

	void RotateCircleAnim()
	{
		rotatingCircle
			.DORotate(new Vector3(90f, 0f, -90f), 0.2f)
			.SetEase(Ease.Linear)
			.From(new Vector3(90f, 0f, 0f))
			.SetLoops(-1, LoopType.Incremental);
	}
	private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(holeCenter.position, radius);
	}
}
