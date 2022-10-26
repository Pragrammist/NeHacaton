using System.Linq;
using Web.Dtos.Sales.Inventory;

namespace Web.Search.Inventory
{
    //для поиска по категориям
    public class InventoryTagSearcherImpl : InventoryTagSearcher
    {
        bool PrivateTagsAreContained(string[]? tags, string? text) 
        // чтобы избежать в будущем проблемы этот код вынесен в прайват, т.к. публичный метод и прайват могут изменяться по-разному
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
        //Вообще по идеи здесь должно использоваться апи, но т.к. в апи нет категорий, 
        //то вся надежда на то что юзер напишет теги для категорий в описании
        // и что ключевые слова будут содержаться в свойствах инвенторя
        // TODO попростиь людей допилить апи, или спросить как это работает
        // а так это временная реализация
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
