﻿namespace ClashRoyale.Server.Logic.Entries
{
    using System.Linq;

    using ClashRoyale.Server.Extensions;
    using ClashRoyale.Server.Extensions.Helper;
    using ClashRoyale.Server.Files.Csv.Client;
    using ClashRoyale.Server.Files.Csv.Logic;

    using Newtonsoft.Json;

    internal class AllianceHeaderEntry
    {
        private Clan Clan;

        [JsonProperty("highId")]        private int HighId;
        [JsonProperty("lowId")]         private int LowId;

        [JsonProperty("name")]          internal string Name;

        [JsonProperty("type")] 			internal int Type;
        [JsonProperty("required_score")]internal int RequiredScore;

        [JsonProperty("region")]        internal RegionData Region;
        [JsonProperty("locale")]        internal LocaleData Locale;
        [JsonProperty("badge")]         internal AllianceBadgeData Badge;

        [JsonProperty("members")]       internal int NumberOfMembers
        {
            get
            {
                return this.Clan.Members.Count;
            }
        }

        [JsonProperty("score")]         internal int Score
        {
            get
            {
                int Score      = 0;
                var Entries    = this.Clan.Members.Values.ToArray();

                for (int I = 0; I < Entries.Length; I++)
                {
                    Score += Entries[I].Score;
                }

                return Score / 2;
            }
        }

        [JsonProperty("donations")]     internal int Donations
        {
            get
            {
                int Donations  = 0;
                var Entries    = this.Clan.Members.Values.ToArray();

                for (int I = 0; I < Entries.Length; I++)
                {
                    Donations += Entries[I].Donations;
                }

                return Donations;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceHeaderEntry"/> class.
        /// </summary>
        public AllianceHeaderEntry()
        {
            // AllianceHeaderEntry.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceHeaderEntry"/> class.
        /// </summary>
        public AllianceHeaderEntry(Clan Clan)
        {
            this.Clan   = Clan;
            this.HighId 	= Clan.HighId;
            this.LowId 	    = Clan.LowId;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Stream)
        {
            Stream.WriteLong(this.Clan.AllianceId);
            Stream.WriteString(this.Name);
            Stream.EncodeData(this.Badge);

            Stream.WriteVInt(this.Type);
            Stream.WriteVInt(this.NumberOfMembers);
            Stream.WriteVInt(this.Score);
            Stream.WriteVInt(this.RequiredScore);

            Stream.WriteVInt(0);
            Stream.WriteVInt(0);
            Stream.WriteVInt(0);
            Stream.WriteVInt(50);
            Stream.WriteVInt(this.Donations);
            Stream.WriteVInt(2);

            Stream.EncodeData(this.Locale);
            Stream.EncodeData(this.Region);

            Stream.WriteBoolean(false);
        }

        /// <summary>
        /// Sets the alliance.
        /// </summary>
        internal void SetAlliance(Clan Clan)
        {
            this.Clan   = Clan;
            this.HighId     = Clan.HighId;
            this.LowId      = Clan.LowId;
        }
    }
}