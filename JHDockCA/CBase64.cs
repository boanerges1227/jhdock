using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHDockCA
{
    public class CBase64
    {
        public enum BASE64_FLAG
        {
            BASE64_FLAG_NONE = 0,
            BASE64_FLAG_NOPAD = 1,
            BASE64_FLAG_CRLF = 2
        };

        static char[] Base64EncodingTable = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
            'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g',    'h',
            'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
            'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };

        public string Encode(string content, int length)
        {
            return Encode(content, length, BASE64_FLAG.BASE64_FLAG_NONE);
        }

        public string Encode(string content, int length, BASE64_FLAG flag)
        {

            if (content == null || content == "" || length == 0)
            {
                return string.Empty;
            }

            int lengthAfterEncode = 0;
            {
                Int64 srcLength4 = (Int64)length * 4;
                lengthAfterEncode = (int)(srcLength4 / 3);

                if ((flag & BASE64_FLAG.BASE64_FLAG_NOPAD) == 0)
                    lengthAfterEncode += (int)(length % 3);

                int CRLFCount = lengthAfterEncode / 76 + 1;
                int lastLineCount = lengthAfterEncode % 76;

                if (lastLineCount == 0)
                {
                    if ((lastLineCount % 4) == 0)
                        lengthAfterEncode += 4 - (lastLineCount % 4);
                }

                CRLFCount *= 2;

                if ((flag & BASE64_FLAG.BASE64_FLAG_CRLF) != 0)
                    lengthAfterEncode += CRLFCount;
            }

            if (lengthAfterEncode == 0)
            {
                return string.Empty;
            }

            lengthAfterEncode++;

            encodedCharArray = new char[lengthAfterEncode];

            int writtenCount = 0;
            int len1 = (int)((length / 3) * 4);
            int len2 = len1 / 76;
            int len3 = 19;
            int srcIndex = 0;
            int dstIndex = 0;
            for (int i = 0; i <= len2; i++)
            {
                if (i == len2)
                    len3 = (len1 % 76) / 4;

                for (int j = 0; j < len3; j++)
                {
                    UInt32 cur = 0;
                    for (int n = 0; n < 3; n++)
                    {
                        cur |= content[srcIndex++];
                        cur <<= 8;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        char b = (char)(cur >> 26);
                        encodedCharArray[dstIndex++] = Base64EncodingTable[b];
                        cur <<= 6;
                    }
                }
                writtenCount += len3 * 4;

                if ((flag & BASE64_FLAG.BASE64_FLAG_CRLF) != 0)
                {
                    encodedCharArray[dstIndex++] = '\r';
                    encodedCharArray[dstIndex++] = '\n';
                    writtenCount += 2;
                }
            }

            if ((writtenCount != 0) && (flag & BASE64_FLAG.BASE64_FLAG_CRLF) != 0)
            {
                dstIndex -= 2;
                writtenCount -= 2;
            }

            len2 = ((length % 3) != 0) ? (int)(length % 3 + 1) : 0;
            if (len2 != 0)
            {
                UInt32 cur = 0;
                for (uint n = 0; n < 3; n++)
                {
                    if (n < (length % 3))
                        cur |= content[srcIndex++];
                    cur <<= 8;
                }
                for (int k = 0; k < len2; k++)
                {
                    char b = (char)(cur >> 26);
                    encodedCharArray[dstIndex++] = Base64EncodingTable[b];
                    cur <<= 6;
                }
                writtenCount += len2;
                if ((flag & BASE64_FLAG.BASE64_FLAG_NOPAD) == 0)
                {
                    len3 = (len2 != 0) ? 4 - len2 : 0;
                    for (int j = 0; j < len3; j++)
                    {
                        encodedCharArray[dstIndex++] = '=';
                    }
                    writtenCount += len3;
                }
            }

            encodedCharArray[writtenCount] = '\0';

            return new string(encodedCharArray, 0, writtenCount);
        }

        public string Decode(string source, ref int length)
        {
            // walk the source buffer
            // each four character sequence is converted to 3 bytes
            // CRLFs and =, and any characters not in the encoding table
            // are skiped

            if (source == null || source == "")
                return string.Empty;

            int srcIndex = 0;
            int dstIndex = 0;
            int writtenCount = 0;
            bool isOverflow = false;
            int srcLen = source.Length;
            decodedCharArray = new char[srcLen];

            while (srcIndex < srcLen)
            {
                UInt32 cur = 0;
                int i = 0;
                int bits = 0;

                for (i = 0; i < 4; i++)
                {
                    if (srcIndex >= srcLen)
                        break;
                    int ch = DECODE_BASE64_CHAR(source[srcIndex]);
                    srcIndex++;
                    if (ch == -1)
                    {
                        // skip this char
                        i--;
                        continue;
                    }
                    cur <<= 6;
                    cur |= (UInt32)ch;
                    bits += 6;
                }

                if (!isOverflow && writtenCount + (bits / 8) > srcLen)
                    isOverflow = true;

                // dwCurr has the 3 bytes to write to the output buffer
                // left to right
                cur <<= 24 - bits;
                for (i = 0; i < bits / 8; i++)
                {
                    if (!isOverflow)
                    {
                        decodedCharArray[dstIndex] = (char)((cur & 0x00ff0000) >> 16);
                        dstIndex++;
                    }
                    cur <<= 8;
                    writtenCount++;
                }

            }
            length = writtenCount;
            if (isOverflow)
                return string.Empty;

            return new string(decodedCharArray);
        }

        private int DECODE_BASE64_CHAR(char x)
        {
            return (((x >= 'A') && (x <= 'Z')) ? (x - 'A') : (((x >= 'a') && (x <= 'z')) ? (x - 'a' + 26) : (((x >= '0') && (x <= '9')) ? (x - '0' + 52) : ((x == '+') ? 62 : ((x == '/') ? 63 : -1)))));
        }

        public static int GetEncodeRequiredLength(int length)
        {
            return GetEncodeRequiredLength(length, BASE64_FLAG.BASE64_FLAG_NONE);
        }

        public static int GetEncodeRequiredLength(int length, BASE64_FLAG flag)
        {
            Int64 nSrcLen4 = (Int64)length * 4;
            int retLength = (int)(nSrcLen4 / 3);

            if ((flag & BASE64_FLAG.BASE64_FLAG_NOPAD) == 0)
                retLength += length % 3;

            int CRLFCount = retLength / 76 + 1;
            int lastLineCount = retLength % 76;

            if (lastLineCount != 0)
            {
                if ((lastLineCount % 4) != 0)
                    retLength += 4 - (lastLineCount % 4);
            }

            CRLFCount *= 2;

            if ((flag & BASE64_FLAG.BASE64_FLAG_CRLF) != 0)
                retLength += CRLFCount;

            return retLength;
        }

        private char[] encodedCharArray = null;
        private char[] decodedCharArray = null;
    }
}