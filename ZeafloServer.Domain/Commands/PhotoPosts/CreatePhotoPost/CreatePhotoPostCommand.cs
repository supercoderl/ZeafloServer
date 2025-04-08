using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.PhotoPosts.CreatePhotoPost
{
    public sealed class CreatePhotoPostCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePhotoPostCommandValidation s_validation = new();

        public Guid PhotoPostId { get; }
        public Guid UserId { get; }
        public string Image { get; }
        public AnnotationType? AnnotationType { get; }
        public string? AnnotationValue { get; }

        public CreatePhotoPostCommand(
            Guid photoPostId,
            Guid userId,
            string image,
            AnnotationType? annotationType,
            string? annotationValue
        ) : base(Guid.NewGuid())
        {
            PhotoPostId = photoPostId;
            UserId = userId;
            Image = image;
            AnnotationType = annotationType;
            AnnotationValue = annotationValue;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
