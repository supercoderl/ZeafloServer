using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class PlaceLike : Entity<Guid>
    {
        [Column("place_like_id")]
        public Guid PlaceLikeId { get; private set; }

        [Column("place_id")]
        public Guid PlaceId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("PlaceLikes")]
        public virtual User? User { get; set; }

        [ForeignKey("PlaceId")]
        [InverseProperty("PlaceLikes")]
        public virtual Place? Place { get; set; }

        public PlaceLike(
            Guid placeLikeId,
            Guid placeId,
            Guid userId,
            DateTime createdAt
        ) : base( placeLikeId )
        {
            PlaceLikeId = placeLikeId;
            PlaceId = placeId;
            UserId = userId;
            CreatedAt = createdAt;
        }

        public void SetPlaceId( Guid placeLikeId ) { PlaceLikeId = placeLikeId; }
        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
