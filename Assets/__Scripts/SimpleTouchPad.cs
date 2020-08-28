using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothingDirection;

	private void Awake()
	{
		direction = Vector2.zero;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		origin = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 currentPosition = eventData.position;
		Vector2 directionRaw = currentPosition - origin;
		direction = directionRaw.normalized;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		direction = Vector2.zero;
	}

	public Vector2 GetDirection()
	{
		smoothingDirection = Vector2.MoveTowards(smoothingDirection, direction, smoothing);
		return smoothingDirection;
	}
}
