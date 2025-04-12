using BrightisRendererV2.Models.Metrics;
using BrightisRendererV2.Models.Texts.Layouts;
using BrightisRendererV2.Models.Texts.Parsers;

namespace BrightisRendererV2.MetricStrategies;

internal class PunctuationMetricStrategy : MetricStrategy
{
    private const ushort Space = 0x8140;
    private const ushort Comma = 0x8143;
    private const ushort Dot = 0x8144;
    private const ushort QuestionMark = 0x8148;
    private const ushort ExclamationPoint = 0x8149;
    private const ushort Zero = 0x824f;
    private const ushort Nine = 0x8258;
    private const ushort QuotationMark = 0x818D;
    private const ushort UpperCaseA = 0x8260;
    private const ushort UpperCaseZ = 0x8279;

    public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
    {
        var result = new List<MetricDetailData>();

        foreach (TextLayoutLineData layoutLine in layout.Lines)
        {
            var isLineStart = true;
            for (var i = 0; i < layoutLine.Characters.Count; i++)
            {
                if (!layoutLine.Characters[i].Character.IsVisible)
                    continue;

                if (TryGetCharacter(layoutLine.Characters[i].Character, out ushort character))
                {
                    switch (character)
                    {
                        case Dot:
                            ValidateDot(layoutLine, ref i, isLineStart, result);
                            break;

                        case QuestionMark:
                            ValidateQuestionMark(layoutLine, ref i, isLineStart, result);
                            break;

                        case ExclamationPoint:
                            ValidateExclamationPoint(layoutLine, ref i, isLineStart, result);
                            break;

                        case Comma:
                            ValidateComma(layoutLine, ref i, isLineStart, result);
                            break;
                    }
                }

                isLineStart = false;
            }
        }

        return result;
    }

