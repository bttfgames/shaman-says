using UnityEngine;
using System.Collections;

public class GUIMeatGrade : MonoBehaviour {

    private string[] MeatGrade = {"Extra-Rare", "Rare", "Medium Rare", "Medium Well", "Well Done", "Over Cooked" };
    private int MeatGradeIndex = 0;
    public GameObject PlayerText;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IncreaseMeatGrade ()
    {
        MeatGradeIndex++;
        PlayerText.GetComponent<Text>().Text = MeatGrade[MeatGradeIndex];
    }

}
