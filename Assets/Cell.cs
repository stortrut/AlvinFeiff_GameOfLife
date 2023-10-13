using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	[HideInInspector] public bool alive = false;
	private SpriteRenderer spriteRenderer;
	private int amount;
	private RaycastHit2D[] hitList;

	public void CheckNearbyCells()
    {
		hitList = Physics2D.BoxCastAll(transform.position, gameObject.transform.lossyScale * 1.1f, 0, Vector2.up, 0);
		amount = 0;
        foreach (var item in hitList){
            if (item.collider.gameObject.GetComponent<Cell>().alive)
				amount++;		
        }

		if(alive)
			amount--;
	}

	public void ChangeLifeStatus(bool status)
	{
		spriteRenderer ??= GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = status;
		alive = status;
	}

	public void UpdateLifeStatus()
    {
		if (amount < 2 || amount > 3)
			ChangeLifeStatus(false);
		if (amount == 3)
			ChangeLifeStatus(true);
	}

    private void OnMouseDown()
    {
		ChangeLifeStatus(!alive);
    }
    private void OnMouseEnter()
    {
		if (Input.GetKey(KeyCode.Mouse0))
			ChangeLifeStatus(!alive);
	}
}