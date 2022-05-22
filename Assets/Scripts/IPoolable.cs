public interface IPoolable
{
    public int PoolId { get; set; }
    void Dispose();

    void SetPooled(bool pooled);
}
