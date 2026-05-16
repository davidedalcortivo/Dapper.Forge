using System.Text;


namespace Dapper.Forge.Core.Models
{
    internal sealed class SqlTemplate
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

        public SqlTemplate(string template, string terminator, string placeholder)
        {
            List<Segment> segments = [];
            StringBuilder sqlBuffer = new();

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
                    Flush(sqlBuffer, segments);
                    segments.Add(new(terminator, false, true));

                    i += j - 1;
                    continue;
                }

                j = 0;

                for (; j < placeholder.Length; j++)
                {
                    if (i + j >= template.Length || template[i + j] != placeholder[j])
                        break;
                }

                if (placeholder.Length > 0 && j >= placeholder.Length)
                {
                    Flush(sqlBuffer, segments);
                    segments.Add(new(placeholder, true, false));

                    i += j - 1;
                    continue;
                }

                sqlBuffer.Append(template[i]);
            }

            Flush(sqlBuffer, segments);
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

        private void Flush(StringBuilder sqlBuffer, List<Segment> segments)
        {
            if (sqlBuffer.Length > 0)
            {
                string literals = sqlBuffer.ToString();

                segments.Add(new(literals, false, false));
                _literalsLength += literals.Length;
                sqlBuffer.Clear();
            }
        }

        private string Render(bool excludeLastTerminator, params object[] args)
        {
            StringBuilder sqlBuffer = new(_literalsLength);
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
                                sqlBuffer.Append(s);
                                break;

                            case StringBuilder sb:
                                sqlBuffer.Append(sb);
                                break;

                            default:
                                sqlBuffer.Append(arg.ToString());
                                break;
                        }
                    }
                    else
                    {
                        sqlBuffer.Append(segment.Text);
                    }
                }
            }

            return sqlBuffer.ToString();
        }
    }
}
