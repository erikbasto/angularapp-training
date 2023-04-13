using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class Seed
	{
		public static async Task SeedUsers(DataContext context)
		{
			if (await context.Users.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

			foreach (var user in users)
			{
				using var hmac = new HMACSHA512();

				user.Username = user.Username.ToLower();
				// using the same password gemneration for training purposes
				user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P4$$w0rd"));
				user.PasswordSalt = hmac.Key;

				context.Users.Add(user);
			}

			await context.SaveChangesAsync();
		}
	}
}

