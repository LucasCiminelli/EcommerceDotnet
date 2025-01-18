using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Persistence
{
    public class EcommerceDbContextData
    {
        public static async Task LoadDataAsync(
            EcommerceDbContext context,
            UserManager<Usuario> usuarioManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {

            try
            {

                if (!roleManager.Roles.Any()) //si no hay roles en mi DB osea, si está vacia, creo los roles.
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));

                }

                if (!usuarioManager.Users.Any())
                {
                    var usuarioAdmin = new Usuario
                    {
                        Nombre = "Lucas",
                        Apellido = "Ciminelli",
                        Email = "lucasaciminelli@gmail.com",
                        UserName = "LucasCiminelli",
                        Telefono = "123123123",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/vaxidrez.jpg?alt=media&token=14a28860-d149-461e-9c25-9774d7ac1b24"
                    };

                    await usuarioManager.CreateAsync(usuarioAdmin, "PasswordLucasCiminelli1234$");
                    await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);


                    var usuario = new Usuario
                    {
                        Nombre = "Juan",
                        Apellido = "Perez",
                        Email = "JuanPerez@gmail.com",
                        UserName = "JuanPerez",
                        Telefono = "3323232323",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                    };

                    await usuarioManager.CreateAsync(usuario, "PasswordJuanPerez1234$");
                    await usuarioManager.AddToRoleAsync(usuario, Role.USER);
                }

                //Agregando data master

                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../Infrastructure/Data/category.json"); //lee la data del json
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData); //la deserializa a una lista de objetos tipo Category

                    await context.Categories!.AddRangeAsync(categories!); //las inserta
                    await context.SaveChangesAsync(); //guarda los cambios en la base de datos. Dispara la transacción.
                }

                if (!context.Products!.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/product.json"); //lee la data del json
                    var products = JsonConvert.DeserializeObject<List<Product>>(productData); //la deserializa a una lista de objetos tipo Product

                    await context.Products!.AddRangeAsync(products!); //las inserta
                    await context.SaveChangesAsync(); //guarda los cambios en la base de datos. Dispara la transacción.
                }

                if (!context.Images!.Any())
                {
                    var imageData = File.ReadAllText("../Infrastructure/Data/image.json"); //lee la data del json
                    var images = JsonConvert.DeserializeObject<List<Image>>(imageData); //la deserializa a una lista de objetos tipo Image

                    await context.Images!.AddRangeAsync(images!); //las inserta en memoria
                    await context.SaveChangesAsync(); //guarda los cambios en la base de datos. Dispara la transacción.
                }

                if (!context.Reviews!.Any())
                {
                    var reviewData = File.ReadAllText("../Infrastructure/Data/review.json"); //lee la data del json
                    var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData); //la deserializa a una lista de objetos tipo Image

                    await context.Reviews!.AddRangeAsync(reviews!); //las inserta en memoria
                    await context.SaveChangesAsync(); //guarda los cambios en la base de datos. Dispara la transacción.
                }

                if (!context.Countries!.Any())
                {
                    var countryData = File.ReadAllText("../Infrastructure/Data/countries.json"); //lee la data del json
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData); //la deserializa a una lista de objetos tipo Country

                    await context.Countries!.AddRangeAsync(countries!); //las inserta en memoria
                    await context.SaveChangesAsync(); //guarda los cambios en la base de datos. Dispara la transacción.
                }



            }
            catch (Exception e)
            {

                var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
                logger.LogError(e.Message);
            }



        }
    }
}