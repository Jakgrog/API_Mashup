using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMashup.Validation
{
    /// <summary>
    /// Checks if input mbid is valid (Exactly 36 characters long)
    /// </summary>
    public class MbidValidation : ValidationBase<string>
    {
        public MbidValidation(string context) : base(context) { }
        public override bool IsValid => Context.Length == 36;
        public override string Message => "Invalid Mbid, please enter a 36 character long mbid. If you are unsure of what a mbid is " +
            "go to: https://musicbrainz.org/doc/MusicBrainz_Identifier for more information";
    }
}