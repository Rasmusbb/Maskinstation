

using Maskinstation.DTOs;

namespace Maskinstation.interfaces
{
    public interface IGallery : ICRUD<GalleryDTO, GalleryDTOID>
    {
        Task<ImageDTOID> AddImageToGallery(ImageDTOCreation imageDTO);
    }
}

