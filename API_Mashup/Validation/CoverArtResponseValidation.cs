using System;
using System.Linq;
using ApiMashup.Models;
using System.Collections;

namespace ApiMashup.Validation
{
    /// <summary>
    /// Checks if Images are null or length 0
    /// </summary>
    public class CoverArtImagesValidation : ValidationBase<CoverArtResponse>
    {
        public CoverArtImagesValidation(CoverArtResponse context) : base(context) { }
        public override bool IsValid => Context.Images != null && Context.Images.Count() > 0;
        public override string Message => "CoverArtArchive response does not contain images";
    }
}