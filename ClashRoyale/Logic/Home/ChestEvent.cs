﻿namespace ClashRoyale.Logic.Home
{
    using System.Collections.Generic;

    using ClashRoyale.Extensions;
    using ClashRoyale.Extensions.Helper;
    using ClashRoyale.Files.Csv.Logic;

    public class ChestEvent
    {
        private TreasureChestData ChestData;
        // private ShopChestData ShopChestData;

        private List<SpellData> FortuneSpells;

        private int Index;

        /// <summary>
        /// Gets the type name of the current <see cref="ShopChestData"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.ChestData?.Name; // this.ShopChestData?.Type
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChestEvent"/> class.
        /// </summary>
        public ChestEvent()
        {
            this.FortuneSpells = new List<SpellData>();
        }

        /// <summary>
        /// Decodes from the specified stream.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public void Decode(ByteStream Stream)
        {
            this.ChestData = Stream.DecodeData<TreasureChestData>();

            Stream.ReadVInt();
            Stream.ReadVInt();

            Stream.ReadString();

            this.Index = Stream.ReadVInt();

            Stream.DecodeSpellList(ref this.FortuneSpells);

            Stream.ReadVInt();
            Stream.ReadVInt();
            Stream.ReadVInt();
        }

        /// <summary>
        /// Encodes in the specified stream.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public void Encode(ChecksumEncoder Stream)
        {
            Stream.EncodeData(this.ChestData);

            Stream.WriteVInt(88);
            Stream.WriteVInt(0);

            Stream.WriteString(this.Name);

            Stream.WriteVInt(346);
            Stream.WriteVInt(this.Index);

            Stream.EncodeSpellList(this.FortuneSpells);

            Stream.WriteVInt(-1);
            Stream.WriteVInt(0);
            Stream.WriteVInt(-1);

            return;

            Stream.WriteVInt(19);
            Stream.WriteVInt(325);
            Stream.WriteVInt(88);
            Stream.WriteVInt(2);

            Stream.WriteString("Fortune");

            Stream.WriteVInt(346);
            Stream.WriteVInt(1); // Index

            Stream.WriteVInt(4); // Array
            {
                Stream.AddRange("1A-02".HexaToBytes());
                Stream.AddRange("1A-15".HexaToBytes());
                Stream.AddRange("1A-26".HexaToBytes());
                Stream.AddRange("1A-19".HexaToBytes());
            }

            Stream.WriteVInt(90000005);
            Stream.WriteVInt(0);
            Stream.WriteVInt(-1);
        }
    }
}