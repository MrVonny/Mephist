using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class MediaFile
    {
        public byte[] Data;
        public string Type;

        public MediaFile(byte[] data, string type)
        {
            Data = data;
            Type = type;
        }

        public MediaFile(string path)
        {
            //ToDo: Реализовать чтения из файловой системы
            //https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_MIME-%D1%82%D0%B8%D0%BF%D0%BE%D0%B2#image
        }
    }
}
