using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }
        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Caderno Espiral Pequeno",
                    Description = "Caderno espiral com 100 folhas pequeno capa dura",
                    Image = "caderno.png",
                    Price = 7.65M,
                    Category = "MaterialEscolar"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Borracha branca pequena",
                    Description = "Borracha branca pequena para lápis",
                    Image = "borracha.png",
                    Price = 4.55M,
                    Category = "MaterialEscolar"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Estojo de plástico pequeno",
                    Description = "Estojo de plástico pequeno azul",
                    Image = "estojo.png",
                    Price = 6.79M,
                    Category = "MaterialEscolar"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Clips para papéis pequenos",
                    Description = "Clips para papéis pequenos 100 g",
                    Image = "clips.png",
                    Price = 3.25M,
                    Category = "Acessorios"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "Compasso de plástico pequeno",
                    Description = "Compasso de plástico pequeno azul",
                    Image = "compasso.png",
                    Price = 8.99M,
                    Category = "Acessorios"
                },
            };
        }
    }
}
