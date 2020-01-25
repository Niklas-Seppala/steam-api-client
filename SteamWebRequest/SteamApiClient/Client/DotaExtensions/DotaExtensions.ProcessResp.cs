using System.Collections.Generic;
using System.Text;

namespace SteamApiClient.Dota
{
    public static partial class DotaExtensions
    {
        private static string ProcessHeroStatsResponse(string raw)
        {
            var bracketStack = new Stack<bool>();
            var sb = new StringBuilder("[");

            bool firstBracketPassed = false;
            for (int i = 1; i < raw.Length; i++)
            {
                if (raw[i] == '{')
                {
                    if (firstBracketPassed)
                    {
                        sb.Append(raw[i]);
                        bracketStack.Push(true);
                        do
                        {
                            i++;
                            if (raw[i] != '}')
                            {
                                if (raw[i] == '{')
                                {
                                    bracketStack.Push(true);
                                }
                                sb.Append(raw[i]);
                            }
                            else
                            {
                                sb.Append(raw[i]);
                                bracketStack.Pop();
                            }
                        } while (bracketStack.Count != 0);
                        sb.Append(',');
                    }
                    else
                    {
                        firstBracketPassed = true;
                    }
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return sb.ToString();
        }

        private static string ProcessItemInfoResponse(string raw)
        {
            var bracketStack = new Stack<bool>();
            var sb = new StringBuilder("[");

            bool firstBracketPassed = false;
            for (int i = 1; i < raw.Length; i++)
            {
                if (raw[i] == '{')
                {
                    if (firstBracketPassed)
                    {
                        sb.Append(raw[i]);
                        bracketStack.Push(true);
                        do
                        {
                            i++;
                            if (raw[i] != '}')
                            {
                                sb.Append(raw[i]);
                            }
                            else
                            {
                                bracketStack.Pop();
                            }
                        } while (bracketStack.Count != 0);
                        sb.Append("},");
                    }
                    else
                    {
                        firstBracketPassed = true;
                    }
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

        public static string ProcessHeroInfoResponse(string raw)
        {
            var bracketStack = new Stack<bool>();
            var sBuilder = new StringBuilder("[");

            for (int i = 1; i < raw.Length - 1; i++)
            {
                if (raw[i] == '{')
                {
                    sBuilder.Append(raw[i]);
                    bracketStack.Push(true);
                    do
                    {
                        i++;
                        if (raw[i] != '}')
                        {
                            sBuilder.Append(raw[i]);
                        }
                        else
                        {
                            bracketStack.Pop();
                        }
                    } while (bracketStack.Count != 0);
                    sBuilder.Append("},");
                }
            }
            sBuilder.Remove(sBuilder.Length - 1, 1);
            sBuilder.Append("]");
            return sBuilder.ToString();
        }
    }
}
