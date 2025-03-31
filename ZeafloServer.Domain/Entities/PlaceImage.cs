using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class PlaceImage : Entity<Guid>
    {
        [Column("place_image_id")]
        public Guid PlaceImageId { get; private set; }

        [Column("place_id")]
        public Guid PlaceId { get; private set; }

        [Column("image_url")]
        public string ImageUrl { get; private set; }

        [ForeignKey("PlaceId")]
        [InverseProperty("PlaceImages")]
        public virtual Place? Place { get; set; }

        public PlaceImage(
            Guid placeImageId,
            Guid placeId,
            string imageUrl
        ) : base(placeImageId)
        {
            PlaceImageId = placeImageId;
            PlaceId = placeId;
            ImageUrl = imageUrl;
        }

        public void SetPlaceId( Guid placeId ) {  PlaceId = placeId; }
        public void SetImageUrl( string imageUrl ) { ImageUrl = imageUrl; }
    }
}
