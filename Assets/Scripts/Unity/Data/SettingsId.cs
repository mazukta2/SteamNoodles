using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnity.Assets.Scripts.Unity.Data
{
    public struct SettingsId
    {
        public string Name { get; private set; }

        public ulong GetId()
        {
            return CalculateHash(Name);
        }

        public static ulong CalculateHash(string str)
        {
            unchecked
            {
                ulong hash1 = (5381 << 16) + 5381;
                ulong hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                var result = hash1 + (hash2 * 1566083941);
                if (result == 0)
                    throw new Exception("wrong cache id for: " + str);
                return result;
            }
        }
    }
}
