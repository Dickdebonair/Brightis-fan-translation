﻿namespace BrightistRenderer.Models.Texts.Parsers.ControlCodes
{
    internal class FlagSetControlCodeCharacterData : ControlCodeCharacterData
    {
        public int Flag => Arguments.Length <= 0 ? -1 : Arguments[0];

        public FlagSetControlCodeCharacterData(byte code) : base(code)
        {
        }

        public FlagSetControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
