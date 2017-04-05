using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform parentReturn = null;
	GameObject placeHolder = null;
	public enum blockType {IF , ELSE, ELSEIF, OR, SHOOT, DONTSHOOT, ENEMY};
	public blockType typeOfBlock = blockType.IF;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

		placeHolder = new GameObject();
		placeHolder.transform.SetParent(this.transform.parent);
		LayoutElement layoutElement = placeHolder.AddComponent<LayoutElement>();
		layoutElement.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		layoutElement.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		layoutElement.flexibleWidth = 0;
		layoutElement.flexibleHeight = 0;

		placeHolder.transform.SetSiblingIndex (this.transform.GetSiblingIndex ());

		parentReturn = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = eventData.position;

		int newSiblingIndex = parentReturn.childCount;

		for (int i = 0; i < parentReturn.childCount; i++) {
			if (this.transform.position.x < parentReturn.GetChild(i).position.x && 
				this.transform.position.y > parentReturn.GetChild(i).position.y) {
				newSiblingIndex = i;
				if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex) {
					newSiblingIndex--;
				}
				break;
			}
		}
		placeHolder.transform.SetSiblingIndex(newSiblingIndex);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
		this.transform.SetParent(parentReturn);
		this.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Destroy(placeHolder);
    }

}