using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using Moq;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data;
using SpaService = SportComplexApp.Data.Models.SpaService;

namespace SportComplexApp.Tests;

[TestFixture]
public class UserServiceTests
{
    private SportComplexDbContext _context = null!;
    private UserService _service = null!;
    private UserManager<Client> _userManager = null!;
    private RoleManager<IdentityRole> _roleManager = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<SportComplexDbContext>()
            .UseInMemoryDatabase(databaseName: $"UserDb_{Guid.NewGuid()}")
            .Options;

        _context = new SportComplexDbContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var userStore = new UserStore<Client>(_context);
        var roleStore = new RoleStore<IdentityRole>(_context);

        _userManager = new UserManager<Client>(
            userStore,
            null,
            new PasswordHasher<Client>(),
            new IUserValidator<Client>[0],
            new IPasswordValidator<Client>[0],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            null,
            null
        );

        _roleManager = new RoleManager<IdentityRole>(
            roleStore,
            new IRoleValidator<IdentityRole>[0],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            null
        );

        _service = new UserService(_userManager, _roleManager, _context);
    }

    private void SeedData_WithUsersAndRoles()
    {
        _context.Roles.AddRange(
            new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "role-member", Name = "Member", NormalizedName = "MEMBER" }
        );

        _context.Users.AddRange(
            new Client
            {
                Id = "user-1",
                UserName = "u1@test.com",
                NormalizedUserName = "U1@TEST.COM",
                Email = "u1@test.com",
                NormalizedEmail = "U1@TEST.COM",
                FirstName = "Alice",
                LastName = "Green"
            },
            new Client
            {
                Id = "user-2",
                UserName = "u2@test.com",
                NormalizedUserName = "U2@TEST.COM",
                Email = "u2@test.com",
                NormalizedEmail = "U2@TEST.COM",
                FirstName = "Bob",
                LastName = "Brown"
            }
        );

        _context.UserRoles.AddRange(
            new IdentityUserRole<string> { UserId = "user-1", RoleId = "role-admin" },
            new IdentityUserRole<string> { UserId = "user-1", RoleId = "role-member" },
            new IdentityUserRole<string> { UserId = "user-2", RoleId = "role-member" }
        );

        _context.SaveChanges();
    }

    private void SeedRelatedForUser(string userId)
    {
        if (!_context.Sports.Any())
            _context.Sports.Add(new Sport { Id = 1, Name = "Football", IsDeleted = false });

        if (!_context.SpaServices.Any())
            _context.SpaServices.Add(new SpaService { Id = 1, Name = "Massage", Duration = 60, Price = 50, ImageUrl = "img", IsDeleted = false });

        if (!_context.Tournaments.Any())
            _context.Tournaments.Add(new Tournament
            {
                Id = 1,
                Name = "Cup",
                Description = "d",
                StartDate = new DateTime(2026, 1, 1),
                EndDate = new DateTime(2026, 1, 2),
                SportId = 1,
                IsDeleted = false
            });

        _context.Reservations.Add(new Reservation
        {
            Id = 100,
            ClientId = userId,
            SportId = 1,
            ReservationDateTime = new DateTime(2025, 1, 1, 10, 0, 0),
            Duration = 60,
            NumberOfPeople = 1
        });

        _context.SpaReservations.Add(new SpaReservation
        {
            Id = 200,
            ClientId = userId,
            SpaServiceId = 1,
            ReservationDateTime = new DateTime(2025, 1, 2, 9, 0, 0),
            NumberOfPeople = 2
        });

        _context.TournamentRegistrations.Add(new TournamentRegistration
        {
            Id = 300,
            ClientId = userId,
            TournamentId = 1
        });

        _context.SaveChanges();
    }


    private void SeedData_UserWithoutRoles()
    {
        _context.Users.Add(new Client
        {
            Id = "solo",
            UserName = "solo@test.com",
            NormalizedUserName = "SOLO@TEST.COM",
            Email = "solo@test.com",
            NormalizedEmail = "SOLO@TEST.COM",
            FirstName = "Solo",
            LastName = "NoRole"
        });
        _context.SaveChanges();
    }

    private async Task EnsureTrainerRoleAsync()
    {
        if (!await _roleManager.RoleExistsAsync("Trainer"))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Trainer", NormalizedName = "TRAINER" });
        }
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsAllUsers_WithMappedFieldsAndRoles()
    {
        // Arrange
        _context.UserRoles.RemoveRange(_context.UserRoles);
        _context.Users.RemoveRange(_context.Users);
        _context.Roles.RemoveRange(_context.Roles);
        await _context.SaveChangesAsync();
        SeedData_WithUsersAndRoles();

        // Act
        var result = (await _service.GetAllUsersAsync()).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));

        var u1 = result.Single(x => x.Id == "user-1");
        Assert.That(u1.Email, Is.EqualTo("u1@test.com"));
        Assert.That(u1.FirstName, Is.EqualTo("Alice"));
        Assert.That(u1.LastName, Is.EqualTo("Green"));
        CollectionAssert.AreEquivalent(new[] { "Admin", "Member" }, u1.Roles);

        var u2 = result.Single(x => x.Id == "user-2");
        Assert.That(u2.Email, Is.EqualTo("u2@test.com"));
        Assert.That(u2.FirstName, Is.EqualTo("Bob"));
        Assert.That(u2.LastName, Is.EqualTo("Brown"));
        CollectionAssert.AreEquivalent(new[] { "Member" }, u2.Roles);
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsEmpty_WhenNoUsers()
    {
        // Arrange
        _context.UserRoles.RemoveRange(_context.UserRoles);
        _context.Users.RemoveRange(_context.Users);
        _context.Roles.RemoveRange(_context.Roles);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllUsersAsync();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllUsersAsync_HandlesUserWithNoRoles()
    {
        // Arrange
        _context.UserRoles.RemoveRange(_context.UserRoles);
        _context.Users.RemoveRange(_context.Users);
        _context.Roles.RemoveRange(_context.Roles);
        await _context.SaveChangesAsync();

        SeedData_UserWithoutRoles();

        // Act
        var result = (await _service.GetAllUsersAsync()).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        var vm = result[0];
        Assert.That(vm.Id, Is.EqualTo("solo"));
        Assert.That(vm.Email, Is.EqualTo("solo@test.com"));
        Assert.That(vm.FirstName, Is.EqualTo("Solo"));
        Assert.That(vm.LastName, Is.EqualTo("NoRole"));
        Assert.That(vm.Roles, Is.Empty);
    }

    [Test]
    public async Task UserExistsByIdAsync_ReturnsTrue_WhenUserExists()
    {
        // Arrange
        SeedData_WithUsersAndRoles();

        // Act
        var exists = await _service.UserExistsByIdAsync("user-1");

        // Assert
        Assert.IsTrue(exists);
    }

    [Test]
    public async Task UserExistsByIdAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        // Arrange
        SeedData_WithUsersAndRoles();

        // Act
        var exists = await _service.UserExistsByIdAsync("non-existent-id");

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task UserExistsByIdAsync_ReturnsFalse_WhenNoUsersInDb()
    {
        // Arrange

        // Act
        var exists = await _service.UserExistsByIdAsync("user-1");

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task AssignUserToRoleAsync_AddsRole_WhenUserAndRoleExist_AndUserNotInRole()
    {
        // Arrange
        SeedData_WithUsersAndRoles();

        // Act
        var ok = await _service.AssignUserToRoleAsync("user-2", "Admin");

        // Assert
        Assert.IsTrue(ok);
        var user2 = await _userManager.FindByIdAsync("user-2");
        Assert.IsTrue(await _userManager.IsInRoleAsync(user2!, "Admin"));
    }

    [Test]
    public async Task AssignUserToRoleAsync_ReturnsTrue_WhenAlreadyInRole_NoDuplicate()
    {
        // Arrange
        SeedData_WithUsersAndRoles();

        // Act
        var ok = await _service.AssignUserToRoleAsync("user-1", "Member");

        // Assert
        Assert.IsTrue(ok);
        var memberRoleId = (await _context.Roles.SingleAsync(r => r.Name == "Member")).Id;
        var links = await _context.UserRoles.CountAsync(ur => ur.UserId == "user-1" && ur.RoleId == memberRoleId);
        Assert.That(links, Is.EqualTo(1));
    }

    [Test]
    public async Task AssignUserToRoleAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.AssignUserToRoleAsync("missing-user", "Admin");

        Assert.IsFalse(ok);
    }

    [Test]
    public async Task AssignUserToRoleAsync_ReturnsFalse_WhenRoleDoesNotExist()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.AssignUserToRoleAsync("user-1", "GhostRole");

        Assert.IsFalse(ok);
    }

    [Test]
    public async Task AssignUserToRoleAsync_CreatesTrainerEntity_WhenRoleTrainer_AndNoTrainerExists()
    {
        // Arrange
        SeedData_WithUsersAndRoles();
        await EnsureTrainerRoleAsync();

        Assert.That(await _context.Trainers.AnyAsync(t => t.ClientId == "user-1"), Is.False);

        // Act
        var ok = await _service.AssignUserToRoleAsync("user-1", "Trainer");

        // Assert
        Assert.IsTrue(ok);

        var u1 = await _userManager.FindByIdAsync("user-1");
        Assert.IsTrue(await _userManager.IsInRoleAsync(u1!, "Trainer"));

        var trainer = await _context.Trainers.SingleOrDefaultAsync(t => t.ClientId == "user-1" && !t.IsDeleted);
        Assert.IsNotNull(trainer);
        Assert.That(trainer!.Name, Is.EqualTo("Alice"));
        Assert.That(trainer.LastName, Is.EqualTo("Green"));
    }

    [Test]
    public async Task AssignUserToRoleAsync_DoesNotDuplicateTrainer_WhenTrainerAlreadyExists()
    {
        // Arrange
        SeedData_WithUsersAndRoles();
        await EnsureTrainerRoleAsync();

        _context.Trainers.Add(new Trainer
        {
            Name = "Bob",
            LastName = "Brown",
            ClientId = "user-2",
            IsDeleted = false
        });
        await _context.SaveChangesAsync();

        var trainerRoleId = (await _context.Roles.SingleAsync(r => r.Name == "Trainer")).Id;
        _context.UserRoles.Add(new IdentityUserRole<string> { UserId = "user-2", RoleId = trainerRoleId });
        await _context.SaveChangesAsync();

        // Act
        var ok = await _service.AssignUserToRoleAsync("user-2", "Trainer");

        // Assert
        Assert.IsTrue(ok);
        var trainerCount = await _context.Trainers.CountAsync(t => t.ClientId == "user-2" && !t.IsDeleted);
        Assert.That(trainerCount, Is.EqualTo(1));
    }

    [Test]
    public async Task AssignUserToRoleAsync_DoesNotCreateTrainer_ForNonTrainerRole()
    {
        // Arrange
        SeedData_WithUsersAndRoles();

        // Act
        var ok = await _service.AssignUserToRoleAsync("user-1", "Admin");

        // Assert
        Assert.IsTrue(ok);
        Assert.That(await _context.Trainers.AnyAsync(t => t.ClientId == "user-1" && !t.IsDeleted), Is.False);
    }

    [Test]
    public async Task AssignUserToRoleAsync_WorksWithUserWithoutRoles_AndCreatesTrainerIfTrainerRole()
    {
        // Arrange
        SeedData_UserWithoutRoles();
        await EnsureTrainerRoleAsync();

        // Act
        var ok = await _service.AssignUserToRoleAsync("solo", "Trainer");

        // Assert
        Assert.IsTrue(ok);
        var solo = await _userManager.FindByIdAsync("solo");
        Assert.IsTrue(await _userManager.IsInRoleAsync(solo!, "Trainer"));

        var trainer = await _context.Trainers.SingleOrDefaultAsync(t => t.ClientId == "solo" && !t.IsDeleted);
        Assert.IsNotNull(trainer);
        Assert.That(trainer!.Name, Is.EqualTo("Solo"));
        Assert.That(trainer.LastName, Is.EqualTo("NoRole"));
    }

    [Test]
    public async Task AssignUserToRoleAsync_ReturnsFalse_WhenAddToRoleFails()
    {
        // Arrange
        var user = new Client
        {
            Id = "user-x",
            UserName = "x@test.com",
            NormalizedUserName = "X@TEST.COM",
            Email = "x@test.com",
            NormalizedEmail = "X@TEST.COM",
            FirstName = "X",
            LastName = "Y"
        };

        var userStoreMock = new Mock<IUserStore<Client>>();
        var userManagerMock = new Mock<UserManager<Client>>(
            userStoreMock.Object,
            null, null, null, null, null, null, null, null
        );

        var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
        var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            roleStoreMock.Object,
            new IRoleValidator<IdentityRole>[0],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            null
        );

        userManagerMock.Setup(m => m.FindByIdAsync("user-x")).ReturnsAsync(user);
        roleManagerMock.Setup(r => r.RoleExistsAsync("Admin")).ReturnsAsync(true);
        userManagerMock.Setup(m => m.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);
        userManagerMock.Setup(m => m.AddToRoleAsync(user, "Admin"))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "forced failure" }));

        var svc = new UserService(userManagerMock.Object, roleManagerMock.Object, _context);

        // Act
        var ok = await svc.AssignUserToRoleAsync("user-x", "Admin");

        // Assert
        Assert.IsFalse(ok);
        userManagerMock.Verify(m => m.AddToRoleAsync(user, "Admin"), Times.Once);
    }

    [Test]
    public async Task RemoveUserRoleAsync_RemovesRole_WhenUserIsInRole()
    {
        // Arrange
        SeedData_WithUsersAndRoles();
        var user = await _userManager.FindByIdAsync("user-1");
        Assert.IsTrue(await _userManager.IsInRoleAsync(user!, "Admin"));

        // Act
        var ok = await _service.RemoveUserRoleAsync("user-1", "Admin");

        // Assert
        Assert.IsTrue(ok);
        user = await _userManager.FindByIdAsync("user-1");
        Assert.IsFalse(await _userManager.IsInRoleAsync(user!, "Admin"));
    }

    [Test]
    public async Task RemoveUserRoleAsync_ReturnsTrue_WhenUserNotInRole_NoOp()
    {
        // Arrange
        SeedData_WithUsersAndRoles();
        var user = await _userManager.FindByIdAsync("user-2");
        Assert.IsFalse(await _userManager.IsInRoleAsync(user!, "Admin"));

        // Act
        var ok = await _service.RemoveUserRoleAsync("user-2", "Admin");

        // Assert
        Assert.IsTrue(ok);
        Assert.IsFalse(await _userManager.IsInRoleAsync(user!, "Admin"));
    }

    [Test]
    public async Task RemoveUserRoleAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.RemoveUserRoleAsync("missing-user", "Admin");

        Assert.IsFalse(ok);
    }

    [Test]
    public async Task RemoveUserRoleAsync_ReturnsFalse_WhenRoleDoesNotExist()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.RemoveUserRoleAsync("user-1", "GhostRole");

        Assert.IsFalse(ok);
    }

    [Test]
    public async Task RemoveUserRoleAsync_ReturnsFalse_WhenRemoveFromRoleFails()
    {
        // Arrange
        var user = new Client
        {
            Id = "u1",
            UserName = "u1@test.com",
            NormalizedUserName = "U1@TEST.COM",
            Email = "u1@test.com",
            NormalizedEmail = "U1@TEST.COM",
            FirstName = "Test",
            LastName = "User"
        };

        var userStoreMock = new Moq.Mock<IUserStore<Client>>();
        var userManagerMock = new Moq.Mock<UserManager<Client>>(
            userStoreMock.Object,
            null, null, null, null, null, null, null, null
        );

        var roleStoreMock = new Moq.Mock<IRoleStore<IdentityRole>>();
        var roleManagerMock = new Moq.Mock<RoleManager<IdentityRole>>(
            roleStoreMock.Object,
            new IRoleValidator<IdentityRole>[0],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            null
        );

        roleManagerMock.Setup(r => r.RoleExistsAsync("Member")).ReturnsAsync(true);
        userManagerMock.Setup(m => m.FindByIdAsync("u1")).ReturnsAsync(user);
        userManagerMock.Setup(m => m.IsInRoleAsync(user, "Member")).ReturnsAsync(true);
        userManagerMock.Setup(m => m.RemoveFromRoleAsync(user, "Member"))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "forced failure" }));

        var svc = new UserService(userManagerMock.Object, roleManagerMock.Object, _context);

        // Act
        var ok = await svc.RemoveUserRoleAsync("u1", "Member");

        // Assert
        Assert.IsFalse(ok);
        userManagerMock.Verify(m => m.RemoveFromRoleAsync(user, "Member"), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_RemovesUserAndRelatedData_ReturnsTrue()
    {
        // Arrange
        SeedData_WithUsersAndRoles();
        SeedRelatedForUser("user-1");

        // sanity
        Assert.That(await _context.Users.AsNoTracking().AnyAsync(u => u.Id == "user-1"), Is.True);
        Assert.That(await _context.Reservations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.True);
        Assert.That(await _context.SpaReservations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.True);
        Assert.That(await _context.TournamentRegistrations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.True);

        // Act
        var ok = await _service.DeleteUserAsync("user-1");

        // Assert
        Assert.IsTrue(ok);

        _context.ChangeTracker.Clear();

        Assert.That(await _context.Users.AsNoTracking().AnyAsync(u => u.Id == "user-1"), Is.False);
        Assert.That(await _context.Reservations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.False);
        Assert.That(await _context.SpaReservations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.False);
        Assert.That(await _context.TournamentRegistrations.AsNoTracking().AnyAsync(r => r.ClientId == "user-1"), Is.False);

        Assert.That(await _context.Users.AsNoTracking().AnyAsync(u => u.Id == "user-2"), Is.True);
    }



    [Test]
    public async Task DeleteUserAsync_UserWithoutRelatedData_ReturnsTrue()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.DeleteUserAsync("user-2");

        Assert.IsTrue(ok);
        Assert.That(await _context.Users.AnyAsync(u => u.Id == "user-2"), Is.False);
    }

    [Test]
    public async Task DeleteUserAsync_ReturnsFalse_WhenUserNotFound()
    {
        SeedData_WithUsersAndRoles();

        var ok = await _service.DeleteUserAsync("missing-id");

        Assert.IsFalse(ok);
    }

    [Test]
    public async Task DeleteUserAsync_ReturnsFalse_WhenUserManagerDeleteFails()
    {
        SeedData_WithUsersAndRoles();
        var user = await _context.Users.FirstAsync(u => u.Id == "user-1");

        var userStoreMock = new Mock<IUserStore<Client>>();
        var userManagerMock = new Mock<UserManager<Client>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );
        userManagerMock.Setup(m => m.FindByIdAsync("user-1")).ReturnsAsync(user);
        userManagerMock.Setup(m => m.DeleteAsync(user))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "forced fail" }));

        var roleStore = new RoleStore<IdentityRole>(_context);
        var roleManager = new RoleManager<IdentityRole>(
            roleStore, new IRoleValidator<IdentityRole>[0],
            new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null
        );

        var svc = new UserService(userManagerMock.Object, roleManager, _context);

        var ok = await svc.DeleteUserAsync("user-1");

        Assert.IsFalse(ok);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _userManager.Dispose();
        _roleManager.Dispose();
    }
}