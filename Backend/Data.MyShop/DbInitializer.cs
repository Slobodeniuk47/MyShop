using Data.MyShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var Categorys = new List<CategoryEntity>
            {
                   new CategoryEntity {
                       Id=1,
                       Name = "Комп'ютери та ноутбуки",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=2,
                       Name = "Смартфони",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=3,
                       Name = "Побутова техніка",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=4,
                       Name = "Дача, сад, город",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=5,
                       Name = "Спорт і захоплення",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=6,
                       Name = "Офіс, школа, книги",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=7,
                       Name = "test",
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       Image = "1.jpg",
                       Description = "description test", }
            };

            modelBuilder.Entity<CategoryEntity>().HasData(Categorys);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    Id = 1,
                    Name = "ПК Х123434",
                    CategoryId = 1,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "test",
                    Price = 436765,
                },
                new ProductEntity
                {
                    Id = 2,
                    Name = "iHunt Titan P13000 PRO 2021",
                    CategoryId = 2,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .",
                    Price = 13940,
                },
                new ProductEntity
                {
                    Id = 3,
                    Name = "BEKO CNA295K20XP",
                    CategoryId = 3,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "Холодильники з системою NeoFrost ",
                    Price = 10999,
                },
                new ProductEntity
                {
                    Id = 4,
                    Name = "Bosch UniversalChain 40",
                    CategoryId = 4,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "Ланцюгова пила Bosch UniversalChain ",
                    Price = 3958,
                },
                new ProductEntity
                {
                    Id = 5,
                    Name = "Champion Spark 29 19.5 Black-neon yellow-white",
                    CategoryId = 5,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "Велосипед Champion Spark 29 ",
                    Price = 5460,
                },
                new ProductEntity
                {
                    Id = 6,
                    Name = "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла",
                    CategoryId = 6,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Description = "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ",
                    Price = 1199,
                }

            );
        }
    }
}
