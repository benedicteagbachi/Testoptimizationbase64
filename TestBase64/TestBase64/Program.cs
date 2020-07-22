using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace TestBase64
{
    public static class Base64
    {
        public static readonly char[] alphabet = new[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V',
                                                       'W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r',
                                                        's', 't','u','v','w','x','y','z','0','1','2','3','4','5','6','7','8','9','/','+'};
        public static string Encode(byte[] source)
        {
                 
            StringBuilder encodedCaracters = new StringBuilder();
            int length = source.Length;
            for (int i = 0; i < length; i+=3)
            {
                
                //Dans chaque triplet on a 3 bytes
                byte b1 = source[i];
                byte b2 = source[i+1];
                byte b3 = source[i+2];

                //Former les 4 groupes de 6 bits a partir des bytes en decalant soit a gauche soit a droite les bits
                byte sixBitgroup1 = (byte)(b1 >> 2);
                                
                byte sixBitgroup2 = (byte)(((b1 & 0x03) << 4) | ((b2 & 0xF0) >> 4));

                byte sixBitgroup3 = (byte)(((b2 & 0x0F) << 2) | ((b3 & 0xC0) >> 6));

                byte sixBitgroup4 = (byte)(b3 & 0x3F); ;

                //Recuperer les caracteres en base 64 a partir des index
                encodedCaracters.Append(alphabet[sixBitgroup1]);
                encodedCaracters.Append(alphabet[sixBitgroup2]);
                encodedCaracters.Append(alphabet[sixBitgroup3]);
                encodedCaracters.Append(alphabet[sixBitgroup4]);

            }

            return encodedCaracters.ToString();
        }

        public static void Main(String[] args)
        {
            
            byte[] source = new byte[] { 0x6A, 0x77, 0xC4 };

            var time = System.Diagnostics.Stopwatch.StartNew();
            String base64caracters = Base64.Encode(source);
            time.Stop();

            Console.WriteLine("Encoding in Base64 result: " + base64caracters);
            Console.WriteLine($"Base64 Anne Solution time: {time.Elapsed.TotalMilliseconds} ms" );

            time.Restart();
            Convert.ToBase64String(source);
            time.Stop();
            Console.WriteLine($"base64 solution .Net time: {time.Elapsed.TotalMilliseconds} ms" );
        }
    }
}
