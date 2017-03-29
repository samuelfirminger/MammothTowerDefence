using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData){

	}

	public void OnPointerExit(PointerEventData eventData){

	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log ("OnDrop to " + gameObject.name);

		Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();
		if(drag != null) {
			drag.parentReturn = this.transform;
		}
	}
}
