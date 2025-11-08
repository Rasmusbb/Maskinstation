

using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IGallery : ICRUD<GalleryDTO, GalleryDTOID>
    {
        Task<ImageDTOID> AddImageToGallery(ImageDTOCreation imageDTO);
        Task<(Stream Stream, string ContentType)> GetVideo(Guid ImageID);
        Task<(MemoryStream Stream, string ContentType)> GetProfilPic(Guid imageID);
        Task<(MemoryStream Stream, string ContentType)> GetFirstPic(Guid GalleryID);
        Task<GalleryDTOID> GetByIDAsync(Guid GalleryID);

    }
}

