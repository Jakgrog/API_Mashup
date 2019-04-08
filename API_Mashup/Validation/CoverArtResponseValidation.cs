using System;
using System.Linq;
using ApiMashup.Models;
using System.Collections;

namespace ApiMashup.Validation
{
    public class CoverArtImagesValidation : ValidationBase<CoverArtResponse>
    {
        public CoverArtImagesValidation(CoverArtResponse context) : base(context) { }
        public override bool IsValid => Context.Images != null && Context.Images.Count() > 0;
        public override string Message => "CoverArtArchive response does not contain images";
    }
    public class CoverArtImageUriValidation : ValidationBase<CoverArtResponse>
    {
        private Image[] nullImages;
        public CoverArtImageUriValidation(CoverArtResponse context) : base(context) { }
        public override bool IsValid {
            get
            {
                nullImages = Context.Images.Where(x => x.image == null).ToArray();
                return nullImages != null && nullImages.Count() > 0;
            }
        }
        public override string Message => "Images: " + 
            nullImages.ToString() + ", in CoverArtArchive response does not contain a image Url";
    }
}