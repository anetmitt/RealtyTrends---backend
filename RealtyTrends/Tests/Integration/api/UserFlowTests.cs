using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using Xunit.Abstractions;

namespace Tests.Integration.api;

public class UserFlowTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Guid _testAccountId = Guid.NewGuid();
    
    public UserFlowTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
    }
    
    [Fact]
    public async Task GetAllUserTriggers_ShouldReturnOk()
    {
        // Arrange
        var token = await LogIn();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        // Act
        var response = await _client.GetAsync("api/v1/UserTriggers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetAllUserTriggers_ShouldReturnUnauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid token");
        
        // Act
        var response = await _client.GetAsync("api/v1/UserTriggers");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task LetUserAddNewTrigger_ShouldReturnOk()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await LogIn());
        var url = "/api/v1/UserTriggers";
        
        // Act
        var triggerFilters = new StatisticFilters();

        triggerFilters.CountySelect = Guid.Parse("c5a0aa07-7465-41bf-a806-a4518a641d05");
        triggerFilters.TransactionType = Guid.Parse("00000000-0000-0000-0000-000000000000");
        triggerFilters.PropertyType = Guid.Parse("00000000-0000-0000-0000-000000000000");
        triggerFilters.StartDate = DateOnly.FromDateTime(DateTime.Now);
        triggerFilters.EndDate = DateOnly.FromDateTime(DateTime.Now);
        triggerFilters.TriggerName = "TestTrigger";
        triggerFilters.TriggerPricePerUnit = 2500;

        var dataJsom= JsonSerializer.Serialize(triggerFilters);;
        var data = new StringContent(dataJsom, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, data);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task LetUserGetStatistics_ShouldReturnOk()
    {
        var url = "/api/v1/UserTriggers";
        
        var statsFilters = new StatisticFilters();

        statsFilters.CountySelect = Guid.Parse("c5a0aa07-7465-41bf-a806-a4518a641d05");
        statsFilters.TransactionType = Guid.Parse("00000000-0000-0000-0000-000000000000");
        statsFilters.PropertyType = Guid.Parse("00000000-0000-0000-0000-000000000000");
        statsFilters.StartDate = DateOnly.FromDateTime(DateTime.Now);

        var dataJsom= JsonSerializer.Serialize(statsFilters);;
        var data = new StringContent(dataJsom, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(url, data);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
    }
    
    private async Task<string> ApiRegisterTestAccount()
    {
        // Arrange
        const string Url = "/api/v1/identity/account/register?expiresInSeconds=1";
        const string email = "register@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";

        var registerData = new
        {
            Email = email,
            Password = password,
            FirstName = firstname,
            LastName = lastname
        };
        var dataJsom= JsonSerializer.Serialize(registerData);;
        var data = new StringContent(dataJsom, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(Url, data);

        if (response.IsSuccessStatusCode)
        {
            var registered = await response.Content.ReadFromJsonAsync<JwtResponse>();
            return registered!.Token;
        }

        throw new Exception($"Registration failed with status code {response.StatusCode}");
    }
    
    private async Task<string> LogIn()
    {
        const string url = "/api/v1/identity/Account/Login";
        const string email = "register@test.ee";
        const string password = "Foo.bar1";

        var loginData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);
        var response = await _factory.CreateClient().PostAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
            var responseObject = await response.Content.ReadFromJsonAsync<JwtResponse>();
            return responseObject!.Token;
        }
        else
        {
            return await ApiRegisterTestAccount();
        }
    }
}
