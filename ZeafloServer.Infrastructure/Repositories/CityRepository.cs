using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Infrastructure.Database;

namespace ZeafloServer.Infrastructure.Repositories
{
    public sealed class CityRepository : BaseRepository<City, Guid>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<City?> GetCityByNameAsync(string city)
        {
            return await DbSet.Where(c => c.Name.ToLower().Contains(RemoveVietnameseDiacritics(city).ToLower())).FirstOrDefaultAsync();
        }

        private string RemoveVietnameseDiacritics(string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;   

            input = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in input)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
