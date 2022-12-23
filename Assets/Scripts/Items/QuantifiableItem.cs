namespace ChiciStudios.ProjectPhoenix.Items
{
    public class QuantifiableItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public QuantifiableItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}