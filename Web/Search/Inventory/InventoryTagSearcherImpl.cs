namespace Web.Search.Inventory
{

    public class InventoryTagSearcherImpl : InventoryTagSearcher
    {
        public bool TagsIsContained(string[] tags, string text)
        {
            if (tags == null || text == null)
                return false;
            else if (tags.Length == 0)
                return true;

            tags = tags.Select(t => t.TrimStart('#').ToLower()).ToArray();
            text = text.ToLower();
            return tags.Any(t => text.Contains(t));
        }
    }
}
