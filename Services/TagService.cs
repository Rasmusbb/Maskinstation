using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Maskinstation.Models;
using Mapster;

namespace Maskinstation.Services
{
    public class TagService : ITag
    {
        private readonly MaskinstationContext _context;

        public TagService(MaskinstationContext context)
        {
            _context = context;
        }

        string DBNullText = "Database context is not available.";
        public async Task<TagDTOID> CreateAsync(TagDTO TagDTO)
        {
            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Tag Tag = TagDTO.Adapt<Tag>();
            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();
            return Tag.Adapt<TagDTOID>();
        }

        public async Task<IEnumerable<TagDTOID>> GetAllAsync()
        {
            IEnumerable<TagDTOID> Tags = new List<TagDTOID>();
            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Tags = _context.Tags.Adapt<IEnumerable<TagDTOID>>();
            return Tags;

        }

        public async Task<TagDTOID> GetByIDAsync(Guid TagID)
        {
            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Tag Tag = await _context.Tags.FindAsync(TagID);
            return Tag.Adapt<TagDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid TagID, TagDTO Tag)
        {
            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid TagID)
        {

            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Tag Tag = await _context.Tags.FindAsync(TagID);
            if (!Tag.deletable)
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this action.");
            }
            _context.Tags.Remove(Tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
