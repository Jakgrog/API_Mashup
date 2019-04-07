using System;
using ApiMashup.DAO;

namespace ApiMashup.Validation
{
    public class CoverArtImagesValidation : ValidationBase<CoverArtResponse>
    {
        public CoverArtImagesValidation(CoverArtResponse context) : base(context) { }
        public override bool IsValid => Context.Images == null || !(Context.Images.Length > 0);

        public override string Message => "CoverArtArchive response does not contain 'images'";
    }
    public class CoverArtUriValidation : ValidationBase<CoverArtResponse>
    {
        public CoverArtUriValidation(CoverArtResponse context) : base(context) { }
        public override bool IsValid => throw new NotImplementedException();
        public override string Message => throw new NotImplementedException();
    }
}