using Data.MyShop.Constants;
using Data.MyShop.Entities;
using Data.MyShop.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.MyShop
{
    public static class DbSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();
                
                if (!context.Categories.Any())
                {
                    //var categories = new List<CategoryEntity>
                    //{
                    //new CategoryEntity {
                    //   Id=1,
                    //   Name = "Комп'ютери та ноутбуки",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=2,
                    //   Name = "Смартфони",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=3,
                    //   Name = "Побутова техніка",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=4,
                    //   Name = "Дача, сад, город",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=5,
                    //   Name = "Спорт і захоплення",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=6,
                    //   Name = "Офіс, школа, книги",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description", },
                    //new CategoryEntity {
                    //   Id=7,
                    //   Name = "test",
                    //   DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //   IsDelete = false,
                    //   Image = "1.jpg",
                    //   Description = "description test", }

                    //};
                    CategoryEntity best = new CategoryEntity()
                    {
                        Name = "Краща ціна",
                        ParentId = null,
                        Description = "Для козаків",
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                        Image = "best.jpg"
                    };
                    context.Categories.Add(best);
                    context.SaveChanges();

                    CategoryEntity heroes = new CategoryEntity()
                    {
                        Name = "Герої",
                        ParentId = null,
                        Description = "Для козаків",
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                        Image = "heroes.jpg"                       
                    };
                    context.Categories.Add(heroes);
                    context.SaveChanges();
                    //context.Categories.AddRange(categories);
                    //context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    //var products = new List<ProductEntity>()
                    //{
                    //    new ProductEntity
                    //    {
                    //        Id = 1,
                    //        Name = "ПК Х123434",
                    //        CategoryId = 1,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "test",
                    //        Price = 436765,
                    //    },
                    //    new ProductEntity
                    //    {
                    //        Id = 2,
                    //        Name = "iHunt Titan P13000 PRO 2021",
                    //        CategoryId = 2,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .",
                    //        Price = 13940,
                    //    },
                    //    new ProductEntity
                    //    {
                    //        Id = 3,
                    //        Name = "BEKO CNA295K20XP",
                    //        CategoryId = 3,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "Холодильники з системою NeoFrost ",
                    //        Price = 10999,
                    //    },
                    //    new ProductEntity
                    //    {
                    //        Id = 4,
                    //        Name = "Bosch UniversalChain 40",
                    //        CategoryId = 4,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "Ланцюгова пила Bosch UniversalChain ",
                    //        Price = 3958,
                    //    },
                    //    new ProductEntity
                    //    {
                    //        Id = 5,
                    //        Name = "Champion Spark 29 19.5 Black-neon yellow-white",
                    //        CategoryId = 5,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "Велосипед Champion Spark 29 ",
                    //        Price = 5460,
                    //    },
                    //    new ProductEntity
                    //    {
                    //        Id = 6,
                    //        Name = "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла",
                    //        CategoryId = 6,
                    //        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    //        Description = "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ",
                    //        Price = 1199,
                    //    }
                    //};
                    //context.Products.AddRange(products);
                    //context.SaveChanges();
                }
                if (!roleManager.Roles.Any())
                {
                    foreach (var role in Roles.All)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity
                        {
                            Name = role
                        }).Result;
                    }
                }
                if (!userManager.Users.Any())
                {
                    var user = new UserEntity
                    {
                        Email = "admin@gmail.com",
                        FirstName = "Admin Firstname",
                        LastName = "Admin Lastname",
                        Image = "admin.jpg",
                        PhoneNumber = "097 23 45 212"
                    };
                    var result = userManager.CreateAsync(user, "123456").Result;
                    result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }
        }
    }
}