    private void ValidateDot(TextLayoutLineData line, ref int index, bool isLineStart, IList<MetricDetailData> details)
    {
        bool isEllipses = TryGetCharacter(line, index + 1, out ushort nextCharacter) && nextCharacter is Dot
            && TryGetCharacter(line, index + 2, out nextCharacter) && nextCharacter is Dot;

        if (isEllipses)
        {
            ValidateEllipses(line, ref index, isLineStart, details);
            return;
        }

        // Dot at start of line
        if (isLineStart)
        {
            // Dot invalid if followed by any amount of punctuations
            int origIndex = index;

            while (TryGetCharacter(line, index + 1, out nextCharacter))
            {
                if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                    break;

                index++;
            }

            details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidDot));
            return;
        }

        if (TryGetCharacter(line, index - 1, out ushort previousCharacter))
        {
            if (previousCharacter is Space or QuotationMark)
            {
                details.Add(CreateMetricDetail(line.Characters[index - 1], line.Characters[index], MetricDetailType.InvalidDot));
                return;
            }
        }

        if (TryGetCharacterData(line, index + 1, out CharacterData? nextCharacterData))
        {
            if (!nextCharacterData.IsVisible)
                return;

            if (TryGetCharacter(nextCharacterData, out nextCharacter))
            {
                if (nextCharacter is Space or QuotationMark)
                    return;

                if (nextCharacter is Dot or ExclamationPoint or QuestionMark or Comma)
                {
                    // Dot invalid if followed by any amount of punctuations
                    int origIndex = index;

                    index++;
                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 0)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidDot));

                    return;
                }
            }

            // Dot is invalid if it's not followed by a space, or line break
            details.Add(CreateMetricDetail(line.Characters[index], line.Characters[index + 1], MetricDetailType.InvalidDot));
        }
    }

    private void ValidateEllipses(TextLayoutLineData line, ref int index, bool isLineStart, IList<MetricDetailData> details)
    {
        index += 2;
        if (TryGetCharacter(line, index - 3, out ushort previousCharacter))
        {
            if (previousCharacter is Space or QuotationMark)
            {
                details.Add(CreateMetricDetail(line.Characters[index - 3], line.Characters[index], MetricDetailType.InvalidEllipses));
                return;
            }
        }

        if (TryGetCharacterData(line, index + 1, out CharacterData? nextCharacterData))
        {
            if (!nextCharacterData.IsVisible)
                return;

            if (TryGetCharacter(nextCharacterData, out ushort nextCharacter))
            {
                if (nextCharacter is QuotationMark)
                    return;

                if (nextCharacter is Space)
                {
                    if (isLineStart)
                    {
                        details.Add(CreateMetricDetail(line.Characters[index - 2], line.Characters[index + 1], MetricDetailType.InvalidEllipses));
                        return;
                    }

                    if (TryGetCharacterData(line, index + 2, out nextCharacterData))
                    {
                        index++;

                        if (!nextCharacterData.IsVisible)
                        {
                            details.Add(CreateMetricDetail(line.Characters[index - 3], line.Characters[index + 1], MetricDetailType.InvalidEllipses));
                            return;
                        }

                        if (TryGetCharacter(nextCharacterData, out nextCharacter))
                        {
                            if (nextCharacter is >= UpperCaseA and <= UpperCaseZ)
                                return;

                            details.Add(CreateMetricDetail(line.Characters[index - 3], line.Characters[index + 1], MetricDetailType.InvalidEllipses, MetricDetailLevel.Warn));
                            return;
                        }

                        return;
                    }
                }

                if (nextCharacter is QuestionMark or ExclamationPoint)
                {
                    // Ellipses valid if followed by a single exclamation point or question mark
                    int origIndex = index++;
                    var threshold = 1;

                    if (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is ExclamationPoint)
                        {
                            index++;
                            threshold++;
                        }
                        else if (nextCharacter is not Space)
                        {
                            details.Add(CreateMetricDetail(line.Characters[origIndex - 2], line.Characters[index + 1], MetricDetailType.InvalidExclamationPoint));
                            return;
                        }
                    }

                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > threshold)
                        details.Add(CreateMetricDetail(line.Characters[origIndex - 2], line.Characters[index], MetricDetailType.InvalidEllipses));

                    return;
                }

                if (nextCharacter is Dot or Comma)
                {
                    // Ellipses invalid if followed by any amount punctuation
                    int origIndex = index;

                    index++;
                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 0)
                        details.Add(CreateMetricDetail(line.Characters[origIndex - 2], line.Characters[index], MetricDetailType.InvalidEllipses));

                    return;
                }
            }

            // Ellipses is invalid if it's not followed by a space, or line break
            if (!isLineStart)
                details.Add(CreateMetricDetail(line.Characters[index - 2], line.Characters[index + 1], MetricDetailType.InvalidEllipses));
        }
    }

    private void ValidateQuestionMark(TextLayoutLineData line, ref int index, bool isLineStart, IList<MetricDetailData> details)
    {
        // Question mark at start of line
        if (isLineStart)
        {
            // Question mark invalid if followed by any amount of punctuations
            int origIndex = index;

            while (TryGetCharacter(line, index + 1, out ushort nextCharacter))
            {
                if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                    break;

                index++;
            }

            details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidQuestionMark));
            return;
        }

        if (TryGetCharacter(line, index - 1, out ushort previousCharacter))
        {
            if (previousCharacter is Space or QuotationMark)
            {
                details.Add(CreateMetricDetail(line.Characters[index - 1], line.Characters[index], MetricDetailType.InvalidQuestionMark));
                return;
            }
        }

        if (TryGetCharacterData(line, index + 1, out CharacterData? nextCharacterData))
        {
            if (!nextCharacterData.IsVisible)
                return;

            if (TryGetCharacter(nextCharacterData, out ushort nextCharacter))
            {
                if (nextCharacter is Space or QuotationMark)
                    return;

                if (nextCharacter is ExclamationPoint)
                {
                    // Question mark valid if followed by a single exclamation point
                    int origIndex = index++;

                    if (TryGetCharacter(line, index + 1, out nextCharacter) && nextCharacter is not Space and not QuotationMark)
                    {
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index + 1], MetricDetailType.InvalidQuestionMark));
                        return;
                    }

                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 1)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidQuestionMark));

                    return;
                }

                if (nextCharacter is Dot or QuestionMark or Comma)
                {
                    // Question mark invalid if followed by any amount of punctuations
                    int origIndex = index;

                    index++;
                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 0)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidQuestionMark));

                    return;
                }
            }

            // Question mark is invalid if it's not followed by a space, or line break
            details.Add(CreateMetricDetail(line.Characters[index], line.Characters[index + 1], MetricDetailType.InvalidQuestionMark));
        }
    }

    private void ValidateExclamationPoint(TextLayoutLineData line, ref int index, bool isLineStart, IList<MetricDetailData> details)
    {
        // Exclamation point at start of line
        if (isLineStart)
        {
            // Exclamation point invalid if followed by any amount of punctuations
            int origIndex = index;

            while (TryGetCharacter(line, index + 1, out ushort nextCharacter))
            {
                if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                    break;

                index++;
            }

            details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidExclamationPoint));
            return;
        }

        if (TryGetCharacter(line, index - 1, out ushort previousCharacter))
        {
            if (previousCharacter is Space or QuotationMark)
            {
                details.Add(CreateMetricDetail(line.Characters[index - 1], line.Characters[index], MetricDetailType.InvalidExclamationPoint));
                return;
            }
        }

        if (TryGetCharacterData(line, index + 1, out CharacterData? nextCharacterData))
        {
            if (!nextCharacterData.IsVisible)
                return;

            if (TryGetCharacter(nextCharacterData, out ushort nextCharacter))
            {
                if (nextCharacter is Space or QuotationMark)
                    return;

                if (nextCharacter is ExclamationPoint)
                {
                    // Exclamation point valid if followed by a single exclamation point
                    int origIndex = index++;

                    if (TryGetCharacter(line, index + 1, out nextCharacter) && nextCharacter is not Space)
                    {
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index + 1], MetricDetailType.InvalidExclamationPoint));
                        return;
                    }

                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex == 1)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidExclamationPoint, MetricDetailLevel.Warn));

                    if (index - origIndex > 1)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidExclamationPoint));

                    return;
                }

                if (nextCharacter is Dot or QuestionMark or Comma)
                {
                    // Exclamation point invalid if followed by any amount of punctuations
                    int origIndex = index;

                    index++;
                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 0)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidExclamationPoint));

                    return;
                }
            }

            // Exclamation point is invalid if it's not followed by a space, or line break
            details.Add(CreateMetricDetail(line.Characters[index], line.Characters[index + 1], MetricDetailType.InvalidExclamationPoint));
        }
    }

    private void ValidateComma(TextLayoutLineData line, ref int index, bool isLineStart, IList<MetricDetailData> details)
    {
        // Comma at start of line
        if (isLineStart)
        {
            // Comma invalid if followed by any amount of punctuations
            int origIndex = index;

            while (TryGetCharacter(line, index + 1, out ushort nextCharacter))
            {
                if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                    break;

                index++;
            }

            details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidComma));
            return;
        }

        var isPreviousNumber = false;
        if (TryGetCharacter(line, index - 1, out ushort previousCharacter))
        {
            // Comma is invalid if it's preceded only by space, or punctuation (shouldn't happen)
            if (previousCharacter is Space or QuotationMark)
            {
                details.Add(CreateMetricDetail(line.Characters[index - 1], line.Characters[index], MetricDetailType.InvalidComma));
                return;
            }

            isPreviousNumber = previousCharacter is >= Zero and <= Nine;
        }

        if (TryGetCharacterData(line, index + 1, out CharacterData? nextCharacterData))
        {
            if (!nextCharacterData.IsVisible)
                return;

            if (TryGetCharacter(nextCharacterData, out ushort nextCharacter))
            {
                if (nextCharacter is Space or QuotationMark)
                    return;

                if (isPreviousNumber && nextCharacter is >= Zero and <= Nine)
                    return;

                if (nextCharacter is Dot or ExclamationPoint or QuestionMark or Comma)
                {
                    // Comma invalid if followed by any amount of punctuations
                    int origIndex = index;

                    index++;
                    while (TryGetCharacter(line, index + 1, out nextCharacter))
                    {
                        if (nextCharacter is not Dot and not ExclamationPoint and not QuestionMark and not Comma)
                            break;

                        index++;
                    }

                    if (index - origIndex > 0)
                        details.Add(CreateMetricDetail(line.Characters[origIndex], line.Characters[index], MetricDetailType.InvalidComma));

                    return;
                }
            }

            // Comma is invalid if it's not followed by a space, number (if preceded by a number), or line break
            details.Add(CreateMetricDetail(line.Characters[index], line.Characters[index + 1], MetricDetailType.InvalidComma));
            return;
        }

        // Comma is invalid if it's not followed by a space, number (if preceded by a number), or line break
        details.Add(CreateMetricDetail(line.Characters[index], MetricDetailType.InvalidComma));
    }
}