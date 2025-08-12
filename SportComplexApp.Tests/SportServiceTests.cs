using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services;
using SportComplexApp.Web.ViewModels.Sport;
using static SportComplexApp.Common.ErrorMessages.Reservation;
using static SportComplexApp.Common.ApplicationConstants;
using SportComplexApp.Common;
using SportComplexApp.Web.ViewModels.Spa;

namespace SportComplexApp.Tests
{
    [TestFixture]
    public class SportServiceTests
    {
        private SportComplexDbContext _context = null!;
        private SportService _service = null!;
        private FakeTimeProvider _fakeTime = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SportComplexDbContext>()
                .UseInMemoryDatabase(databaseName: $"SportDb_{Guid.NewGuid()}")
                .Options;

            _fakeTime = new FakeTimeProvider(new DateTimeOffset(2025, 8, 5, 10, 0, 0, TimeSpan.Zero));

            _context = new SportComplexDbContext(options);

            SeedData(_context);

            _service = new SportService(_context, _fakeTime);
        }

        private void SeedData(SportComplexDbContext context)
        {
            var facilities = new List<Facility>
            {
                new Facility { Id = 1, Name = "Main Hall", IsDeleted = false },
                new Facility { Id = 2, Name = "Old Hall", IsDeleted = true }
            };
            context.Facilities.AddRange(facilities);

            var client1 = new Client
                { Id = "U1", FirstName = "John", LastName = "Doe", Email = "john@ex.com", UserName = "john@ex.com" };
            var client2 = new Client
                { Id = "U2", FirstName = "Jane", LastName = "Smith", Email = "jane@ex.com", UserName = "jane@ex.com" };
            context.Users.AddRange(client1, client2);

            var trainer1 = new Trainer { Id = 1, Name = "John", LastName = "Doe", ClientId = "U1", IsDeleted = false };
            var trainer2 = new Trainer { Id = 2, Name = "Jane", LastName = "Smith", ClientId = "U2", IsDeleted = false };
            context.Trainers.AddRange(trainer1, trainer2);

            var sports = new List<Sport>
            {
                new Sport()
                {
                    Id = 1, Name = "Tennis", Duration = 60, Price = 20, ImageUrl = "tennis.jpg",
                    FacilityId = 1, Facility = facilities[0], IsDeleted = false, MinPeople = 1, MaxPeople = 4
                },
                new Sport()
                {
                    Id = 2, Name = "Football", Duration = 90, Price = 30, ImageUrl = "football.jpg",
                    FacilityId = 1, Facility = facilities[0], IsDeleted = false, MinPeople = 2, MaxPeople = 22
                },
                new Sport()
                {
                    Id = 3, Name = "Basketball", Duration = 60, Price = 25, ImageUrl = "basketball.jpg",
                    FacilityId = 2, Facility = facilities[1], IsDeleted = false, MinPeople = 2, MaxPeople = 10
                },
                new Sport()
                {
                    Id = 4, Name = "Swimming", Duration = 30, Price = 15, ImageUrl = "swimming.jpg",
                    FacilityId = 1, Facility = facilities[0], IsDeleted = true, MinPeople = 1, MaxPeople = 5
                }
            };

            context.Reservations.AddRange(
                new Reservation
                {
                    Id = 100,
                    ClientId = "U1",
                    SportId = 1, // Tennis
                    TrainerId = null, // No trainer
                    ReservationDateTime = new DateTime(2025, 8, 10, 10, 0, 0),
                    Duration = 60,
                    NumberOfPeople = 2
                },
                new Reservation
                {
                    Id = 101,
                    ClientId = "U1",
                    SportId = 2, // Football
                    TrainerId = 1, // John Doe
                    ReservationDateTime = new DateTime(2025, 8, 12, 9, 0, 0),
                    Duration = 90,
                    NumberOfPeople = 3,
                },
                new Reservation()
                {
                    Id = 102,
                    ClientId = "U2",
                    SportId = 1, // Tennis
                    TrainerId = 2, // Jane Smith
                    ReservationDateTime = new DateTime(2025, 8, 12, 9, 0, 0),
                    Duration = 60,
                    NumberOfPeople = 1
                });


            context.Sports.AddRange(sports);

            context.SportTrainers.Add(new SportTrainer { SportId = 1, TrainerId = 1 });
            context.SportTrainers.Add(new SportTrainer { SportId = 1, TrainerId = 2 });

            context.SaveChanges();
        }

