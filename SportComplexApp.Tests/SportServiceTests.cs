using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services;
using SportComplexApp.Web.ViewModels.Sport;
using static SportComplexApp.Common.ErrorMessages.Reservation;
using static SportComplexApp.Common.ApplicationConstants;

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

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}