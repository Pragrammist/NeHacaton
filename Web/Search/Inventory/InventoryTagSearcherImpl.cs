using System.Linq;
using Web.Dtos.Sales.Inventory;

namespace Web.Search.Inventory
{

    public class InventoryTagSearcherImpl : InventoryTagSearcher
    {
        bool PrivateTagsAreContained(string[]? tags, string? text) // that method created to use inner class
        {
            if (tags != null && text == null)
                return false;
            else if (text == null || tags == null || tags.Length == 0) // tags are null here
                return true;
            

            tags = tags.Select(t => t.TrimStart('#').ToLower()).ToArray();
            text = text.ToLower();

            return tags.Any(t => text.Contains(t));
        }
        public bool TagsAreContained(string[]? tags, string? text)
        {
            return PrivateTagsAreContained(tags, text);
        }

        public IEnumerable<OutputInventoryDto> SelectInventoriesByTags(string[]? tags, IEnumerable<OutputInventoryDto> inventories) => 
            inventories.Where(i => PrivateTagsAreContained(tags, i.Description)
                || PrivateTagsAreContained(tags, i.Title)
                || PrivateTagsAreContained(tags, i.Artule)
                || PrivateTagsAreContained(tags, i.Point?.Title)
                || i.Prices.Any(t => PrivateTagsAreContained(tags, t.Title))
                || PrivateTagsAreContained(tags, i.Resource?.Title)
                || PrivateTagsAreContained(tags, i.Resource?.Description));
        
    }
}
