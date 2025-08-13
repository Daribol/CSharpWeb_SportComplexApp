using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;

namespace SportComplexApp.Data.Seeding;

public class TrainerSeeding : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> config)
    {
        config.HasData(
            new Trainer
            {
                Id = 1,
                Name = "Kiril",
                LastName = "Raikov",
                Bio = "Certified basketball coach with 8+ years of experience.",
                ImageUrl = "https://nutrigen.bg/_cms/wp-content/uploads/2019/11/Kiril-Raykov-profile-pic-702x1024.jpg",
                IsDeleted = false,
                ClientId = null
            },
            new Trainer
            {
                Id = 2,
                Name = "Vili",
                LastName = "Markovska",
                Bio = "Yoga & Pilates instructor focused on mobility and mindfulness.",
                ImageUrl = "https://static.dir.bg/uploads/images/2015/08/03/691218/orig.jpg?_=1526561264",
                IsDeleted = false,
                ClientId = null
            },
            new Trainer
            {
                Id = 3,
                Name = "Kostadin",
                LastName = "Lefterov",
                Bio = "CrossFit coach, competition-level conditioning specialist.",
                ImageUrl = "https://toppresa.com/318883/%D0%BA%D0%BE%D1%81%D1%82%D0%B0%D0%B4%D0%B8%D0%BD-%D0%BB%D0%B5%D1%84%D1%82%D0%B5%D1%80%D0%BE%D0%B2-%D0%BE%D1%82-%D0%B3%D0%BE%D1%86%D0%B5-%D0%B4%D0%B5%D0%BB%D1%87%D0%B5%D0%B2-%D0%B5%D0%B4%D0%BD%D0%B0",
                IsDeleted = false,
                ClientId = null
            },
            new Trainer
            {
                Id = 4,
                Name = "Nikolai",
                LastName = "Mollov",
                Bio = "Swimming coach—technique and endurance for all levels.",
                ImageUrl = "https://www.pluvane.com/wp-content/uploads/2021/02/coach_13.jpg",
                IsDeleted = false,
                ClientId = null
            },
            new Trainer
            {
                Id = 5,
                Name = "Grigor",
                LastName = "Dimitrov",
                Bio = "Tennis training: technique, tactics, and matchplay.",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fbg.wikipedia.org%2Fwiki%2F%25D0%2593%25D1%2580%25D0%25B8%25D0%25B3%25D0%25BE%25D1%2580_%25D0%2594%25D0%25B8%25D0%25BC%25D0%25B8%25D1%2582%25D1%2580%25D0%25BE%25D0%25B2&psig=AOvVaw3qECx0GFabpMtQcgCFtLWW&ust=1755155485839000&source=images&cd=vfe&opi=89978449&ved=2ahUKEwj13avrnYePAxWcb_EDHVP9G7EQjRx6BAgAEBo",
                IsDeleted = false,
                ClientId = null
            }
        );
    }
}