using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuNode : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onActivate;
    [SerializeField]
    public UnityEvent onDeactivate;
    [SerializeField]
    public UnityEvent onSelect;
    [SerializeField]
    public UnityEvent onDeselect;

    [SerializeField]
    protected MenuNode up = null;
    public virtual MenuNode Up { get { return up; } }
    [SerializeField]
    protected MenuNode down = null;
    public virtual MenuNode Down { get { return down; } }
    [SerializeField]
    protected MenuNode left = null;
    public virtual MenuNode Left { get { return left; } }
    [SerializeField]
    protected MenuNode right = null;
    public virtual MenuNode Right { get { return right; } }
    [SerializeField]
    protected MenuNode forward = null;
    public virtual MenuNode Forward { get { return forward; } }
    [SerializeField]
    protected MenuNode backward = null;
    public virtual MenuNode Backward { get { return backward; } }

    [SerializeField]
    private Color defaultOutlineColor = Color.white;
    public void ResetColor() { OutlineColor = defaultOutlineColor; }

    private Color _outlineColor = Color.white;
    public Color OutlineColor
    {
        get { return _outlineColor; }
        set
        {
            _outlineColor = value;
            if (outline)
            {
                outline.effectColor = _outlineColor;
            }
        }
    }

    private Outline outline;

    void Start()
    {
        ResetColor();
    }

    public virtual void SelectNode()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.effectColor = OutlineColor;
        outline.effectDistance = new Vector2(2, 2);
        onSelect.Invoke();
    }

    public virtual void DeselectNode()
    {
        Destroy(outline);
        outline = null;
        onDeselect.Invoke();
    }
    public virtual void ActivateNode() { onActivate.Invoke(); }
    public virtual void DeactivateNode() { onDeactivate.Invoke(); }
}
