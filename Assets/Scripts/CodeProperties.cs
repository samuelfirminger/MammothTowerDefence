using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ways of identifying "good" and "bad" code
[System.Serializable]
public class CodeProperties : MonoBehaviour {

	//the extension of the file
	//eg .zip, .exe etc.
	public string extension;

	public int speed;
	public int size;

	public bool encryption;

	//the author of the file
	public CodeSource source;

}
