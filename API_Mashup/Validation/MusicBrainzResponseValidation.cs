using System;
using System.Linq;
using ApiMashup.Models;

namespace ApiMashup.Validation
{
    public class MusicBrainzRelationsValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzRelationsValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => Context.Relations != null || Context.Relations.Any();
        public override string Message => "MusicBrainz response does not contain 'relations'";
    }

    public class MusicBrainzWikipediaValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzWikipediaValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => throw new NotImplementedException();
        public override string Message => throw new NotImplementedException();
    }

    public class MusicBrainzReleaseGroupsValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzReleaseGroupsValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => throw new NotImplementedException();
        public override string Message => throw new NotImplementedException();
    }
}