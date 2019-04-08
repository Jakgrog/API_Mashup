using System;
using System.Linq;
using ApiMashup.Models;

namespace ApiMashup.Validation
{
    public class MusicBrainzRelationsValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzRelationsValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => Context.Relations != null || Context.Relations.Any();
        public override string Message => "MusicBrainz response does not contain relations";
    }

    public class MusicBrainzWikipediaValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzWikipediaValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => Context.GetWikidataID() != null;
        public override string Message => "MusicBrainz response does not contain any Wikidata ID";
    }

    public class MusicBrainzReleaseGroupsValidation : ValidationBase<MusicBrainzResponse>
    {
        public MusicBrainzReleaseGroupsValidation(MusicBrainzResponse context) : base(context) { }
        public override bool IsValid => Context.ReleaseGroups != null || Context.ReleaseGroups.Any();
        public override string Message => "MusicBrainz response does not contain any release groups";
    }
}