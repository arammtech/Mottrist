using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Configurations.TravellersSchemaConfiguration;
using Mottrist.Repository.EntityFrameworkCore.Context;

namespace Mottrist.Repository.SeedData
{
    public class SeedDb
    {
      
        private readonly AppDbContext _context;

        public SeedDb(AppDbContext context)
        {
            
            _context = context;
        }

        //public async Task<Result> LoadTravelers()
        //{
        //}
        
    }
}
