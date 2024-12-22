using TranslationToSource.Models;

namespace TranslationToSource
{
    /// <summary>
    /// Parser for command line arguments and parameters.
    /// </summary>
    class OptionsParser
    {
        private static readonly string[] Commands = { "-h", "-s", "-ci", "-cs" };

        private string? _parsedCommand;
        private int _parameterCount;

        /// <summary>
        /// Parse all command line arguments.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns></returns>
        public ParsedOptions Parse(string[] args)
        {
            if (args.Length <= 0)
                return new ParsedOptions(true);

            var values = new Dictionary<string, List<string>?>();

            foreach (string arg in args)
            {
                if (_parsedCommand == null)
                {
                    ParseCommand(arg);

                    if (_parsedCommand == "-h")
                    {
                        values["-h"] = null;
                        _parsedCommand = null;
                    }
                }
                else
                {
                    string? parsedCommand = _parsedCommand;
                    if (!values.TryGetValue(parsedCommand, out List<string>? valueList))
                        values[parsedCommand] = valueList = new List<string>();

                    string parameter = ParseParameter(arg);
                    valueList!.Add(parameter);
                }
            }

            return new ParsedOptions(
                values.GetValueOrDefault("-s")?.FirstOrDefault(),
                values.GetValueOrDefault("-ci")?.FirstOrDefault(),
                values.GetValueOrDefault("-cs")?.FirstOrDefault(),
                values.ContainsKey("-h"));
        }

        #region Private methods

        /// <summary>
        /// Parse a command argument.
        /// </summary>
        /// <param name="arg">Argument to parse.</param>
        private void ParseCommand(string arg)
        {
            EnsureArgumentCommand(arg);

            _parameterCount = 0;
            _parsedCommand = arg;
        }

        /// <summary>
        /// Parse a command parameter.
        /// </summary>
        /// <param name="arg">Argument to parse.</param>
        /// <returns>The parsed command parameter.</returns>
        private string ParseParameter(string arg)
        {
            _parameterCount++;
            switch (_parsedCommand)
            {
                case "-a":
                    EnsureArgumentNoCommand(arg);
                    if (_parameterCount == 3)
                        _parsedCommand = null;
                    return arg;
                case "-s":
                    EnsureArgumentNoCommand(arg);
                    _parsedCommand = null;
                    return arg;
                case "-ci":
                    EnsureArgumentNoCommand(arg);
                    _parsedCommand = null;
                    return arg;
                case "-cs":
                    EnsureArgumentNoCommand(arg);
                    _parsedCommand = null;
                    return arg;
                default:
                    throw new InvalidOperationException($"Unknown option '{_parsedCommand}'.");
            }
        }

        /// <summary>
        /// Ensures that the argument is no known command.
        /// </summary>
        /// <param name="arg">The value to ensure.</param>
        private void EnsureArgumentNoCommand(string arg)
        {
            if (arg == null || Commands.Contains(arg))
                throw new InvalidOperationException($"'{arg}' is no valid parameter.");
        }

        /// <summary>
        /// Ensures that the argument is a known command.
        /// </summary>
        /// <param name="arg">The value to ensure.</param>
        private void EnsureArgumentCommand(string arg)
        {
            if (arg == null || !Commands.Contains(arg))
                throw new InvalidOperationException($"'{arg}' is no valid command.");
        }

        #endregion
    }
}
