namespace BrightistRenderer.Models.Metrics
{
    public enum MetricDetailType
    {
        InvalidOpenTag,
        InvalidCloseTag,

        InvalidCharacter,

        InvalidComma,
        InvalidExclamationPoint,
        InvalidDot,
        InvalidEllipses,
        InvalidQuestionMark,

        ContinuousSpaces,
        MisplacedSpace,
        
        TooManyLines,
        LineTooLong
    }
}
