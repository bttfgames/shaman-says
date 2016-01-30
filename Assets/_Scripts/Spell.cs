using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public enum SpellType { UP, DOWN, LEFT, RIGHT };
    public Color _color;
    public SpellType _type;
    private float _velocity = 1.0f;
    public bool _isOnTrigger = false;
    public bool _last = false;
	
    public void SetType(SpellType type)
    {
        _type = type;
        switch (_type)
        {
            case SpellType.UP:
                _color = Color.yellow;
                break;
            case SpellType.DOWN:
                _color = Color.green;
                break;
            case SpellType.LEFT:
                _color = Color.blue;
                break;
            case SpellType.RIGHT:
                _color = Color.red;
                break;
            default:
                break;
        }
        this.GetComponent<SpriteRenderer>().color = _color;

        this._velocity = Random.Range(1.0f, 3.0f);
    }

}
