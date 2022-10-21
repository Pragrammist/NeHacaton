using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;
using HendInRentApi.Dto.Inventory;


namespace HendInRentApi
{
    public class InventoryRepositoryApi
    {
        private static readonly string get_invetory_url = API_URL + POST_INVENTORY_ITEMS; // puty dlya poluchenia inventory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestData">soderzhit chto iskat</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public async Task<OutputInventoriesResultDto> PostInvetoryItems(string token, InputInventoryDto? requestData = null)
        {
            var client = GetClientWithHeaders(token); // ne budu povtoryat

            var response = await client.PostAsJsonAsync(get_invetory_url, requestData); // zapros

            await response.StatusIsOKOrThrowException(get_invetory_url); //uze bilo

            var result = await response.Content.ReadJsonByNewtonsoft<OutputInventoriesResultDto>() ?? throw new NullReferenceException("result of HttpContent.ReadFromJsonAsync is null");

            return result;
        }
    }
}
