using UnityEngine;
using System.Collections;

public class DrawGizmos : MonoBehaviour {

    public GameManager GM;
    public Color _boxColor = Color.red;


	void OnDrawGizmos() {
        if (GM.DrawGizmos)
        {
            Gizmos.color = _boxColor;
            Gizmos.DrawWireCube(this.transform.position, this.GetComponent<BoxCollider>().size);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, this.transform.localScale.x * 0.1f);
        }
    }
}
