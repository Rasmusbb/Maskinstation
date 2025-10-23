using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Maskinstation.Services
{
    public class BrandService : IBrand
    {
        private readonly MaskinstationContext _context;

        public BrandService(MaskinstationContext context)
        {
            _context = context;
        }

        string DBNullText = "Database context is not available.";
        public async Task<BrandDTOID> CreateAsync(BrandDTO BrandDTO)
        {
            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Brand Brand = BrandDTO.Adapt<Brand>();
            _context.Brands.Add(Brand);
            await _context.SaveChangesAsync();
            return Brand.Adapt<BrandDTOID>();
        }

        public async Task<IEnumerable<BrandDTOID>> GetAllAsync()
        {
            IEnumerable<BrandDTOID> Brands = new List<BrandDTOID>();
            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Brands = _context.Brands.Adapt<IEnumerable<BrandDTOID>>();
            return Brands;

        }

        public async Task<BrandDTOID> GetByIDAsync(Guid BrandID)
        {
            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Brand Brand = await _context.Brands.FindAsync(BrandID);
            return Brand.Adapt<BrandDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid BrandID, BrandDTO Brand)
        {
            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid BrandID)
        {

            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Brand Brand = await _context.Brands.FindAsync(BrandID);
            _context.Brands.Remove(Brand);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
