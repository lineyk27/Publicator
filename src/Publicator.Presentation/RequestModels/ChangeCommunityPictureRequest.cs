using System;

namespace Publicator.Presentation.RequestModels
{
    public class ChangeCommunityPictureRequest : ChangePictureRequest
    {
        public Guid CommunityId { get; set; }
    }
}
