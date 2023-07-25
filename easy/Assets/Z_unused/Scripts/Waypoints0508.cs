using UnityEngine;

public class Waypoints0508 : MonoBehaviour
{

	public Transform[] points;

	void Awake()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);
		}
	}

}
