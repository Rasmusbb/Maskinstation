using Maskinstation.interfaces;
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.models;
using Mapster;


namespace Maskinstation.Services
{
    public class ImageService : IImage
    {
        private readonly MaskinstationContext _context;

        public ImageService(MaskinstationContext context)
        {
            _context = context;
        }

        string DBNullText = "Database context is not available.";
        public async Task<ImageDTOID> CreateAsync(ImageDTO ImageDTO)
        {
            if (_context.Images == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Image Image = ImageDTO.Adapt<Image>();
            _context.Images.Add(Image);
            await _context.SaveChangesAsync();
            return Image.Adapt<ImageDTOID>();
        }

        public async Task<IEnumerable<ImageDTOID>> GetAllAsync() 
        {
            IEnumerable<ImageDTOID> Images = new List<ImageDTOID>();
            if (_context.Images == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Images = _context.Images.Adapt<IEnumerable<ImageDTOID>>();
            return Images;

        }

        public async Task<ImageDTOID> GetByIDAsync(Guid ImageID)
        {
            if (_context.Images == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Image Image = await _context.Images.FindAsync(ImageID);
            return Image.Adapt<ImageDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid ImageID, ImageDTO Image)
        {
            if (_context.Images == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid ImageID)
        {

            if (_context.Images == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Image Image = await _context.Images.FindAsync(ImageID);
            _context.Images.Remove(Image);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

