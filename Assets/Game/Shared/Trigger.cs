public struct Trigger
{
    private bool val;
    public bool Value
    {
        get
        {
            bool old = val;
            val = false;
            return old;
        }
        set
        {
            val = value;
        }
    }
}