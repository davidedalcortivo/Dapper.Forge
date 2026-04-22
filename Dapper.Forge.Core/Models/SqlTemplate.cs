using System.Text;


namespace Dapper.Forge.Core.Models
{
    public sealed class SqlTemplate
    {
        private readonly Segment[] _segments;
        private readonly int _literalsLength;

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
            StringBuilder current = new();

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
                    if (current.Length > 0)
                    {
                        string lit = current.ToString();
                        segments.Add(new(lit, false, false));
                        _literalsLength += lit.Length;
                        current.Clear();
                    }

                    segments.Add(new(terminator, false, true));

                    i += j - 1;
                    continue;
                }

                if (template[i] == '{' && i + 1 < template.Length && template[i + 1] == '}')
                {
                    if (current.Length > 0)
                    {
                        string lit = current.ToString();
                        segments.Add(new(lit, false, false));
                        _literalsLength += lit.Length;
                        current.Clear();
                    }

                    segments.Add(new(string.Empty, true, false));

                    i++;
                }
                else
                {
                    current.Append(template[i]);
                }
            }

            if (current.Length > 0)
            {
                string lit = current.ToString();
                segments.Add(new(lit, false, false));
                _literalsLength += lit.Length;
            }

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

        private string Render(bool excludeLastTerminator, params object[] args)
        {
            StringBuilder sb = new(_literalsLength);
            int argIndex = 0;

            for (int i = 0; i < _segments.Length; i++)
            {
                Segment seg = _segments[i];

                if (!excludeLastTerminator || !_segments[i].IsLastTerminator)
                {
                    if (seg.IsPlaceholder && argIndex < args.Length)
                    {
                        object arg = args[argIndex++];

                        switch (arg)
                        {
                            case string s:
                                sb.Append(s);
                                break;

                            case StringBuilder sbExternal:
                                sb.Append(sbExternal);
                                break;

                            default:
                                sb.Append(arg.ToString());
                                break;
                        }
                    }
                    else
                    {
                        sb.Append(seg.Text);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
