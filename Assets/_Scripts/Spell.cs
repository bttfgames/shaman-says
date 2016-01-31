using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public enum SpellType { NEW, UP, DOWN, LEFT, RIGHT };
    public Sprite[] _image;
    public SpellType _type;
    private float _velocity = 1.0f;
    public bool _isOnTrigger = false;
    public bool _last = false;
    public GameObject _oldSprite;

    public void SetType(SpellType type)
    {
        _type = type;
        _oldSprite.SetActive(false);
        switch (_type)
        {
            case SpellType.UP:
                GetComponentInChildren<SpriteRenderer>().sprite = _image[0];
                break;
            case SpellType.DOWN:
                GetComponentInChildren<SpriteRenderer>().sprite = _image[1];
                break;
            case SpellType.LEFT:
                GetComponentInChildren<SpriteRenderer>().sprite = _image[2];
                break;
            case SpellType.RIGHT:
                GetComponentInChildren<SpriteRenderer>().sprite = _image[3];
                break;
            default:
                break;
        }
    }

    public bool CheckType(SpellType type)
    {
        Debug.Log("type: " + type + " _type" + _type);
        if(_type == SpellType.NEW)
        {
            SetType(type);
            return true;
        }
        else
        {
            if( _type == type)
            {
                return true;
            }
        }

        return false;
    }

}
