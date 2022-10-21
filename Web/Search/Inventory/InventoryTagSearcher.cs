namespace Web.Search.Inventory
{
    public interface InventoryTagSearcher
    {
        bool TagsIsContained(string[] tags, string text);
    }
}
