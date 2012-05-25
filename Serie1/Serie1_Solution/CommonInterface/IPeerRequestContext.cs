namespace CommonInterface
{
    public interface IPeerRequestContext
    {
        int Jumps { get; set; }
        bool CheckAndAdd(ISuperPeer sp);
    }
}