        private SportReservationFormViewModel CreateBaseModel(DateTime dateTime)
        {
            return new SportReservationFormViewModel()
            {
                SportId = 1,
                Duration = 60,
                NumberOfPeople = 2,
                ReservationDateTime = dateTime,
            };
        }

        [Test]
        public async Task GetAllSportsAsync_ShouldReturnOnlyNotDeletedAndNotDeletedFacility()
        {
            var result = await _service.GetAllSportsAsync();

            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(2)); // Only Tennis and Football should be returned
            Assert.That(list.Any(s => s.Name == "Tennis"), Is.True);
            Assert.That(list.Any(s => s.Name == "Football"), Is.True);
            Assert.That(list.Any(s => s.Name == "Basketball"), Is.False); // facility is deleted
            Assert.That(list.Any(s => s.Name == "Swimming"), Is.False); // sport is deleted
        }

        [Test]
        public async Task GetAllSportsAsync_FiltersBySearchQuery()
        {
            var result = await _service.GetAllSportsAsync("Ten");

            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(1)); // Only Tennis should be returned
            Assert.That(list[0].Name, Is.EqualTo("Tennis"));
        }

        [Test]
        public async Task GetAllSportsAsync_FiltersByMinDuration()
        {
            var result = await _service.GetAllSportsAsync(null, 70);

            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(1)); // Only Football should be returned
            Assert.That(list[0].Name, Is.EqualTo("Football"));
        }

        [Test]
        public async Task GetAllSportsAsync_FiltersByMaxDuration()
        {
            var result = await _service.GetAllSportsAsync(null, null, 60);

            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(1)); // Tennis should be returned
            Assert.That(list[0].Name, Is.EqualTo("Tennis"));
        }

        [Test]
        public async Task GetAllSportsAsync_FiltersByMinAndMaxDuration()
        {
            var result = await _service.GetAllSportsAsync(null, 40, 70);

            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(1)); // Tennis should be returned
            Assert.That(list[0].Name, Is.EqualTo("Tennis"));
        }

        [Test]
        public async Task GetAllSportsAsync_ReturnsProjectsToViewModelCorrectly()
        {
            var result = await _service.GetAllSportsAsync();
            var tennis = result.First(x => x.Name == "Tennis");

            Assert.Multiple(() =>
            {
                Assert.That(tennis.Facility, Is.EqualTo("Main Hall"));
                Assert.That(tennis.ImageUrl, Is.EqualTo("tennis.jpg"));
                Assert.That(tennis.Duration, Is.EqualTo(60));
                Assert.That(tennis.Price, Is.EqualTo(20));
            });
        }

        [Test]
        public async Task GetAllForHomeAsync_ReturnsOnlyActiveSports_WithProjection()
        {
            var list = (await _service.GetAllForHomeAsync()).ToList();
                
            CollectionAssert.AreEquivalent(new[] { 1, 2, 3 }, list.Select(x => x.Id));
            Assert.IsTrue(list.Any(x => x.Name == "Tennis" && x.ImageUrl == "tennis.jpg"));
            Assert.IsTrue(list.Any(x => x.Name == "Football" && x.ImageUrl == "football.jpg"));
            Assert.IsTrue(list.Any(x => x.Name == "Basketball" && x.ImageUrl == "basketball.jpg"));
        }

        [Test]
        public async Task GetReservationFormAsync_ShouldReturnsNull_WhenSportNotFound()
        {
            var result = await _service.GetReservationFormAsync(999, null);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReservationFormAsync_ShouldReturnsModelWithMappedFieldAndAllTrainers_WhenNoCurrentUser()
        {
            var result = await _service.GetReservationFormAsync(1, null);

            Assert.IsNotNull(result);
            Assert.That(result!.SportId, Is.EqualTo(1));
            Assert.That(result.SportName, Is.EqualTo("Tennis"));
            Assert.That(result.FacilityName, Is.EqualTo("Main Hall"));
            Assert.That(result.Duration, Is.EqualTo(60));
            Assert.That(result.MinDuration, Is.EqualTo(60));
            Assert.That(result.MaxDuration, Is.EqualTo(120));
            Assert.That(result.MinPeople, Is.EqualTo(1));
            Assert.That(result.MaxPeople, Is.EqualTo(4));

            Assert.That(result.Trainers.Count(), Is.EqualTo(2));
            var ids = result.Trainers.Select(x => x.Id).OrderBy(x => x).ToList();
            Assert.That(ids, Is.EqualTo(new List<int> { 1, 2 })); // Trainers should be sorted by Id
        }

        [Test]
        public async Task GetReservationFormAsync_WhenCurrentUserIsTrainerForThatSport()
        {
            var result = await _service.GetReservationFormAsync(1, "U1");

            Assert.IsNotNull(result);
            Assert.That(result!.Trainers.Count(), Is.EqualTo(1));
            Assert.That(result.Trainers.Single().Id, Is.EqualTo(2)); // Only trainer with Id 2 is available for Tennis
        }

        [Test]
        public async Task GetReservationFormAsync_WhenCurrentUserNotTrainerOrNotLinked()
        {
            var result = await _service.GetReservationFormAsync(1, "U3");

            Assert.IsNotNull(result);
            Assert.That(result!.Trainers.Count(), Is.EqualTo(2)); // All trainers should be available
        }

        [Test]
        public void CreateReservationAsync_ThrowsWhenOutsideWorkingHours()
        {
            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddHours(11));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => 
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationOutsideWorkingHours));
        }

        [Test]
        public void CreateReservationAsync_ThrowsWhenTooFarInFuture()
        {
            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddDays(MaxReservationDaysInAdvance + 1));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));
            Assert.That(ex!.Message, Is.EqualTo(ReservationTooFarInFuture));
        }

        [Test]
        public void CreateReservationAsync_ThrowsWhenTooSoon()
        {
            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddMinutes(30));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));
            Assert.That(ex!.Message, Is.EqualTo(ReservationTooSoon));
        }

        [Test]
        public void CreateReservationAsync_ThrowsWhenInPast()
        {
            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddHours(-1));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationInPast));
        }

        [Test]
        public async Task CreateReservationAsync_ThrowsWhenClientAlreadyHasReservationAtThisTime()
        {
            _context.Reservations.Add(new Reservation
            {
                Id = 1,
                SportId = 1,
                ClientId = "U1",
                ReservationDateTime = _fakeTime.GetLocalNow().DateTime.AddHours(2),
                Duration = 60
            });
            await _context.SaveChangesAsync();

            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddHours(2)); // same time as existing reservation
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationConflict));
        }

        [Test]
        public async Task CreateReservationAsync_ThrowsWhenTrainerBusy()
        {
            _context.Reservations.Add(new Reservation()
            {
                Id = 1,
                SportId = 1,
                TrainerId = 1,
                ClientId = "U2",
                ReservationDateTime = _fakeTime.GetLocalNow().DateTime.AddHours(2),
                Duration = 60
            });
            await _context.SaveChangesAsync();

            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddHours(2));
            model.TrainerId = 1; // Trainer 1 is busy at this time

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));
            Assert.That(ex!.Message, Is.EqualTo(TrainerBusy));
        }

        [Test]
        public async Task CreateReservationAsync_WhenValid()
        {
            var model = CreateBaseModel(_fakeTime.GetLocalNow().DateTime.AddHours(3));

            var id = await _service.CreateReservationAsync(model, "U1");

            var saved = await _context.Reservations.FindAsync(id);
            Assert.IsNotNull(saved);
            Assert.That(saved.ClientId, Is.EqualTo("U1"));
        }

        [Test]
        public void CreateReservationAsync_Throws_WhenSportOverlapExists()
        {
            var existingReservationStart = new DateTime(2025, 8, 12, 9, 0, 0);
            var overlappingTime = existingReservationStart.AddMinutes(30);

            var model = new SportReservationFormViewModel
            {
                SportId = 1,
                SportName = "Tennis",
                FacilityName = "Main Hall",
                ReservationDateTime = overlappingTime,
                Duration = 60,
                NumberOfPeople = 2,
                TrainerId = 1
            };

            // Act + Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationConflict));
        }

        [Test]
        public async Task CreateReservationAsync_Throws_WhenSpaOverlapExists()
        {
            var baseDate = _fakeTime.GetLocalNow().DateTime.Date.AddDays(1);
            var sportStart = baseDate.AddHours(10).AddMinutes(30);
            var spaStart = baseDate.AddHours(10).AddMinutes(15);

            _context.SpaServices.Add(new SportComplexApp.Data.Models.SpaService
            {
                Id = 99,
                Name = "Test SPA",
                Description = "x",
                ProcedureDetails = "y",
                Duration = 60,
                Price = 10,
                ImageUrl = "img",
                IsDeleted = false
            });
            _context.SpaReservations.Add(new SpaReservation
            {
                Id = 9001,
                ClientId = "U1",
                SpaServiceId = 99,
                ReservationDateTime = spaStart,
                NumberOfPeople = 1
            });
            _context.SaveChanges();

            var model = CreateBaseModel(sportStart);

            // Act + Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationConflict));
        }

        [Test]
        public async Task CreateReservationAsync_Succeeds_WhenSpaEndsExactlyAtSportStart()
        {
            // Arrange
            var baseDate = _fakeTime.GetLocalNow().DateTime.Date.AddDays(1);
            var sportStart = baseDate.AddHours(11);
            var spaStart = baseDate.AddHours(10);

            _context.SpaServices.Add(new SportComplexApp.Data.Models.SpaService
            {
                Id = 98,
                Name = "SPA NoOverlap",
                Description = "x",
                ProcedureDetails = "y",
                Duration = 60,
                Price = 10,
                ImageUrl = "img",
                IsDeleted = false
            });
            _context.SpaReservations.Add(new SpaReservation
            {
                Id = 9002,
                ClientId = "U1",
                SpaServiceId = 98,
                ReservationDateTime = spaStart,
                NumberOfPeople = 1
            });
            _context.SaveChanges();

            var model = CreateBaseModel(sportStart);

            // Act
            var id = await _service.CreateReservationAsync(model, "U1");

            // Assert
            var saved = await _context.Reservations.FindAsync(id);
            Assert.IsNotNull(saved);
            Assert.That(saved!.ReservationDateTime, Is.EqualTo(sportStart));
        }

        [Test]
        public async Task CreateReservationAsync_Throws_WhenCurrentUserIsTrainerAndHasOverlap()
        {
            // Arrange
            var start = _fakeTime.GetLocalNow().DateTime.AddHours(2);

            _context.Reservations.Add(new Reservation
            {
                Id = 777,
                ClientId = "U2",
                SportId = 1,
                TrainerId = 1,
                ReservationDateTime = start,
                Duration = 90,
                NumberOfPeople = 1
            });
            await _context.SaveChangesAsync();

            var model = new SportReservationFormViewModel
            {
                SportId = 1,
                ReservationDateTime = start.AddMinutes(30),
                Duration = 60,
                NumberOfPeople = 2,
                TrainerId = null
            };

            // Act + Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateReservationAsync(model, "U1"));

            Assert.That(ex!.Message, Is.EqualTo(ReservationConflict));
        }

        [Test]
        public async Task CreateReservationAsync_Succeeds_WhenTrainerProvidedAndFree()
        {
            var baseDate = _fakeTime.GetLocalNow().DateTime.Date.AddDays(1);
            var start = baseDate.AddHours(11);

            var model = CreateBaseModel(start);
            model.TrainerId = 1;

            // Act
            var id = await _service.CreateReservationAsync(model, "U1");

            // Assert
            var saved = await _context.Reservations.FindAsync(id);
            Assert.IsNotNull(saved);
            Assert.That(saved!.ReservationDateTime, Is.EqualTo(start));
            Assert.That(saved.TrainerId, Is.EqualTo(1));
        }

        [Test]
        public async Task GetUserReservationsAsync_ShouldReturnsEmpty_WhenUserHasNoReservations()
        {
            var result = await _service.GetUserReservationsAsync("U6");
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetUserReservationsAsync_ShouldReturnsUserReservations()
        {
            var result = (await _service.GetUserReservationsAsync("U1")).ToList();

            Assert.That(result.Count, Is.EqualTo(2)); // U1 has 2 reservations
            Assert.That(result.Any(r => r.Id == 100), Is.True);
            Assert.That(result.Any(r => r.Id == 101), Is.True);
            Assert.That(result.Any(r => r.Id == 102), Is.False); // U2's reservation should not be included
        }

        [Test]
        public async Task GetUserReservationsAsync_ShouldReturnsUserReservationWithMapsField_NoTrainerAndComputesTotalPrice()
        {
            var res = (await _service.GetUserReservationsAsync("U1")).First(r => r.Id == 100);

            Assert.That(res.SportName, Is.EqualTo("Tennis"));
            Assert.That(res.FacilityName, Is.EqualTo("Main Hall"));
            Assert.That(res.Duration, Is.EqualTo(60));
            Assert.That(res.NumberOfPeople, Is.EqualTo(2));
            Assert.That(res.TrainerName, Is.EqualTo("No Trainer"));
            Assert.That(res.TotalPrice, Is.EqualTo(40)); // 20 per person * 2 people
        }

        [Test]
        public async Task GetUserReservationsAsync_ShouldReturnsUserReservationWithMapsField_WithTrainerAndComputesTotalPrice()
        {
            var res = (await _service.GetUserReservationsAsync("U1")).First(r => r.Id == 101);

            Assert.That(res.SportName, Is.EqualTo("Football"));
            Assert.That(res.FacilityName, Is.EqualTo("Main Hall"));
            Assert.That(res.Duration, Is.EqualTo(90));
            Assert.That(res.NumberOfPeople, Is.EqualTo(3));
            Assert.That(res.TrainerName, Is.EqualTo("John Doe"));
            Assert.That(res.TotalPrice, Is.EqualTo(90));
        }

        [Test]
        public async Task ReservationExistsAsync_ShouldReturnsTrue_WhenReservationWithIdBelongsToUser()
        {
            var exists = await _service.ReservationExistsAsync(101, "U1");
            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task ReservationExistsAsync_ShouldReturnsFalse_WhenReservationWithIdDoesNotBelongToUser()
        {
            var exists = await _service.ReservationExistsAsync(101, "U2");
            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task ReservationExistsAsync_ShouldReturnsFalse_WhenReservationWithIdDoesNotExist()
        {
            var exists = await _service.ReservationExistsAsync(999, "U1");
            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task CancelReservationAsync_ShouldCancelReservation_WhenReservationBelongsToUser()
        {
            var before = await _context.Reservations.CountAsync();

            await _service.CancelReservationAsync(101, "U1");

            var after = await _context.Reservations.CountAsync();
            Assert.That(after, Is.EqualTo(before - 1)); // One reservation should be removed
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 101), Is.False);
        }

        [Test]
        public async Task CancelReservationAsync_ShouldDoesNothing_WhenReservationDoesNotExist()
        {
            var before = await _context.Reservations.CountAsync();

            await _service.CancelReservationAsync(999, "U1");

            var after = await _context.Reservations.CountAsync();
            Assert.That(after, Is.EqualTo(before)); // No reservations should be removed
        }

        [Test]
        public async Task DeleteExpiredReservationsAsync_RemovesExpired_ForUser_IncludingBoundaryEndEqualsNow()
        {
            // Arrange
            var now = _fakeTime.GetLocalNow().DateTime;

            _context.Reservations.AddRange(
                new Reservation
                {
                    Id = 200,
                    ClientId = "U1",
                    SportId = 1,
                    TrainerId = null,
                    ReservationDateTime = now.AddMinutes(-60),
                    Duration = 60,
                    NumberOfPeople = 1
                },
                new Reservation
                {
                    Id = 201,
                    ClientId = "U1",
                    SportId = 1,
                    TrainerId = null,
                    ReservationDateTime = now.AddMinutes(-90),
                    Duration = 60,
                    NumberOfPeople = 1
                }
            );
            await _context.SaveChangesAsync();

            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 200), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 201), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 100), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 101), Is.True);

            // Act
            await _service.DeleteExpiredReservationsAsync("U1");

            // Assert
            _context.ChangeTracker.Clear();

            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 200), Is.False);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 201), Is.False);

            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 100), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 101), Is.True);

            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 102), Is.True);
        }

        [Test]
        public async Task DeleteExpiredReservationsAsync_DoesNothing_WhenNoExpiredForUser()
        {
            // Arrange
            var countBefore = await _context.Reservations.CountAsync();

            // Act
            await _service.DeleteExpiredReservationsAsync("U1");

            // Assert
            _context.ChangeTracker.Clear();
            var countAfter = await _context.Reservations.CountAsync();
            Assert.That(countAfter, Is.EqualTo(countBefore));
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 100), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 101), Is.True);
            Assert.That(await _context.Reservations.AnyAsync(r => r.Id == 102), Is.True);
        }

        [Test]
        public async Task GetAddFormModelAsync_ReturnsFacilitiesSelectList()
        {
            var vm = await _service.GetAddFormModelAsync();

            var facilities = vm.Facilities.ToList();
            Assert.That(facilities.Count, Is.EqualTo(2));
            Assert.IsTrue(facilities.Any(f => f.Value == "1" && f.Text == "Main Hall"));
            Assert.IsTrue(facilities.Any(f => f.Value == "2" && f.Text == "Old Hall"));
        }

        [Test]
        public async Task AddAsync_CreatesSport_WithProvidedFields()
        {
            var model = new AddSportViewModel
            {
                Name = $"Padel {Guid.NewGuid()}",
                Price = 18,
                Duration = 60,
                ImageUrl = "padel.jpg",
                FacilityId = 1,
                MinPeople = 2,
                MaxPeople = 4
            };

            var before = await _context.Sports.CountAsync();
            await _service.AddAsync(model);
            var after = await _context.Sports.CountAsync();

            Assert.That(after, Is.EqualTo(before + 1));
            var created = await _context.Sports.FirstOrDefaultAsync(s => s.Name.StartsWith("Padel "));
            Assert.IsNotNull(created);
            Assert.IsFalse(created!.IsDeleted);
            Assert.That(created.Price, Is.EqualTo(18));
            Assert.That(created.Duration, Is.EqualTo(60));
            Assert.That(created.ImageUrl, Is.EqualTo("padel.jpg"));
            Assert.That(created.FacilityId, Is.EqualTo(1));
            Assert.That(created.MinPeople, Is.EqualTo(2));
            Assert.That(created.MaxPeople, Is.EqualTo(4));
        }

        [Test]
        public async Task ExistsAsync_ReturnsTrue_ForExistingActiveName()
        {
            Assert.IsTrue(await _service.ExistsAsync("Tennis"));
        }

        [Test]
        public async Task ExistsAsync_ReturnsFalse_ForDeletedOrMissing_AndIsCaseSensitive_CurrentImplementation()
        {
            Assert.IsFalse(await _service.ExistsAsync("Swimming"));
            Assert.IsFalse(await _service.ExistsAsync("tennis"));
            Assert.IsFalse(await _service.ExistsAsync("NoSuch"));
        }

        [Test]
        public async Task GetSportForEditAsync_ReturnsModel_WhenActive()
        {
            var vm = await _service.GetSportForEditAsync(1);

            Assert.IsNotNull(vm);
            Assert.That(vm!.Id, Is.EqualTo(1));
            Assert.That(vm.Name, Is.EqualTo("Tennis"));
            Assert.That(vm.Price, Is.EqualTo(20));
            Assert.That(vm.Duration, Is.EqualTo(60));
            Assert.That(vm.ImageUrl, Is.EqualTo("tennis.jpg"));
            Assert.That(vm.FacilityId, Is.EqualTo(1));
            Assert.That(vm.MinPeople, Is.EqualTo(1));
            Assert.That(vm.MaxPeople, Is.EqualTo(4));

            Assert.That(vm.Facilities.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetSportForEditAsync_ReturnsNull_WhenDeletedOrMissing()
        {
            Assert.IsNull(await _service.GetSportForEditAsync(4));
            Assert.IsNull(await _service.GetSportForEditAsync(999));
        }

        [Test]
        public async Task EditAsync_UpdatesFields_WhenSportExists()
        {
            var model = new AddSportViewModel
            {
                Name = "Tennis+",
                Price = 22,
                Duration = 75,
                ImageUrl = "tennis2.jpg",
                FacilityId = 1,
                MinPeople = 2,
                MaxPeople = 6
            };

            await _service.EditAsync(1, model);
            _context.ChangeTracker.Clear();

            var s = await _context.Sports.SingleAsync(x => x.Id == 1);
            Assert.That(s.Name, Is.EqualTo("Tennis+"));
            Assert.That(s.Price, Is.EqualTo(22));
            Assert.That(s.Duration, Is.EqualTo(75));
            Assert.That(s.ImageUrl, Is.EqualTo("tennis2.jpg"));
            Assert.That(s.FacilityId, Is.EqualTo(1));
            Assert.That(s.MinPeople, Is.EqualTo(2));
            Assert.That(s.MaxPeople, Is.EqualTo(6));
        }

        [Test]
        public async Task EditAsync_DoesNothing_WhenSportNotFound()
        {
            var before = await _context.Sports.AsNoTracking().ToListAsync();
            await _service.EditAsync(999, new AddSportViewModel { Name = "X", Price = 1, Duration = 1, ImageUrl = "x", FacilityId = 1, MinPeople = 1, MaxPeople = 1 });
            var after = await _context.Sports.AsNoTracking().ToListAsync();
            Assert.That(after.Count, Is.EqualTo(before.Count));
            CollectionAssert.AreEquivalent(before.Select(s => s.Id), after.Select(s => s.Id));
        }

        [Test]
        public async Task EditAsync_UpdatesEvenIfSportIsDeleted_CurrentImplementation()
        {
            var model = new AddSportViewModel
            {
                Name = "Swimming+",
                Price = 20,
                Duration = 35,
                ImageUrl = "swim2.jpg",
                FacilityId = 1,
                MinPeople = 1,
                MaxPeople = 5
            };

            await _service.EditAsync(4, model);
            _context.ChangeTracker.Clear();

            var s = await _context.Sports.SingleAsync(x => x.Id == 4);
            Assert.That(s.Name, Is.EqualTo("Swimming+"));
            Assert.That(s.Price, Is.EqualTo(20));
            Assert.That(s.Duration, Is.EqualTo(35));
            Assert.That(s.ImageUrl, Is.EqualTo("swim2.jpg"));
            Assert.IsTrue(s.IsDeleted);
        }

        [Test]
        public async Task GetSportForDeleteAsync_ReturnsModel_WithFacilityName_WhenActive()
        {
            var vm = await _service.GetSportForDeleteAsync(1);

            Assert.IsNotNull(vm);
            Assert.That(vm!.Id, Is.EqualTo(1));
            Assert.That(vm.Name, Is.EqualTo("Tennis"));
            Assert.That(vm.Facility, Is.EqualTo("Main Hall"));
        }

        [Test]
        public async Task GetSportForDeleteAsync_ReturnsModel_EvenIfFacilityIsDeleted_CurrentImplementation()
        {
            var vm = await _service.GetSportForDeleteAsync(3);

            Assert.IsNotNull(vm);
            Assert.That(vm!.Id, Is.EqualTo(3));
            Assert.That(vm.Name, Is.EqualTo("Basketball"));
            Assert.That(vm.Facility, Is.EqualTo("Old Hall"));
        }

        [Test]
        public async Task GetSportForDeleteAsync_ReturnsNull_WhenDeletedOrMissing()
        {
            Assert.IsNull(await _service.GetSportForDeleteAsync(4));
            Assert.IsNull(await _service.GetSportForDeleteAsync(999));
        }

        [Test]
        public async Task DeleteAsync_SoftDeletes_WhenActive()
        {
            await _service.DeleteAsync(2);

            var s = await _context.Sports.FindAsync(2);
            Assert.IsNotNull(s);
            Assert.IsTrue(s!.IsDeleted);
        }

        [Test]
        public async Task DeleteAsync_DoesNothing_WhenDeletedOrMissing()
        {
            await _service.DeleteAsync(4);
            var s4 = await _context.Sports.FindAsync(4);
            Assert.IsTrue(s4!.IsDeleted);

            var before = await _context.Sports.CountAsync();
            await _service.DeleteAsync(999);
            var after = await _context.Sports.CountAsync();
            Assert.That(after, Is.EqualTo(before));
        }

        [Test]
        public async Task GetFacilitiesSelectListAsync_ReturnsAllFacilities_NoDeletionFilter()
        {
            var list = (await _service.GetFacilitiesSelectListAsync()).ToList();

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.IsTrue(list.Any(f => f.Value == "1" && f.Text == "Main Hall"));
            Assert.IsTrue(list.Any(f => f.Value == "2" && f.Text == "Old Hall"));
        }

        [Test]
        public async Task GetAllAsSelectListAsync_ReturnsActiveSports_OrderedByName()
        {
            var list = (await _service.GetAllAsSelectListAsync()).ToList();

            Assert.That(list.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(
                new[] { "Basketball", "Football", "Tennis" },
                list.Select(x => x.Text).ToArray()
            );
            CollectionAssert.AreEqual(
                new[] { "3", "2", "1" },
                list.Select(x => x.Value).ToArray()
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}