using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Maskinstation.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Maskinstation.Migrations;

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

            var tags = await _context.Tags
                .Where(t => imageCreation.Tags.Contains(t.TagID))
                .ToListAsync();
            List<ImageTag> ImageTags = new();
            foreach (var tag in tags)
            {
                ImageTag imagetag = new ImageTag { ImageID = image.ImageID, Image = image, TagID = tag.TagID, Tag = tag };
                ImageTags.Add(imagetag);
            }
            image.Tags = ImageTags;
            await _context.SaveChangesAsync();
            return image.Adapt<ImageDTOID>();
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
