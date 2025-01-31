﻿using BrightistRenderer.Models.Texts.Parsers;

namespace BrightistRenderer.Models.UI.Components
{
    internal class TextBlock
    {
        public IList<IList<CharacterData>> CharacterLines { get; } = new List<IList<CharacterData>>();
        public bool IsFullStop { get; set; }
    }
}
