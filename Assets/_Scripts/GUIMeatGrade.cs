using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIMeatGrade : MonoBehaviour {

    private string[] MeatGrade = {"Extra-Rare", "Rare", "Medium Rare", "Medium Well", "Well Done", "Over Cooked" };
	private int MeatGradeIndex = 0;

	public Sprite[] LifeBar;
    public GameObject PlayerText;
	public bool ToRight = true;

    public void IncreaseMeatGrade ()
    {
        MeatGradeIndex++;
		//Assegura que não vai buscar valor maior que o tamanho do vetor
		if (MeatGradeIndex > MeatGrade.Length -1)
			return;
		
		Text t = PlayerText.GetComponent<Text>();
		t.text = MeatGrade [MeatGradeIndex];

		//movimenta a barra
		if (ToRight)
			transform.Translate(1.45f,0,0);
		else
			transform.Translate(-1.45f,0,0);

		//troca a imagem da barra
		Image NewBar = gameObject.GetComponent<Image>();
		NewBar.sprite = LifeBar [MeatGradeIndex];
    }

}
