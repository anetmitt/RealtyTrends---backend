using Newtonsoft.Json.Linq;

namespace Infrastructure.WebCrawler;

public class PropertyDataCrawler
{
    private static readonly HttpClient HttpClient = new();

    public static async Task<List<RealEstateData>> CrawlData(int itemsPerPage, int page)
    {
        try
        {
            var url = BuildUrl(itemsPerPage, page);
            var response = await SendRequest(url);
            var data = await HandleResponse(response);
            var dataList = ParseData(data);
            return dataList;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error has occured on page {page}: " + ex.Message);
        }
    }

    private static string BuildUrl(int itemsPerPage = 1, int page = 1)
    {
        return $"https://m-api.city24.ee/en_GB/search/realties?address%5Bcc%5D=1&itemsPerPage={itemsPerPage}&page={page}";
    }

    private static async Task<HttpResponseMessage> SendRequest(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await HttpClient.SendAsync(request);
    }

    private static async Task<string> HandleResponse(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    private static List<RealEstateData> ParseData(string data)
    {
        var jsonArray = JArray.Parse(data);
        return jsonArray.Select(property => NewPropertyObj(property)).ToList();
    }

    private static RealEstateData NewPropertyObj(JToken propertyObj)
    {
        var attributes = propertyObj["attributes"] ?? null;
        var hasEnergyClass = attributes != null ? attributes.Contains("ENERGY_CERTIFICATE_TYPE") : false;
        var energyClass = hasEnergyClass ? attributes!["ENERGY_CERTIFICATE_TYPE"]![0]!.ToString() : string.Empty;
        var newData = new RealEstateData
        {
            Id = ExtractValue(propertyObj, "id"),
            Price = ExtractValue(propertyObj, "price"),
            PricePerUnit = ExtractValue(propertyObj, "price_per_unit"),
            RoomCount = ExtractValue(propertyObj, "room_count"),
            PropertySize = ExtractValue(propertyObj, "property_size"),
            CountyName = ExtractValue(propertyObj["address"], "county_name"),
            ParishName = ExtractValue(propertyObj["address"], "parish_name"),
            CityName = ExtractValue(propertyObj["address"], "city_name"),
            StreetName = ExtractValue(propertyObj["address"], "street_name"),
            EnergyCertificateType = energyClass,
            UnitType = ExtractValue(propertyObj, "unit_type"),
            TransactionType = ExtractValue(propertyObj, "transaction_type") == "/transaction_types/1" ? "Sale" : "Rent"
        };

        return newData;
    }

    private static string ExtractValue(JToken token, string propertyName)
    {
        return token[propertyName]?.ToString() ?? string.Empty;
    }
}





// get the response headers an extract the cookie
/*var responseHeaders = response.Headers;
var cookies = responseHeaders.GetValues("Set-Cookie").ToList();
var cookie = cookies[0];*/