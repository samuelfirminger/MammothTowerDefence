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

		GameObject selectInstruction = GameObject.Find ("SelectInstruction");
		GameObject programTower = GameObject.Find ("ProgramTower");

		if (this.transform == programTower.transform) {
			if (BetweenScenes.transName == null || BetweenScenes.transName.Equals("Canvas")) {
				GameObject repCard = Instantiate (drag.replacementCard);
				repCard.transform.SetParent (selectInstruction.transform);
				Draggable dragReplaceCard = repCard.GetComponent (typeof(Draggable)) as Draggable;
				dragReplaceCard.parentReturn = null;
				CanvasGroup canvasReplaceCard = repCard.GetComponent (typeof(CanvasGroup)) as CanvasGroup;
				canvasReplaceCard.blocksRaycasts = true;
				RectTransform rectTransform = repCard.GetComponent (typeof(RectTransform)) as RectTransform;
				rectTransform.localScale = new Vector3 (1, 1, 1);
			}
		}
			
		if (this.transform == selectInstruction.transform && BetweenScenes.transName.Equals("Panel")) {
			destroyReplacementCard (drag.typeOfBlock);
		}
	}

	private void destroyReplacementCard(BlockType bt){
		bool hasDeleted = false;
		string str = getName (bt);

		while (!hasDeleted) {
			GameObject toDelete = GameObject.Find (str);
			if (toDelete != null &&
				toDelete.transform.parent == GameObject.Find ("SelectInstruction").transform) {
				GameObject.Destroy(toDelete);
				hasDeleted = true;
			}
			else{
				str = str + "(Clone)";
			}
		}
	}

	private string getName(BlockType bt){
		switch (bt) {
		// Conditions
		case BlockType.IF:
			return "IF";
		case BlockType.ELSE:
			return "ELSE";
		case BlockType.ELSEIF:
			return "ELSEIF";
		case BlockType.AND:
			return "AND";
		case BlockType.OR:
			return "OR";
		// Code properties
		case BlockType.ENCRYPTION:
			return "ENCRYPTION";
		case BlockType.EXTENSION:
			return "EXTENSION";
		case BlockType.SPEED:
			return "SPEED";
		case BlockType.SIZE:
			return "SIZE";
		case BlockType.SOURCE:
			return "SOURCE";
		// Code property values
		case BlockType.TRUE:
			return "TRUE";
		case BlockType.FALSE:
			return "FALSE";
		case BlockType.INTEGER:
			return "INTEGER";
		case BlockType.SOURCE_PROP:
			return "SOURCE_PROP";
		case BlockType.EXTENSION_PROP:
			return "EXTENSION_PROP";
		// Operators
		case BlockType.EQUALS:
			return "EQUALS";
		case BlockType.GREATER:
			return "GREATER";
		case BlockType.LESS:
			return "LESS";
		// Shoot/Dont shoot
		case BlockType.SHOOT:
			return "SHOOT";
		case BlockType.DONTSHOOT:
			return "DONTSHOOT";
		default:
			return "";
		}
	}
}
