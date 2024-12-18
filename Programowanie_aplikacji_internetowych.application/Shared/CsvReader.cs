using CsvHelper;
using Microsoft.AspNetCore.Http;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Programowanie_aplikacji_internetowych.application.Shared
{
    public static class CsvReader
    {
        public static List<Post> ReadPosts(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Przesłany plik jest pusty lub nie został dołączony.");
            }

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, 
                PrepareHeaderForMatch = args => args.Header?.Trim() 
            });

            var posts = new List<Post>();

            try
            {

                if (csv.Read())
                {
                    csv.ReadHeader();
                }


                while (csv.Read())
                {
                    var post = new Post
                    {
                        Title = csv.GetField("Title"),
                        Text = csv.GetField("Text"),
                        ImageUrl = csv.GetField("ImageUrl")
                    };

                    posts.Add(post);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Błąd podczas przetwarzania pliku CSV: {ex.Message}", ex);
            }

            return posts;
        }
    }
}
