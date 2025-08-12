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

        context.Users.AddRange(
            new Client { Id = "u1", UserName = "u1@test.com", NormalizedUserName = "U1@TEST.COM", Email = "u1@test.com", NormalizedEmail = "U1@TEST.COM", FirstName = "Alice", LastName = "G" },
            new Client { Id = "u2", UserName = "u2@test.com", NormalizedUserName = "U2@TEST.COM", Email = "u2@test.com", NormalizedEmail = "U2@TEST.COM", FirstName = "Bob", LastName = "B" }
        );

        var reservations = new List<SpaReservation>
        {
            new SpaReservation
            {
                Id = 101,
                ClientId = "u1",
                SpaServiceId = 1,
                ReservationDateTime = _fakeTime.GetLocalNow().DateTime.AddHours(1),
                NumberOfPeople = 2
            },
            new SpaReservation
            {
                Id = 102,
                ClientId = "u1",
                SpaServiceId = 2,
                ReservationDateTime = _fakeTime.GetLocalNow().DateTime.AddHours(3),
                NumberOfPeople = 1
            },
            new SpaReservation
            {
                Id = 201,
                ClientId = "u2",
                SpaServiceId = 1,
                ReservationDateTime = _fakeTime.GetLocalNow().DateTime.AddHours(4),
                NumberOfPeople = 1
            }
        };
        context.SpaReservations.AddRange(reservations);

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

    [Test]
    public async Task CreateReservationAsync_Throws_WhenOverlapsWithSportReservation_ForSameUser()
    {
        // Arrange
        var now = _fakeTime.GetLocalNow().DateTime;

        var spaStart = now.AddHours(2);
        var spaModel = new SpaReservationFormViewModel
        {
            SpaServiceId = 1,
            ReservationDate = spaStart,
            NumberOfPeople = 1
        };

        if (!await _context.Sports.AnyAsync(s => s.Id == 10))
        {
            _context.Sports.Add(new Sport { Id = 10, Name = "Football", IsDeleted = false });
            await _context.SaveChangesAsync();
        }

        _context.Reservations.Add(new Reservation
        {
            Id = 900,
            ClientId = "u1",
            SportId = 10,
            ReservationDateTime = spaStart.AddMinutes(30),
            Duration = 60,
            NumberOfPeople = 1
        });
        await _context.SaveChangesAsync();

        // Act + Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreateReservationAsync(spaModel, "u1"));

    }

    [Test]
    public async Task GetAllForHomeAsync_ReturnsOnlyActive_WithProjection()
    {
        var list = (await _service.GetAllForHomeAsync()).ToList();

        CollectionAssert.AreEquivalent(new[] { 1, 2 }, list.Select(x => x.Id));
        Assert.IsTrue(list.Any(x => x.Name == "Massage" && x.ImageUrl == "img1"));
        Assert.IsTrue(list.Any(x => x.Name == "Sauna" && x.ImageUrl == "img2"));
    }

    [Test]
    public async Task GetUserReservationsAsync_ReturnsUserReservations_WithComputedFields()
    {
        var list = (await _service.GetUserReservationsAsync("u1")).OrderBy(x => x.Id).ToList();

        Assert.That(list.Count, Is.EqualTo(2));

        var r101 = list[0];
        Assert.That(r101.Id, Is.EqualTo(101));
        Assert.That(r101.SpaServiceName, Is.EqualTo("Massage"));
        Assert.That(r101.Duration, Is.EqualTo(60));
        Assert.That(r101.People, Is.EqualTo(2));
        Assert.That(r101.TotalPrice, Is.EqualTo(100)); // 50 * 2

        var r102 = list[1];
        Assert.That(r102.SpaServiceName, Is.EqualTo("Sauna"));
        Assert.That(r102.Duration, Is.EqualTo(45));
        Assert.That(r102.People, Is.EqualTo(1));
        Assert.That(r102.TotalPrice, Is.EqualTo(40)); // 40 * 1
    }

    [Test]
    public async Task GetUserReservationsAsync_ReturnsEmpty_ForUnknownUser()
    {
        var list = await _service.GetUserReservationsAsync("unknown");
        Assert.That(list, Is.Empty);
    }

    [Test]
    public async Task GetSpaDetailsByIdAsync_ReturnsDetails_WhenExists()
    {
        var vm = await _service.GetSpaDetailsByIdAsync(1);

        Assert.IsNotNull(vm);
        Assert.That(vm!.Name, Is.EqualTo("Massage"));
        Assert.That(vm.ProcedureDetails, Is.EqualTo("Full body massage with essential oils."));
        Assert.That(vm.Price, Is.EqualTo(50));
        Assert.That(vm.Duration, Is.EqualTo(60));
        Assert.That(vm.ImageUrl, Is.EqualTo("img1"));
    }

    [Test]
    public async Task GetSpaDetailsByIdAsync_ReturnsNull_WhenNotFound()
    {
        var vm = await _service.GetSpaDetailsByIdAsync(999);
        Assert.IsNull(vm);
    }

    [Test]
    public async Task GetSpaDetailsByIdAsync_ReturnsDeleted_AsPerCurrentImplementation()
    {
        var vm = await _service.GetSpaDetailsByIdAsync(3);
        Assert.IsNotNull(vm);
        Assert.That(vm!.Name, Is.EqualTo("Facial"));
    }

    [Test]
    public async Task CancelReservationAsync_RemovesWhenOwnedByUser()
    {
        await _service.CancelReservationAsync(101, "u1");
        _context.ChangeTracker.Clear();

        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 101), Is.False);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 102), Is.True);
    }

    [Test]
    public async Task CancelReservationAsync_DoesNothing_WhenNotOwnedOrMissing()
    {
        await _service.CancelReservationAsync(101, "u2");
        await _service.CancelReservationAsync(999, "u1");
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 101), Is.True);
    }

    [Test]
    public async Task ReservationExistsAsync_ReturnsTrue_ForExistingOwnedReservation()
    {
        Assert.IsTrue(await _service.ReservationExistsAsync(101, "u1"));
    }

    [Test]
    public async Task ReservationExistsAsync_ReturnsFalse_WhenNotOwnedOrMissing()
    {
        Assert.IsFalse(await _service.ReservationExistsAsync(101, "u2"));
        Assert.IsFalse(await _service.ReservationExistsAsync(999, "u1"));
    }

    [Test]
    public async Task DeleteExpiredSpaReservationsAsync_RemovesNothing_WhenAllAreInFuture()
    {
        await _service.DeleteExpiredSpaReservationsAsync("u1");
        _context.ChangeTracker.Clear();

        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 101), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 102), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 201), Is.True);
    }

    [Test]
    public async Task DeleteExpiredSpaReservationsAsync_RemovesExpired_ForUser()
    {
        // Arrange
        var now = _fakeTime.GetLocalNow().DateTime;

        _context.SpaReservations.AddRange(
            new SpaReservation
            {
                Id = 301,
                ClientId = "u1",
                SpaServiceId = 1,
                ReservationDateTime = now,
                NumberOfPeople = 1
            },
            new SpaReservation
            {
                Id = 302,
                ClientId = "u1",
                SpaServiceId = 2,
                ReservationDateTime = now.AddMinutes(-30),
                NumberOfPeople = 1
            }
        );
        await _context.SaveChangesAsync();

        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 301), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 302), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 101), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 102), Is.True);

        // Act
        await _service.DeleteExpiredSpaReservationsAsync("u1");

        // Assert
        _context.ChangeTracker.Clear();
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 301), Is.False);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 302), Is.False);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 101), Is.True);
        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 102), Is.True);

        Assert.That(await _context.SpaReservations.AnyAsync(r => r.Id == 201), Is.True);
    }

    [Test]
    public async Task AddAsync_CreatesSpaService_WithFields()
    {
        var model = new AddSpaServiceViewModel
        {
            Name = $"Hot Stones {Guid.NewGuid()}",
            Description = "Stones",
            ProcedureDetails = "Basalt",
            Price = 70,
            Duration = 50,
            ImageUrl = "hs.jpg"
        };

        await _service.AddAsync(model);

        var created = await _context.SpaServices.FirstOrDefaultAsync(s => s.Name.StartsWith("Hot Stones "));
        Assert.IsNotNull(created);
        Assert.That(created!.Description, Is.EqualTo("Stones"));
        Assert.That(created.ProcedureDetails, Is.EqualTo("Basalt"));
        Assert.That(created.Price, Is.EqualTo(70));
        Assert.That(created.Duration, Is.EqualTo(50));
        Assert.That(created.ImageUrl, Is.EqualTo("hs.jpg"));
        Assert.IsFalse(created.IsDeleted);
    }

    [Test]
    public async Task ExistsAsync_ReturnsTrue_ForCaseInsensitiveMatch_AndTrim()
    {
        Assert.IsTrue(await _service.ExistsAsync("  MASSAGE "));
        Assert.IsTrue(await _service.ExistsAsync("sauna"));
    }

    [Test]
    public async Task ExistsAsync_ReturnsFalse_WhenDeletedOrMissing()
    {
        Assert.IsFalse(await _service.ExistsAsync("Facial")); // IsDeleted = true
        Assert.IsFalse(await _service.ExistsAsync("NoSuch"));
    }

    [Test]
    public async Task GetForEditAsync_ReturnsModel_WhenActive()
    {
        var vm = await _service.GetForEditAsync(1);
        Assert.IsNotNull(vm);
        Assert.That(vm!.Name, Is.EqualTo("Massage"));
        Assert.That(vm.Description, Is.EqualTo("Relax"));
        Assert.That(vm.ProcedureDetails, Is.EqualTo("Full body massage with essential oils."));
        Assert.That(vm.Price, Is.EqualTo(50));
        Assert.That(vm.Duration, Is.EqualTo(60));
        Assert.That(vm.ImageUrl, Is.EqualTo("img1"));
    }

    [Test]
    public async Task GetForEditAsync_ReturnsNull_WhenDeletedOrMissing()
    {
        Assert.IsNull(await _service.GetForEditAsync(3));   // deleted
        Assert.IsNull(await _service.GetForEditAsync(999)); // missing
    }

    [Test]
    public async Task EditAsync_Updates_WhenActive()
    {
        var model = new AddSpaServiceViewModel
        {
            Name = "Massage+",
            Description = "Relax++",
            ProcedureDetails = "Full body + aroma",
            Price = 55,
            Duration = 75,
            ImageUrl = "m2.jpg"
        };

        await _service.EditAsync(1, model);
        _context.ChangeTracker.Clear();

        var s = await _context.SpaServices.SingleAsync(x => x.Id == 1);
        Assert.That(s.Name, Is.EqualTo("Massage+"));
        Assert.That(s.Description, Is.EqualTo("Relax++"));
        Assert.That(s.ProcedureDetails, Is.EqualTo("Full body + aroma"));
        Assert.That(s.Price, Is.EqualTo(55));
        Assert.That(s.Duration, Is.EqualTo(75));
        Assert.That(s.ImageUrl, Is.EqualTo("m2.jpg"));
    }

    [Test]
    public async Task EditAsync_DoesNothing_WhenDeletedOrMissing()
    {
        var before = await _context.SpaServices.AsNoTracking().SingleAsync(s => s.Id == 3);
        await _service.EditAsync(3, new AddSpaServiceViewModel { Name = "X", Description = "Y", ProcedureDetails = "Z", Price = 1, Duration = 1, ImageUrl = "i" });
        var after = await _context.SpaServices.AsNoTracking().SingleAsync(s => s.Id == 3);
        Assert.That(after.Name, Is.EqualTo(before.Name));

        var countBefore = await _context.SpaServices.CountAsync();
        await _service.EditAsync(999, new AddSpaServiceViewModel { Name = "X", Description = "Y", ProcedureDetails = "Z", Price = 1, Duration = 1, ImageUrl = "i" });
        var countAfter = await _context.SpaServices.CountAsync();
        Assert.That(countAfter, Is.EqualTo(countBefore));
    }

    [Test]
    public async Task GetForDeleteAsync_ReturnsModel_WhenActive()
    {
        var vm = await _service.GetForDeleteAsync(2);
        Assert.IsNotNull(vm);
        Assert.That(vm!.Id, Is.EqualTo(2));
        Assert.That(vm.Name, Is.EqualTo("Sauna"));
    }

    [Test]
    public async Task GetForDeleteAsync_ReturnsNull_WhenDeletedOrMissing()
    {
        Assert.IsNull(await _service.GetForDeleteAsync(3));   // deleted
        Assert.IsNull(await _service.GetForDeleteAsync(999)); // missing
    }

    [Test]
    public async Task DeleteAsync_SetsIsDeletedTrue_WhenExists()
    {
        await _service.DeleteAsync(2);
        var s = await _context.SpaServices.FindAsync(2);
        Assert.IsNotNull(s);
        Assert.IsTrue(s!.IsDeleted);
    }

    [Test]
    public async Task DeleteAsync_DoesNothing_WhenMissing()
    {
        var before = await _context.SpaServices.CountAsync();
        await _service.DeleteAsync(999);
        var after = await _context.SpaServices.CountAsync();
        Assert.That(after, Is.EqualTo(before));
    }

    [Test]
    public async Task GetSpaServicesCountAsync_AppliesFilters_IncludingDeleted_CurrentImplementation()
    {
        var countAll = await _service.GetSpaServicesCountAsync(null, null, null);
        Assert.That(countAll, Is.EqualTo(3));

        var countByName = await _service.GetSpaServicesCountAsync("a", null, null);
        Assert.That(countByName, Is.EqualTo(3));

        var countMin = await _service.GetSpaServicesCountAsync(null, 45, null);
        Assert.That(countMin, Is.EqualTo(2));

        var countMax = await _service.GetSpaServicesCountAsync(null, null, 40);
        Assert.That(countMax, Is.EqualTo(1));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}