using Microsoft.EntityFrameworkCore;
using SportComplexApp.Services.Data;
using System;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;

namespace SportComplexApp.Tests;

[TestFixture]
public class TournamentServiceTests
{
    private SportComplexDbContext _context = null!;
    private TournamentService _service = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<SportComplexDbContext>()
            .UseInMemoryDatabase(databaseName: $"TournamentDb_{Guid.NewGuid()}")
            .Options;

        _context = new SportComplexDbContext(options);

        _service = new TournamentService(_context);
        SeedData(_context);
    }

    private void SeedData(SportComplexDbContext context)
    {
        var football = new Sport { Id = 1, Name = "Football" };
        var tennis = new Sport { Id = 2, Name = "Tennis" };

        var tournaments = new List<Tournament>
        {
            new Tournament
            {
                Id = 1,
                Name = "Champions League",
                Description = "Top clubs",
                Sport = football,
                StartDate = new DateTime(2026, 1, 1),
                EndDate = new DateTime(2026, 6, 1),
                IsDeleted = false
            },
            new Tournament
            {
                Id = 2,
                Name = "Wimbledon",
                Description = "Grand Slam Tennis",
                Sport = tennis,
                StartDate = new DateTime(2026, 7, 1),
                EndDate = new DateTime(2026, 7, 15),
                IsDeleted = false
            },
            new Tournament
            {
                Id = 3,
                Name = "Local cup",
                Description = "Amateur football",
                Sport = football,
                StartDate = new DateTime(2026, 2, 1),
                EndDate = new DateTime(2026, 3, 1),
                IsDeleted = true
            },
            new Tournament
            {
                Id = 4,
                Name = "City Open",
                Description = "tennis indoors",
                Sport = tennis,
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 8, 15),
                IsDeleted = false
            }
        };


        context.Sports.AddRange(football, tennis);
        context.Tournaments.AddRange(tournaments);
        context.SaveChanges();
    }

    [Test]
    public async Task Returns_All_NotDeleted_When_NoFilters()
    {
        var result = await _service.GetAllAsync();

        var list = result.ToList();

        Assert.That(list.Count(), Is.EqualTo(3));
        Assert.That(list.All(t => !t.IsDeleted), Is.True);
        Assert.That(list.Any(t => t.Name == "Champions League"), Is.True);
        Assert.That(list.Any(t => t.Name == "Wimbledon"), Is.True);
        Assert.That(list.Any(t => t.Name == "City Open"), Is.True);
        Assert.That(list.Any(t => t.Name == "Local cup"), Is.False);

    }

    [Test]
    public async Task Filters_By_SearchQuery_In_Name()
    {
        var result = await _service.GetAllAsync(searchQuery: "Wimbledon");

        var list = result.ToList();

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list[0].Name, Is.EqualTo("Wimbledon"));
    }

    [Test]
    public async Task Filters_By_SearchQuery_In_Description()
    {
        var result = await _service.GetAllAsync(searchQuery: "tennis");
        var list = result.ToList();

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list[0].Name, Is.EqualTo("City Open"));
    }

    [Test]
    public async Task Filters_By_Sport_CaseInsensitive_And_Trim()
    {
        var result = await _service.GetAllAsync(sport: "  tEnNiS  ");
        var list = result.ToList();

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(list.All(x => x.Sport == "Tennis"));
    }

    [Test]
    public async Task Applies_Both_Filters_Search_And_Sport()
    {
        var result = await _service.GetAllAsync(searchQuery: "tennis", sport: "Tennis");
        var list = result.ToList();

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list[0].Name, Is.EqualTo("City Open"));
    }

    [Test]
    public async Task Returns_Empty_When_NoMatches()
    {
        var result = await _service.GetAllAsync(searchQuery: "no-such-text");
        var list = result.ToList();

        Assert.That(list, Is.Empty);
    }

    [Test]
    public async Task Deleted_Tournaments_Are_Excluded_Even_If_Matching()
    {
        var result = await _service.GetAllAsync(searchQuery: "football");
        var list = result.ToList();

        Assert.That(list, Is.Empty);
    }

    [Test]
    public async Task Maps_Sport_Name_In_ViewModel()
    {
        var result = (await _service.GetAllAsync(searchQuery: "Champions")).FirstOrDefault();

        Assert.IsNotNull(result);
        Assert.That(result!.Sport, Is.EqualTo("Football"));
        Assert.That(result.StartDate, Is.EqualTo(new DateTime(2026, 1, 1)));
        Assert.That(result.EndDate, Is.EqualTo(new DateTime(2026, 6, 1)));
        Assert.That(result.Description, Is.EqualTo("Top clubs"));
    }

    [Test]
    public async Task Sport_Filter_With_Only_Whitespace_Yields_NoResults()
    {
        var result = await _service.GetAllAsync(sport: "    ");
        var list = result.ToList();

        Assert.That(list, Is.Empty);
    }

    [Test]
    public async Task ReturnsNull_WhenIdNotFound()
    {
        var tournament = await _service.GetByIdAsync(999);

        Assert.IsNull(tournament);
    }

    [Test]
    public async Task ReturnsNull_WhenTournamentIsDeleted()
    {
        var tournament = await _service.GetByIdAsync(3);
        Assert.IsNull(tournament);
    }

    [Test]
    public async Task ReturnsMappedViewModel_WhenFoundAndNotDeleted()
    {
        var tournament = await _service.GetByIdAsync(1);

        Assert.IsNotNull(tournament);
        Assert.That(tournament!.Id, Is.EqualTo(1));
        Assert.That(tournament.Name, Is.EqualTo("Champions League"));
        Assert.That(tournament.Sport, Is.EqualTo("Football"));
        Assert.That(tournament.StartDate, Is.EqualTo(new DateTime(2026, 1, 1)));
        Assert.That(tournament.EndDate, Is.EqualTo(new DateTime(2026, 6, 1)));
        Assert.That(tournament.Description, Is.EqualTo("Top clubs"));
    }

    [Test]
    public async Task Registers_WhenNotAlreadyRegistered_AddsOneRow()
    {
        // Arrange
        int tournamentId = 1;
        string clientId = "clientId";

        //Act
        await _service.RegisterAsync(tournamentId, clientId);

        var registrations = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == tournamentId && r.ClientId == clientId)
            .ToListAsync();

        // Assert
        Assert.That(registrations.Count, Is.EqualTo(1));
        Assert.That(registrations[0].TournamentId, Is.EqualTo(tournamentId));
        Assert.That(registrations[0].ClientId, Is.EqualTo(clientId));
    }

    [Test]
    public async Task Register_IsIdempotent_CalledTwice_StillOneRow()
    {
        // Arrange
        int tournamentId = 1;
        string userId = "test-client-id";

        // Act
        await _service.RegisterAsync(tournamentId, userId);
        await _service.RegisterAsync(tournamentId, userId);

        var registrations = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == tournamentId && r.ClientId == userId)
            .ToListAsync();

        // Assert
        Assert.That(registrations.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task Register_DifferentUser_ProducesSeparateRow()
    {
        // Arrange
        int tournamentId = 1;
        string userA = "user-a";
        string userB = "user-b";

        // Act
        await _service.RegisterAsync(tournamentId, userA);
        await _service.RegisterAsync(tournamentId, userB);

        var t1All = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == tournamentId)
            .ToListAsync();

        // Assert
        Assert.That(t1All.Count, Is.EqualTo(2));
        Assert.That(t1All.Any(r => r.ClientId == userA));
        Assert.That(t1All.Any(r => r.ClientId == userB));
    }

    [Test]
    public async Task Register_SameUser_DifferentTournament_ProducesSeparateRow()
    {
        // Arrange
        string userId = "user-a";

        // Act
        await _service.RegisterAsync(1, userId);
        await _service.RegisterAsync(2, userId);

        var userRegs = await _context.TournamentRegistrations
            .Where(r => r.ClientId == userId)
            .OrderBy(r => r.TournamentId)
            .ToListAsync();

        // Assert
        Assert.That(userRegs.Count, Is.EqualTo(2));
        Assert.That(userRegs[0].TournamentId, Is.EqualTo(1));
        Assert.That(userRegs[1].TournamentId, Is.EqualTo(2));
    }

    [Test]
    public async Task Register_DoesNothing_WhenPreSeededRegistrationExists()
    {
        // Arrange
        int tournamentId = 1;
        string userId = "test-client-id";

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = tournamentId,
            ClientId = userId
        });
        await _context.SaveChangesAsync();

        // Act
        await _service.RegisterAsync(tournamentId, userId);

        var registrations = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == tournamentId && r.ClientId == userId)
            .ToListAsync();

        // Assert
        Assert.That(registrations.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task Unregister_RemovesRegistration_WhenExists_AndTournamentIsInFuture()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 1,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var ok = await _service.UnregisterAsync(1, UserId);

        var remaining = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == 1 && r.ClientId == UserId)
            .ToListAsync();

        // Assert
        Assert.IsTrue(ok);
        Assert.That(remaining, Is.Empty);
    }

    [Test]
    public async Task Unregister_ReturnsFalse_WhenRegistrationDoesNotExist()
    {
        // Arrange
        var UserId = "test-client-id";

        var ok = await _service.UnregisterAsync(1, UserId);

        var all = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == 1 && r.ClientId == UserId)
            .ToListAsync();

        // Assert
        Assert.IsFalse(ok);
        Assert.That(all, Is.Empty);
    }

    [Test]
    public async Task Unregister_ReturnsFalse_WhenTournamentAlreadyStarted()
    {
        // Arrange
        var UserId = "test-client-id";
        var t1 = await _context.Tournaments.FindAsync(1);
        t1!.StartDate = DateTime.Now.AddDays(-1);
        await _context.SaveChangesAsync();

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 1,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var ok = await _service.UnregisterAsync(1, UserId);

        var stillThere = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == 1 && r.ClientId == UserId)
            .ToListAsync();

        // Assert
        Assert.IsFalse(ok);
        Assert.That(stillThere.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task Unregister_DoesNotAffect_OtherUsersOrTournaments()
    {
        // Arrange
        var UserId = "test-client-id";

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 1,
            ClientId = "other-user"
        });
        await _context.SaveChangesAsync();

        // Act:
        var ok = await _service.UnregisterAsync(1, UserId);

        var all = await _context.TournamentRegistrations.ToListAsync();

        // Assert
        Assert.IsFalse(ok);
        Assert.That(all.Count, Is.EqualTo(1));
        Assert.That(all[0].TournamentId, Is.EqualTo(1));
        Assert.That(all[0].ClientId, Is.EqualTo("other-user"));
    }

    [Test]
    public async Task Unregister_Works_Even_IfTournamentIsDeleted_InSeed()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 3,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var ok = await _service.UnregisterAsync(3, UserId);

        var left = await _context.TournamentRegistrations
            .Where(r => r.TournamentId == 3 && r.ClientId == UserId)
            .ToListAsync();

        // Assert
        Assert.IsTrue(ok);
        Assert.That(left, Is.Empty);
    }

    [Test]
    public async Task Returns_OnlyFuture_NonDeleted_Tournaments_ForUser()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.AddRange(
            new TournamentRegistration { TournamentId = 1, ClientId = UserId },
            new TournamentRegistration { TournamentId = 3, ClientId = UserId },
            new TournamentRegistration { TournamentId = 4, ClientId = UserId }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = (await _service.GetUserTournamentsAsync(UserId)).ToList();

        // Assert:
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(x => x.Id == 1 && x.Name == "Champions League" && x.Sport == "Football"));
        Assert.That(result.Any(x => x.Id == 4 && x.Name == "City Open" && x.Sport == "Tennis"));
        Assert.That(result.All(x => x.Id != 3));
    }

    [Test]
    public async Task Removes_PastRegistrations_And_ExcludesThemFromResult()
    {
        // Arrange:
        var UserId = "test-client-id";
        var t1 = await _context.Tournaments.FindAsync(1);
        t1!.StartDate = DateTime.Now.AddDays(-2);
        await _context.SaveChangesAsync();

        _context.TournamentRegistrations.AddRange(
            new TournamentRegistration { TournamentId = 1, ClientId = UserId },
            new TournamentRegistration { TournamentId = 2, ClientId = UserId }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = (await _service.GetUserTournamentsAsync(UserId)).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(2));

        var still1 = await _context.TournamentRegistrations
            .CountAsync(r => r.TournamentId == 1 && r.ClientId == UserId);
        var still2 = await _context.TournamentRegistrations
            .CountAsync(r => r.TournamentId == 2 && r.ClientId == UserId);

        Assert.That(still1, Is.EqualTo(0));
        Assert.That(still2, Is.EqualTo(1));
    }

    [Test]
    public async Task Returns_Empty_When_UserHasNoRegistrations()
    {
        // Arrange
        var UserId = "test-client-id";
        // Act
        var result = (await _service.GetUserTournamentsAsync(UserId)).ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task Keeps_Registrations_To_Deleted_Tournaments_Untouched_And_ReturnsEmpty()
    {
        // Arrange:
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(
            new TournamentRegistration { TournamentId = 3, ClientId = UserId }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = (await _service.GetUserTournamentsAsync(UserId)).ToList();

        // Assert:
        Assert.That(result, Is.Empty);

        var regStillThere = await _context.TournamentRegistrations
            .CountAsync(r => r.TournamentId == 3 && r.ClientId == UserId);
        Assert.That(regStillThere, Is.EqualTo(1));
    }

    [Test]
    public async Task Maps_ViewModel_Fields_Correctly()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(
            new TournamentRegistration { TournamentId = 2, ClientId = UserId }
        );
        await _context.SaveChangesAsync();

        // Act
        var vm = (await _service.GetUserTournamentsAsync(UserId)).Single();

        // Assert
        Assert.That(vm.Id, Is.EqualTo(2));
        Assert.That(vm.Name, Is.EqualTo("Wimbledon"));
        Assert.That(vm.Sport, Is.EqualTo("Tennis"));
        Assert.That(vm.StartDate, Is.EqualTo(new DateTime(2026, 7, 1)));
        Assert.That(vm.EndDate, Is.EqualTo(new DateTime(2026, 7, 15)));
        Assert.That(vm.Description, Is.EqualTo("Grand Slam Tennis"));
    }

    [Test]
    public async Task ReturnsTrue_WhenUserHasRegistrationForTournament()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 1,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.IsUserRegisteredAsync(1, UserId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task ReturnsFalse_WhenUserHasNoRegistrationForTournament()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 2,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.IsUserRegisteredAsync(1, UserId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task ReturnsFalse_WhenOtherUserRegisteredForTournament()
    {
        // Arrange
        var UserId = "test-client-id";

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 1,
            ClientId = "other-user"
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.IsUserRegisteredAsync(1, UserId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task ReturnsTrue_EvenIfTournamentIsDeleted()
    {
        // Arrange
        var UserId = "test-client-id";
        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            TournamentId = 3,
            ClientId = UserId
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.IsUserRegisteredAsync(3, UserId);

        // Assert
        Assert.IsTrue(result);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}