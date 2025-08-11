using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using Moq;
using SportComplexApp.Common;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Web.ViewModels.Spa;
using SpaService = SportComplexApp.Services.Data.SpaService;

namespace SportComplexApp.Tests;

[TestFixture]
public class SpaServiceTests
{
    private SportComplexDbContext _context = null!;
    private SpaService _service = null!;
    private FakeTimeProvider _fakeTime = null!;
    private const string UserId = "userId";

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SportComplexDbContext>()
            .UseInMemoryDatabase(databaseName: $"SpaDb_{Guid.NewGuid()}")
            .Options;

        _fakeTime = new FakeTimeProvider(new DateTimeOffset(2025, 8, 5, 10, 0, 0, TimeSpan.Zero));

        _context = new SportComplexDbContext(options);

        SeedData(_context);

        _service = new SpaService(_context, _fakeTime);
    }

    private void SeedData(SportComplexDbContext context)
    {
        var services = new List<Data.Models.SpaService>
        {
            new Data.Models.SpaService
            {
                Id = 1,
                Name = "Massage",
                Description = "Relax",
                ProcedureDetails = "Full body massage with essential oils.",
                Duration = 60,
                Price = 50,
                ImageUrl = "img1",
                IsDeleted = false,
            },
            new Data.Models.SpaService
            {
                Id = 2,
                Name = "Sauna",
                Description = "Hot room",
                ProcedureDetails = "Hot room",
                Duration = 45,
                Price = 40,
                ImageUrl = "img2",
                IsDeleted = false,
            },
            new Data.Models.SpaService
            {
                Id = 3,
                Name = "Facial",
                Description = "Skin care",
                ProcedureDetails = "Deep cleansing facial with natural products.",
                Duration = 30,
                Price = 30,
                ImageUrl = "img3",
                IsDeleted = true,
            }
        };

        context.SpaServices.AddRange(services);
        context.SaveChanges();
    }
    private static DateTime At(DateTime date, TimeSpan time) =>
        new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Unspecified);

    [Test]
    public async Task GetAllSpaServicesAsync_ShouldReturnAllNonDeletedServices()
    {
        var result = await _service.GetAllSpaServicesAsync();

        var list = result.ToList();
        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(list.Any(s => s.Name == "Massage"), Is.True);
        Assert.That(list.Any(s => s.Name == "Sauna"),Is.True);
        Assert.That(list.Any(s => s.Name == "Facial"), Is.False);

    }

    [Test]
    public async Task GetAllSpaServicesPaginationAsync_ReturnsPaginatedResults()
    {
        var result = await _service.GetAllSpaServicesPaginationAsync(null, null, null, 1, 1);

        Assert.That(result.SpaServices.Count(), Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(2));
        Assert.That(result.CurrentPage, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(1));
        Assert.IsTrue(result.SpaServices.Any(s => s.Name == "Massage" || s.Name == "Sauna"));
    }

    [Test]
    public async Task GetAllSpaServicesPaginationAsync_ReturnsFilteredResults()
    {
        var result = await _service.GetAllSpaServicesPaginationAsync("Massage", 30, 60, 1, 10);

        Assert.That(result.SpaServices.Count(), Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));
        Assert.That(result.CurrentPage, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.IsTrue(result.SpaServices.Any(s => s.Name == "Massage"));
    }

    [Test]
    public async Task GetAllSpaServicesPaginationAsync_ReturnsEmptyList_WhenNoMatches()
    {
        var result = await _service.GetAllSpaServicesPaginationAsync("NonExistentService", null, null, 1, 10);

        Assert.That(result.SpaServices, Is.Empty);
        Assert.That(result.TotalPages, Is.EqualTo(0));
        Assert.That(result.CurrentPage, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
    }

    [Test]
    public async Task GetAllSpaServicesPaginationAsync_SetsCurrentPageToLast_WhenRequestedPageTooHigh()
    {
        int requestedPage = 5;
        int spaPerPage = 1;

        var result = await _service.GetAllSpaServicesPaginationAsync(null, null, null, requestedPage, spaPerPage);

        Assert.That(result.TotalPages, Is.GreaterThan(0));
        Assert.That(result.CurrentPage, Is.EqualTo(result.TotalPages));
        Assert.That(result.SpaServices.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetSpaServiceByIdAsync_ReturnsNull_WhenServiceNotFound()
    {
        int missingId = 999;

        var result = await _service.GetSpaServiceByIdAsync(missingId);

        Assert.IsNull(result);
    }

    [Test]
    public async Task GetSpaServiceByIdAsync_MapsFields_AndSetsReservationDateToTomorrow()
    {
        var result = await _service.GetSpaServiceByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.That(result!.SpaServiceId, Is.EqualTo(1));
        Assert.That(result.SpaServiceName, Is.EqualTo("Massage"));
        Assert.That(result.ImageUrl, Is.EqualTo("img1"));
        Assert.That(result.ReservationDate.Date, Is.EqualTo(_fakeTime.GetLocalNow().DateTime.AddDays(1).Date));
    }

    [Test]
    public async Task GetSpaServiceByIdAsync_IgnoresDeletedFlagForFind_ButYouMayExtendIfNeeded()
    {
        var result = await _service.GetSpaServiceByIdAsync(2);

        Assert.IsNotNull(result);
        Assert.That(result!.SpaServiceName, Is.EqualTo("Sauna"));
        Assert.That(result.ReservationDate.Date, Is.EqualTo(_fakeTime.GetLocalNow().DateTime.AddDays(1).Date));
    }

    [Test]
    public async Task CreateReservation_Succeeds_WhenAllRulesPass()
    {
        // Arrange
        var start = _fakeTime.GetLocalNow().DateTime.AddHours(2);
        Assert.That(start.TimeOfDay, Is.GreaterThanOrEqualTo(ApplicationConstants.OpeningTime));
        Assert.That(start.TimeOfDay, Is.LessThan(ApplicationConstants.ClosingTime));

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = start,
            NumberOfPeople = 2
        };

        // Act
        var id = await _service.CreateReservationAsync(model, UserId);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
        var saved = await _context.SpaReservations.FindAsync(id);
        Assert.IsNotNull(saved);
        Assert.That(saved!.ReservationDateTime, Is.EqualTo(start));
        Assert.That(saved.ClientId, Is.EqualTo(UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenBeforeOpening()
    {
        var beforeOpen = At(_fakeTime.GetLocalNow().DateTime, ApplicationConstants.OpeningTime).AddMinutes(-10);

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = beforeOpen,
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenAtOrAfterClosing()
    {
        var atClosing = At(_fakeTime.GetLocalNow().DateTime, ApplicationConstants.ClosingTime); // >= ClosingTime -> throw

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = atClosing,
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenTooFarInFuture()
    {
        var tooFar = _fakeTime.GetLocalNow().DateTime.AddDays(ApplicationConstants.MaxReservationDaysInAdvance + 1)
            .Date
            .Add(ApplicationConstants.OpeningTime)
            .AddHours(1);

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = tooFar,
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenInPast()
    {
        var past = _fakeTime.GetLocalNow().DateTime.AddMinutes(-5);

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = past,
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenTooSoon_LessThanOneHourFromNow()
    {
        var tooSoon = _fakeTime.GetLocalNow().DateTime.AddMinutes(30); // < now + 1h

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = tooSoon,
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Throws_WhenOverlapsExisting_ForSameUser()
    {
        var existingStart = _fakeTime.GetLocalNow().DateTime.AddHours(2); // 11:30 -> 12:30
        _context.SpaReservations.Add(new SpaReservation
        {
            SpaServiceId = 1,
            ReservationDateTime = existingStart,
            NumberOfPeople = 1,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        var overlappingStart = existingStart.AddMinutes(30); // 12:00 overlaps

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = overlappingStart,
            NumberOfPeople = 1
        };

        // Act + Assert
        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [Test]
    public async Task CreateReservation_Allows_BackToBack_NoOverlap()
    {
        var firstStart = _fakeTime.GetLocalNow().DateTime.AddHours(2); // 11:00 -> 12:00
        _context.SpaReservations.Add(new SpaReservation
        {
            SpaServiceId = 1,
            ReservationDateTime = firstStart,
            NumberOfPeople = 1,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        var secondStart = firstStart.AddMinutes(60);

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = secondStart,
            NumberOfPeople = 1
        };

        // Act
        var id = await _service.CreateReservationAsync(model, UserId);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public async Task CreateReservation_DoesNotConflict_WithOtherUser()
    {
        var existingStart = _fakeTime.GetLocalNow().DateTime.AddHours(2);
        _context.SpaReservations.Add(new SpaReservation
        {
            SpaServiceId = 1,
            ReservationDateTime = existingStart,
            NumberOfPeople = 1,
            ClientId = "other-user"
        });
        await _context.SaveChangesAsync();

        var overlappingSameTime = existingStart.AddMinutes(30);

        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = overlappingSameTime,
            NumberOfPeople = 1
        };

        var id = await _service.CreateReservationAsync(model, UserId);
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void CreateReservation_Throws_NullReference_WhenServiceMissingOrDeleted_CurrentImplementation()
    {
        var model = new SpaReservationFormViewModel
        {
            SpaServiceId = 999,
            ReservationDate = _fakeTime.GetLocalNow().DateTime.AddHours(2),
            NumberOfPeople = 1
        };

        Assert.ThrowsAsync<NullReferenceException>(() =>
            _service.CreateReservationAsync(model, UserId));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}