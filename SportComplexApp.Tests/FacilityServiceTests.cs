using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data;
using SportComplexApp.Web.ViewModels.Facility;

namespace SportComplexApp.Tests;

[TestFixture]
public class FacilityServiceTests
{
    private SportComplexDbContext _context = null!;
    private FacilityService _service = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SportComplexDbContext>()
            .UseInMemoryDatabase(databaseName: $"FacilityDb_{Guid.NewGuid()}")
            .Options;

        _context = new SportComplexDbContext(options);
        SeedData(_context);
        _service = new FacilityService(_context);
    }

    private void SeedData(SportComplexDbContext context)
    {
        context.Facilities.AddRange(
            new Facility { Id = 1, Name = "Main Arena", IsDeleted = false, Sports = new List<Sport>() },
            new Facility { Id = 2, Name = "Training Hall", IsDeleted = false, Sports = new List<Sport>() },
            new Facility { Id = 3, Name = "Old Stadium", IsDeleted = true, Sports = new List<Sport>() },
            new Facility { Id = 4, Name = "Community Center", IsDeleted = false, Sports = new List<Sport>() }
        );
        context.Sports.AddRange(
            new Sport { Id = 101, Name = "Football", IsDeleted = false, FacilityId = 1},
            new Sport { Id = 102, Name = "Tennis", IsDeleted = true, FacilityId =  1},
            new Sport { Id = 103, Name = "Basketball", IsDeleted = false, FacilityId = 3 },
            new Sport { Id = 104, Name = "Swimming", IsDeleted = false, FacilityId = 4 },
            new Sport { Id = 105, Name = "Volleyball", IsDeleted = false, FacilityId = 4 }

        );
        context.SaveChanges();
    }

    [Test]
    public async Task Returns_Only_NotDeleted_Facilities()
    {
        // Act
        var result = (await _service.GetAllAsync()).ToList();

        // Assert
        Assert.That(result.Select(r => r.Id), Is.EquivalentTo(new[] { 1, 2, 4 }));
        Assert.That(result.Any(r => r.Id == 3), Is.False); // Deleted
    }

    [Test]
    public async Task SportCount_Excludes_Deleted_Sports()
    {
        // Act
        var result = (await _service.GetAllAsync()).ToList();

        var f1 = result.Single(r => r.Id == 1);
        var f2 = result.Single(r => r.Id == 2);
        var f4 = result.Single(r => r.Id == 4);

        // Assert
        Assert.That(f1.SportCount, Is.EqualTo(1)); // Football only
        Assert.That(f2.SportCount, Is.EqualTo(0)); // No sports
        Assert.That(f4.SportCount, Is.EqualTo(2)); // Swimming + Volleyball
    }

    [Test]
    public async Task Maps_Id_And_Name_Correctly()
    {
        // Act
        var result = (await _service.GetAllAsync()).OrderBy(r => r.Id).ToList();

        // Assert
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Main Arena"));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[1].Name, Is.EqualTo("Training Hall"));
        Assert.That(result[2].Id, Is.EqualTo(4));
        Assert.That(result[2].Name, Is.EqualTo("Community Center"));
    }

    [Test]
    public async Task Deleted_Facility_Is_Excluded_Even_If_Has_Active_Sports()
    {
        // Act
        var result = (await _service.GetAllAsync()).ToList();

        // Assert
        Assert.That(result.Any(r => r.Id == 3), Is.False); // 3 has Basketball but is deleted
    }

    [Test]
    public async Task Returns_Empty_When_AllFacilitiesDeleted()
    {
        // Arrange
        foreach (var facility in _context.Facilities)
        {
            facility.IsDeleted = true;
        }
        await _context.SaveChangesAsync();

        // Act
        var result = (await _service.GetAllAsync()).ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task AddAsync_AddsNewFacility_WithGivenName_AndIsDeletedFalse()
    {
        // Arrange
        var model = new AddFacilityViewModel
        {
            Name = "New Sports Hall"
        };

        // Act
        await _service.AddAsync(model);

        var facilities = await _context.Facilities
            .Where(f => f.Name == "New Sports Hall")
            .ToListAsync();

        // Assert
        Assert.That(facilities.Count, Is.EqualTo(1));
        Assert.That(facilities[0].Name, Is.EqualTo("New Sports Hall"));
        Assert.IsFalse(facilities[0].IsDeleted);
    }

    [Test]
    public async Task AddAsync_Increases_Total_Facility_Count()
    {
        // Arrange
        var initialCount = await _context.Facilities.CountAsync();
        var model = new AddFacilityViewModel { Name = "Extra Court" };

        // Act
        await _service.AddAsync(model);
        var newCount = await _context.Facilities.CountAsync();

        // Assert
        Assert.That(newCount, Is.EqualTo(initialCount + 1));
    }

    [Test]
    public async Task AddAsync_DoesNotAddSports_ByDefault()
    {
        // Arrange
        var model = new AddFacilityViewModel { Name = "Empty Facility" };

        // Act
        await _service.AddAsync(model);

        var addedFacility = await _context.Facilities
            .FirstOrDefaultAsync(f => f.Name == "Empty Facility");

        // Assert
        Assert.IsNotNull(addedFacility);
        Assert.That(_context.Sports.All(s => s.FacilityId != addedFacility!.Id), Is.True);
    }

    [Test]
    public async Task ReturnsTrue_WhenFacilityWithNameExists_AndIsNotDeleted()
    {
        // Act
        var exists = await _service.ExistsAsync("Main Arena");

        // Assert
        Assert.IsTrue(exists);
    }

    [Test]
    public async Task ReturnsFalse_WhenFacilityNameDoesNotExist()
    {
        // Act
        var exists = await _service.ExistsAsync("Non Existing Facility");

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task ReturnsFalse_WhenFacilityWithNameIsDeleted()
    {
        // Act
        var exists = await _service.ExistsAsync("Old Stadium");

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task NameMatch_IsCaseSensitive_ByDefault()
    {
        // Act
        var existsLower = await _service.ExistsAsync("main arena");

        // Assert
        Assert.IsFalse(existsLower);
    }

    [Test]
    public async Task ReturnsModel_WhenFacilityExists_AndNotDeleted()
    {
        // Act
        var vm = await _service.GetFacilityForEditAsync(1);

        // Assert
        Assert.IsNotNull(vm);
        Assert.That(vm!.Name, Is.EqualTo("Main Arena"));
    }

    [Test]
    public async Task ReturnsNull_WhenFacilityIsDeleted()
    {
        // Act
        var vm = await _service.GetFacilityForEditAsync(3);

        // Assert
        Assert.IsNull(vm);
    }

    [Test]
    public async Task ReturnsNull_WhenFacilityDoesNotExist()
    {
        // Act
        var vm = await _service.GetFacilityForEditAsync(999);

        // Assert
        Assert.IsNull(vm);
    }

    [Test]
    public async Task MapsOnly_Name_Field_AsExpected()
    {
        // Act
        var vm = await _service.GetFacilityForEditAsync(4);

        // Assert
        Assert.IsNotNull(vm);
        Assert.That(vm!.Name, Is.EqualTo("Community Center"));
    }

    [Test]
    public async Task EditAsync_UpdatesName_WhenFacilityExistsAndNotDeleted()
    {
        // Arrange
        var model = new AddFacilityViewModel { Name = "Updated Arena" };

        // Act
        await _service.EditAsync(1, model);

        var updated = await _context.Facilities.FindAsync(1);

        // Assert
        Assert.That(updated!.Name, Is.EqualTo("Updated Arena"));
    }

    [Test]
    public async Task EditAsync_DoesNothing_WhenFacilityIsDeleted()
    {
        // Arrange
        var model = new AddFacilityViewModel { Name = "Should Not Update" };

        // Act
        await _service.EditAsync(3, model);

        var unchanged = await _context.Facilities.FindAsync(3);

        // Assert
        Assert.That(unchanged!.Name, Is.EqualTo("Old Stadium"));
    }

    [Test]
    public async Task EditAsync_DoesNothing_WhenFacilityDoesNotExist()
    {
        // Arrange
        var model = new AddFacilityViewModel { Name = "Non Existing" };

        // Act
        await _service.EditAsync(999, model);

        // Assert
        Assert.That(await _context.Facilities.CountAsync(f => f.Name == "Non Existing"), Is.EqualTo(0));
    }

    [Test]
    public async Task EditAsync_PersistsChangeInDatabase()
    {
        // Arrange
        var model = new AddFacilityViewModel { Name = "Persisted Name" };

        // Act
        await _service.EditAsync(4, model);

        // Assert
        var persisted = await _context.Facilities.FindAsync(4);
        Assert.That(persisted!.Name, Is.EqualTo("Persisted Name"));
    }

    [Test]
    public async Task ReturnsModel_WhenFacilityExistsAndNotDeleted()
    {
        // Act
        var vm = await _service.GetFacilityForDeleteAsync(1);

        // Assert
        Assert.IsNotNull(vm);
        Assert.That(vm!.Id, Is.EqualTo(1));
        Assert.That(vm.Name, Is.EqualTo("Main Arena"));
    }

    [Test]
    public async Task CorrectlyMaps_Id_And_Name()
    {
        // Act
        var vm = await _service.GetFacilityForDeleteAsync(4);

        // Assert
        Assert.IsNotNull(vm);
        Assert.That(vm!.Id, Is.EqualTo(4));
        Assert.That(vm.Name, Is.EqualTo("Community Center"));
    }

    [Test]
    public async Task DeleteAsync_SoftDeletes_WhenFacilityHasNoSports()
    {
        // Act
        await _service.DeleteAsync(2);

        // Assert
        var f2 = await _context.Facilities.FindAsync(2);
        Assert.IsNotNull(f2);
        Assert.IsTrue(f2!.IsDeleted);
    }
    [Test]
    public void DeleteAsync_Throws_WhenFacilityHasAnySports_Facility1()
    {
        // Arrange: Facility 1 има поне един спорт

        // Act + Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteAsync(1));

        var f1 = _context.Facilities.Find(1);
        Assert.IsFalse(f1!.IsDeleted);
    }

    [Test]
    public void DeleteAsync_Throws_WhenFacilityHasAnySports_Facility4()
    {
        // Arrange: Facility 4 има 2 спорта

        // Act + Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteAsync(4));
        var f4 = _context.Facilities.Find(4);
        Assert.IsFalse(f4!.IsDeleted);
    }

    [Test]
    public void DeleteAsync_CurrentImplementation_ThrowsNullReference_WhenFacilityNotFound()
    {
        // Act + Assert
        Assert.ThrowsAsync<NullReferenceException>(() => _service.DeleteAsync(3));
        Assert.ThrowsAsync<NullReferenceException>(() => _service.DeleteAsync(999));
    }

    [Test]
    public async Task DeleteAsync_DoesNotAffect_OtherFacilities()
    {
        // Arrange
        var before1 = await _context.Facilities.FindAsync(1);
        var before4 = await _context.Facilities.FindAsync(4);

        // Act
        await _service.DeleteAsync(2);

        // Assert
        var after1 = await _context.Facilities.FindAsync(1);
        var after4 = await _context.Facilities.FindAsync(4);

        Assert.IsFalse(after1!.IsDeleted);
        Assert.IsFalse(after4!.IsDeleted);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}