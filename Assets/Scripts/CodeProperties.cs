using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ways of identifying "good" and "bad" code
[System.Serializable]
public class CodeProperties : MonoBehaviour {

	public CodeExtension extension;
	public int speed;
	public int size;
	public bool encryption;
	public CodeSource source;

}
