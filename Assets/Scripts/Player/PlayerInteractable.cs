public interface IPlayerInteractable
{
    class InteractionContext
    {
        public PlayerData PlayerData { get; }
        public InventorySlot InventorySlot { get; }
        public InteractionContext(PlayerData playerData, InventorySlot inventorySlot)
        {
            PlayerData = playerData;
            InventorySlot = inventorySlot;
        }
    }
    void Interact(InteractionContext ctx);
    ItemData RequiredItem(InteractionContext ctx);
    int Priority { get => 0; }
}
