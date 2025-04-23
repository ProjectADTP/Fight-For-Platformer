public interface ICollectibleItem
{
    public void Accept(IItemTaker visitor);
    public void Remove();
}
