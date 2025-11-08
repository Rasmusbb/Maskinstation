using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Maskinstation.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;



namespace Maskinstation.Services
{
    public class GalleryService : IGallery
    {
        private readonly MaskinstationContext _context;
        private readonly GridFSService _GridFS;

        public GalleryService(MaskinstationContext context, GridFSService gridFS)
        {
            _context = context;
            _GridFS = gridFS;
        }

        string DBNullText = "Database context is not available.";
        public async Task<GalleryDTOID> CreateAsync(GalleryDTO GalleryDTO)
        {
            if (_context.Galleries == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Gallery Gallery = GalleryDTO.Adapt<Gallery>();
            _context.Galleries.Add(Gallery);
            await _context.SaveChangesAsync();
            return Gallery.Adapt<GalleryDTOID>();
        }


        public async Task<(MemoryStream Stream, string ContentType)> GetProfilPic(Guid ImageID)
        {
            Image image = await _context.Images.FindAsync(ImageID);
            if (image == null)
            {
                throw new KeyNotFoundException($"Picture with ID '{ImageID}' was not found.");
            }
            return await _GridFS.DownloadImageAsync(image.FileID);
        }

        public async Task<(MemoryStream Stream, string ContentType)> GetFirstPic(Guid GalleryID)
        {
            Image image = await _context.Images.Where(i => i.GalleryID == GalleryID).FirstOrDefaultAsync();
            if(image == null)
            {
                throw new KeyNotFoundException($"Gallery empty");
            }
            return await GetProfilPic(image.ImageID);
        }

        public async Task<ImageDTOID> AddImageToGallery(ImageDTOCreation imageCreation)
        {
            string FileID = await _GridFS.UploadImageAsync(imageCreation.ImageData);
            if (FileID == null) {
                throw new InvalidOperationException("Failed to upload image to GridFS.");
            }
            Image image = imageCreation.Adapt<Image>();
            image.FileID = FileID;
            image.Created = DateTime.UtcNow;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image.Adapt<ImageDTOID>();
        }



        public async Task<(Stream Stream, string ContentType)> GetVideo(Guid ImageID)
        {
            Image image = await _context.Images.FindAsync(ImageID);
            if (image == null)
            {
                throw new KeyNotFoundException($"Picture with ID '{ImageID}' was not found.");
            }
            return await _GridFS.GetVideoAsync(image.FileID);
        }

        public async Task<IEnumerable<GalleryDTOID>> GetAllAsync()
        {
            if (_context.Galleries == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            IEnumerable<Gallery> galleries = await _context.Galleries.Include(g => g.Images).ThenInclude(i => i.Tags).ToListAsync();
            return galleries.Adapt<List<GalleryDTOID>>();

        }

        public async Task<GalleryDTOID> GetByIDAsync(Guid GalleryID)
        {
            if (_context.Galleries == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Gallery gallery = await _context.Galleries.Include(g => g.Images).ThenInclude(i => i.Tags).FirstOrDefaultAsync(g => g.GalleryID == GalleryID);
            return gallery.Adapt<GalleryDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid GalleryID, GalleryDTO Gallery)
        {
            if (_context.Galleries == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid GalleryID)
        {

            if (_context.Galleries == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Gallery Gallery = await _context.Galleries.FindAsync(GalleryID);
            _context.Galleries.Remove(Gallery);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
