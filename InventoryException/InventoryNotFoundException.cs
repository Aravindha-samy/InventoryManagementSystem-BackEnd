namespace InventoryManagementSystem.InventoryException
{
    public class InventoryNotFoundException : Exception
    {
        public InventoryNotFoundException(string? message) : base(message)
        {
        }
    }
}
