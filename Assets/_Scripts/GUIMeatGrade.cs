using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIMeatGrade : MonoBehaviour {

    private string[] MeatGrade = {"Extra-Rare", "Rare", "Medium Rare", "Medium Well", "Well Done", "Over Cooked" };
	private int MeatGradeIndex = 0;

	public Sprite[] LifeBar;
    public GameObject PlayerText;

	// Use this for initialization
	void Awake () {
		//Teste da função
		IncreaseMeatGrade ();
	}

    public void IncreaseMeatGrade ()
    {
        MeatGradeIndex++;
		//Assegura que não vai buscar valor maior que o tamanho do vetor
		if (MeatGradeIndex > MeatGrade.Length)
			return;
		
		Text t = PlayerText.GetComponent<Text>();
		t.text = MeatGrade [MeatGradeIndex];
    }

}
