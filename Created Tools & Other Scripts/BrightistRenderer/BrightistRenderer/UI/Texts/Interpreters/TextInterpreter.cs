using BrightistRenderer.Models.Texts.Parsers;
using BrightistRenderer.Models.Texts.Parsers.ControlCodes;
using BrightistRenderer.Models.UI.Texts;

namespace BrightistRenderer.UI.Texts.Interpreters
{
    internal class TextInterpreter
    {
        private readonly PlayerStateData _playerState;

        public TextInterpreter(PlayerStateData playerState)
        {
            _playerState = playerState;
        }

        // TODO: Detect loops
        public IList<CharacterData> Interpret(IList<CharacterData> characters)
        {
            var result = new List<CharacterData>();

            IDictionary<int, bool> flags = _playerState.Flags.ToDictionary();
            var labels = new Dictionary<int, int>();
            int targetLabel = -1;

            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i] is not LabelControlCodeCharacterData && targetLabel >= 0)
                    continue;

                switch (characters[i])
                {
                    case JumpControlCodeCharacterData jump:
                        ProcessLabelJump(labels, jump.LabelIndex, ref i, ref targetLabel);
                        break;

                    case FlagJumpControlCodeCharacterData flagJump:
                        if (flags.TryGetValue(flagJump.Flag, out bool isSet) && isSet)
                            ProcessLabelJump(labels, flagJump.LabelIndex, ref i, ref targetLabel);
                        break;

                    case LabelControlCodeCharacterData label:
                        if (targetLabel == label.LabelIndex)
                            targetLabel = -1;

                        labels[label.LabelIndex] = i;
                        break;

                    case FlagSetControlCodeCharacterData flag:
                        flags[flag.Flag] = true;
                        break;

                    default:
                        result.Add(characters[i]);
                        break;
                }
            }

            return result;
        }

        private void ProcessLabelJump(IDictionary<int, int> labels, int labelIndex, ref int characterIndex, ref int targetLabelIndex)
        {
            if (labels.TryGetValue(labelIndex, out int jumpIndex))
            {
                targetLabelIndex = -1;
                characterIndex = jumpIndex;
                return;
            }

            targetLabelIndex = labelIndex;
        }
    }
}
