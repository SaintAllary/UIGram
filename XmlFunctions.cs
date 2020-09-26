using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuslanMessager
{
    static class XmlFunctions
    {
        public static UserPreviewSerializableList GetPreviewListInfo()
        {
            UserPreviewSerializableList userPreviewSerializableList = new UserPreviewSerializableList();

            XmlSerializer formatter = new XmlSerializer(typeof(UserPreviewSerializableList));

            using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate))
            {
                userPreviewSerializableList = (UserPreviewSerializableList)formatter.Deserialize(fs);

              
            }

            return userPreviewSerializableList;
        }
        public static long GetBiggestId()
        {
            long current = 0;
    

            foreach (var item in GetPreviewListInfo().userPreviewSerializables)
            {
                if (item.ID >= current)
                    current = item.ID;

            }
             
              


            return current;
        }

       
    }
}
