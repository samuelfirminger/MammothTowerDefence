using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Draggable sript encodes information regarding draggable elements of the DragNDrop interface
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject replacementCard = null;
	private bool deletePlaceHolder = false;

	public Transform parentReturn = null;
	GameObject placeHolder = null;
	public BlockType typeOfBlock;
	private int intValue;
	private CodeSource codeSource;
	private CodeExtension codeExtension;

    public void OnBeginDrag(PointerEventData eventData)
    {
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

		BetweenScenes.transName = this.transform.parent.name;
	}

    //Drag events must replace cards once they are put into the programming interface
    public void OnDrag(PointerEventData eventData)
    {
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

    //placeHolder blankSpace is filled by a duplicate card when the drag ends
    public void OnEndDrag(PointerEventData eventData)
    {
		this.transform.SetParent(parentReturn);
		this.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Destroy(placeHolder);
    }

    //Getters and Setters for various properties
	public void setIntValue(int intValue){
		this.intValue = intValue;
	}

	public int getIntValue(){
		return intValue;
	}

	public void setCodeSource(CodeSource codeSource){
		this.codeSource = codeSource;
	}

	public CodeSource getCodeSource(){
		return codeSource;
	}

	public void setCodeExtension(CodeExtension codeExtension){
		this.codeExtension = codeExtension;
	}

	public CodeExtension getCodeExtension(){
		return codeExtension;
	}
}