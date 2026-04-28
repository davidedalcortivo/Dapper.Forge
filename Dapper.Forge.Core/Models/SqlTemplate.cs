using System.Text;


namespace Dapper.Forge.Core.Models
{
    public sealed class SqlTemplate
    {
        private readonly Segment[] _segments;
        private int _literalsLength;

        private readonly struct Segment
        {
            public readonly string Text;
            public readonly bool IsPlaceholder;
            public readonly bool IsLastTerminator;

            public Segment(string text, bool isPlaceholder, bool isLastTerminator)
            {
                Text = text;
                IsPlaceholder = isPlaceholder;
                IsLastTerminator = isLastTerminator;
            }
        }

        public SqlTemplate()
        {
            _segments = [];
        }

        public SqlTemplate(string template, string terminator)
        {
            List<Segment> segments = [];
            StringBuilder stringBuilder = new();

            for (int i = 0; i < template.Length; i++)
            {
                int j = 0;

                for (; j < terminator.Length; j++)
                {
                    if (i + j >= template.Length || template[i + j] != terminator[j])
                        break;
                }

                if (i + j >= template.Length && terminator.Length > 0 && j >= terminator.Length)
                {
                    Flush(stringBuilder, segments);
                    segments.Add(new(terminator, false, true));

                    i += j - 1;
                    continue;
                }

                if (template[i] == '{' && i + 1 < template.Length && template[i + 1] == '}')
                {
                    Flush(stringBuilder, segments);
                    segments.Add(new(string.Empty, true, false));

                    i++;
                }
                else
                {
                    stringBuilder.Append(template[i]);
                }
            }

            Flush(stringBuilder, segments);
            _segments = [.. segments];
        }

        public string Render(params object[] args)
        {
            return Render(false, args);
        }

        public string RenderWithoutLastTerminator(params object[] args)
        {
            return Render(true, args);
        }

        private void Flush(StringBuilder stringBuilder, List<Segment> segments)
        {
            if (stringBuilder.Length > 0)
            {
                string literals = stringBuilder.ToString();

                segments.Add(new(literals, false, false));
                _literalsLength += literals.Length;
                stringBuilder.Clear();
            }
        }

        private string Render(bool excludeLastTerminator, params object[] args)
        {
            StringBuilder stringBuilder = new(_literalsLength);
            int j = 0;

            for (int i = 0; i < _segments.Length; i++)
            {
                Segment segment = _segments[i];

                if (!excludeLastTerminator || !_segments[i].IsLastTerminator)
                {
                    if (segment.IsPlaceholder && j < args.Length)
                    {
                        object arg = args[j++];

                        switch (arg)
                        {
                            case string s:
                                stringBuilder.Append(s);
                                break;

                            case StringBuilder sb:
                                stringBuilder.Append(sb);
                                break;

                            default:
                                stringBuilder.Append(arg.ToString());
                                break;
                        }
                    }
                    else
                    {
                        stringBuilder.Append(segment.Text);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
