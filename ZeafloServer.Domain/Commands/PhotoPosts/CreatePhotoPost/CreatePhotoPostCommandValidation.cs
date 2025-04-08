using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.PhotoPosts.CreatePhotoPost
{
    public sealed class CreatePhotoPostCommandValidation : AbstractValidator<CreatePhotoPostCommand>
    {
        public CreatePhotoPostCommandValidation()
        {
            
        }
    }
}
