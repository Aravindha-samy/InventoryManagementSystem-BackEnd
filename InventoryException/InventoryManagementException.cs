namespace InventoryManagementSystem.InventoryException
{
    public class InventoryManagementException : Exception
    {
        public InventoryManagementException(string? message) : base(message)
        {
        }

        public InventoryManagementException(string? message, string message1) : base(message)
        {
        }
    }
}
