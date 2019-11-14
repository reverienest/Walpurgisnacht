/// Prevents movement of navigator while activated
public class StickyNode : MenuNode
{
    override public MenuNode Up { get { return activated ? null : base.Up; } }
    override public MenuNode Down { get { return activated ? null : base.Down; } }
    override public MenuNode Left { get { return activated ? null : base.Left; } }
    override public MenuNode Right { get { return activated ? null : base.Right; } }
    override public MenuNode Forward { get { return activated ? null : base.Forward; } }
    override public MenuNode Backward { get { return activated ? null : base.Backward; } }

    private bool activated;

    override public void ActivateNode()
    {
        activated = true;
        base.ActivateNode();
    }

    override public void DeactivateNode()
    {
        activated = false;
        base.DeactivateNode();
    }
}