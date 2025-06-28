using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Patchers.Layout;

namespace TranslationToSource.Patchers.Layout
{
    internal class OvrPatchLayouter
    {
        private readonly PatchPacker _packer = new();

        public OvrPatchLayoutData? Create(OvrPatchData patchData, IList<OvrSectionData> sections)
        {
            var patchLayouts = new List<OvrTextPatchLayoutData>();

            List<OvrTextPatchData> textPatches = patchData.TextPatches.ToList();
            foreach (OvrSectionData section in sections)
            {
                if (section.Length < 0)
                {
                    var offset = 0;
                    foreach (OvrTextPatchData patch in textPatches)
                    {
                        patchLayouts.Add(new OvrTextPatchLayoutData
                        {
                            Offset = section.Offset + offset,
                            Patch = patch
                        });

                        offset += patch.Patch.Length;
                    }

                    textPatches.Clear();
                    break;
                }

                (OvrTextPatchData, int)[] sectionPatches = _packer.Pack(textPatches, section.Length);
                foreach ((OvrTextPatchData patch, int offset) in sectionPatches)
                {
                    textPatches.Remove(patch);

                    patchLayouts.Add(new OvrTextPatchLayoutData
                    {
                        Offset = section.Offset + offset,
                        Patch = patch
                    });
                }
            }

            // if (textPatches.Count > 0)
            //     return null;

            return new OvrPatchLayoutData
            {
                Config = patchData.Config,
                OverlayRange = patchData.OverlayRange,
                TextPatches = patchLayouts.ToArray()
            };
        }
    }
}
