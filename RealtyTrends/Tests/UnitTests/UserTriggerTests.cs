using DAL.EF.App;
using DAL.EF.App.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Tests.UnitTests;

public class UserTriggerTests
{

    private readonly ApplicationDbContext _ctx;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly UserTriggerRepository _userTriggerRepository;
    private readonly Guid _testUserId = Guid.NewGuid();
    private readonly Guid _testUserTriggerId = Guid.NewGuid();
    private readonly Guid _testTriggerId1 = Guid.NewGuid();
    private readonly Guid _testTriggerId2 = Guid.NewGuid();
    private readonly Guid _testTriggerId3 = Guid.NewGuid();
    private readonly Guid _testFilterId1 = Guid.NewGuid();
    private readonly Guid _testFilterId2 = Guid.NewGuid();


    public UserTriggerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _ctx = new ApplicationDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<UserTriggerTests>();

        _userTriggerRepository = new UserTriggerRepository(_ctx);

        SeedTheData();
    }

    [Fact]
    public async Task Add_UserTriggers()
    {
        // Arrange
        var userTrigger1 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };

        // Act
        try
        {
            _userTriggerRepository.Add(userTrigger1);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            Assert.True(false);
        }
        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task AllAsync_ReturnsAllUserTriggers()
    {
        // Arrange
        var userTrigger1 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };
        _ctx.Add(userTrigger1);
        await _ctx.SaveChangesAsync();

        // Act
        var userTriggers = await _userTriggerRepository.AllAsync();

        // Assert
        Assert.NotNull(userTriggers);
        Assert.Equal(1, userTriggers.Count());
    }

    [Fact]
    public async Task FindAsync_ExistingId_ReturnsUserTrigger()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userTrigger1 = new UserTrigger
        {
            Id = id,
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };
        _ctx.Add(userTrigger1);
        await _ctx.SaveChangesAsync();

        // Act
        var userTrigger = await _userTriggerRepository.FindAsync(id);

        // Assert
        Assert.NotNull(userTrigger);
        Assert.Equal(id, userTrigger.Id);
    }

    [Fact]
    public async Task FindAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var userTrigger = await _userTriggerRepository.FindAsync(id);

        // Assert
        Assert.Null(userTrigger);
    }

    [Fact]
    public async Task FindAsync_TriggerIdAndUserId_ReturnsUserTrigger()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userTrigger1 = new UserTrigger
        {
            Id = id,
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };
        _ctx.Add(userTrigger1);
        await _ctx.SaveChangesAsync();
        
        // Act
        var userTrigger = await _userTriggerRepository.FindAsync(_testTriggerId1, _testUserId);

        // Assert
        Assert.NotNull(userTrigger);
        Assert.Equal(_testTriggerId1, userTrigger.TriggerId);
        Assert.Equal(_testUserId, userTrigger.AppUserId);
    }

    [Fact]
    public async Task FindAsync_TriggerIdAndUserId_NonExisting_ReturnsNull()
    {
        // Arrange
        var triggerId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var userTrigger = await _userTriggerRepository.FindAsync(triggerId, userId);

        // Assert
        Assert.Null(userTrigger);
    }

    [Fact]
    public async Task AllAsync_UserId_ReturnsUserTriggers()
    {
        // Arrange
        var userTrigger1 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };
        var userTrigger2 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId2,
        };
        _ctx.Add(userTrigger1);
        _ctx.Add(userTrigger2);
        await _ctx.SaveChangesAsync();

        // Act
        var userTriggers = await _userTriggerRepository.AllAsync(_testUserId);

        // Assert
        Assert.NotNull(userTriggers);
        Assert.Equal(2, userTriggers.Count());
        Assert.Equal(_testUserId, userTriggers.First().AppUserId);
    }

    [Fact]
    public async Task UpdateTriggerAsync_ExistingTriggerAndUser_UpdatesTriggerPrice()
    {
        // Arrange
        var userTrigger1 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };
        _ctx.Add(userTrigger1);
        await _ctx.SaveChangesAsync();
        var newPrice = (float)5000.40;

        // Act
        await _userTriggerRepository.UpdateTriggerAsync(_testTriggerId1, _testUserId, newPrice);
        var updatedTrigger = await _ctx.Triggers.FindAsync(_testTriggerId1);

        // Assert
        Assert.NotNull(updatedTrigger);
        Assert.Equal(newPrice, updatedTrigger.UserSquareMeterPrice);
    }

    [Fact]
    public async Task RemoveAsync_ExistingTriggerAndUser_RemovesUserTriggerAndTrigger()
    {
        // Arrange
        var userTrigger1 = new UserTrigger
        {
            Id = Guid.NewGuid(),
            AppUserId = _testUserId,
            TriggerId = _testTriggerId2,
        };
        _ctx.Add(userTrigger1);
        await _ctx.SaveChangesAsync();

        // Act
        var removedUserTrigger = await _userTriggerRepository.RemoveAsync(_testTriggerId2, _testUserId);
        await _ctx.SaveChangesAsync();
        
        var removedTrigger = await _ctx.Triggers.FindAsync(_testTriggerId2);

        // Assert
        Assert.NotNull(removedUserTrigger);
        Assert.Null(removedTrigger);
    }

    private void SeedTheData()
    {
        SeedTriggers();
        SeedAppUsers();
        //SeedUserTriggers();
        SeedFilters();
        SeedTriggerFilters();
    }

    private void SeedTriggerFilters()
    {
        var triggerFilters = new TriggerFilter()
        {
            TriggerId = _testTriggerId1,
            FilterId = _testFilterId1
        };

        var triggerFilters2 = new TriggerFilter()
        {
            TriggerId = _testTriggerId1,
            FilterId = _testFilterId2
        };

        _ctx.TriggerFilters.Add(triggerFilters);
        _ctx.TriggerFilters.Add(triggerFilters2);
        _ctx.SaveChanges();
    }

    private void SeedTriggers()
    {
        var trigger1 = new Trigger
        {
            Id = _testTriggerId1,
            Name = "Trigger1",
            BeginningSquareMeterPrice = 3000,
            UserSquareMeterPrice = 3500,
            CurrentSquareMeterPrice = 3100
        };

        var trigger2 = new Trigger
        {
            Id = _testTriggerId2,
            Name = "Trigger2",
            BeginningSquareMeterPrice = 5000,
            UserSquareMeterPrice = 6000,
            CurrentSquareMeterPrice = null
        };

        var trigger3 = new Trigger
        {
            Id = _testTriggerId3,
            Name = "Trigger3",
            BeginningSquareMeterPrice = 1000,
            UserSquareMeterPrice = 1500,
            CurrentSquareMeterPrice = 1100
        };

        _ctx.Triggers.Add(trigger1);
        _ctx.Triggers.Add(trigger2);
        _ctx.Triggers.Add(trigger3);
        _ctx.SaveChanges();
    }

    private void SeedUserTriggers()
    {
        var userTrigger1 = new UserTrigger
        {
            Id = _testUserTriggerId,
            AppUserId = _testUserId,
            TriggerId = _testTriggerId1,
        };

        var userTrigger2 = new UserTrigger
        {
            Id = _testUserTriggerId,
            AppUserId = _testUserId,
            TriggerId = _testTriggerId2,
        };

        _ctx.UserTriggers.Add(userTrigger1);
        _ctx.UserTriggers.Add(userTrigger2);
        _ctx.SaveChanges();
    }

    private void SeedFilters()
    {
        var filters = SeedFilterTypes();

        var filter1 = new Filter
        {
            Id = _testFilterId1,
            FilterTypeId = filters.Item1.Id,
            Value = 2300,
        };

        var filter2 = new Filter
        {
            Id = _testFilterId2,
            FilterTypeId = filters.Item2.Id,
            FilterType = _ctx.FilterTypes.FirstOrDefault(f => f.Name == "TestFilterType2")!,
            Value = 2500,
        };

        _ctx.Filters.Add(filter1);
        _ctx.Filters.Add(filter2);
        _ctx.SaveChanges();
    }

    private Tuple<FilterType, FilterType> SeedFilterTypes()
    {
        var filterType1 = new FilterType()
        {
            Name = "TestFilterType1",
        };

        var filterType2 = new FilterType()
        {
            Name = "TestFilterType2",
        };

        return new(filterType1, filterType2);
    }

    private void SeedAppUsers()
    {
        var user = new AppUser
        {
            Id = _testUserId,
            UserName = "TestUser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@test19.com",
            PasswordHash = "Hallo1234!"

        };

        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        
    }
}
