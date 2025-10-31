

using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IGallery : ICRUD<GalleryDTO, GalleryDTOID>
    {
        Task<ImageDTOID> AddImageToGallery(ImageDTOCreation imageDTO);
    }
}

