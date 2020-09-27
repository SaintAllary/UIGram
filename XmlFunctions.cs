using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public static DayMessageJournalSerializable GetDayJournal(long chatID, string dateToFind)
        {

            DayMessageJournalSerializable userPreviewSerializableList = null;

            if (File.Exists(CreatePathToJournal(chatID, dateToFind)))
            {

                XmlSerializer formatter = new XmlSerializer(typeof(DayMessageJournalSerializable));

                using (FileStream fs = new FileStream(CreatePathToJournal(chatID, dateToFind), FileMode.OpenOrCreate))
                {
                    userPreviewSerializableList = (DayMessageJournalSerializable)formatter.Deserialize(fs);
                }



            }


            return userPreviewSerializableList;

        }

        public static string CreatePathToJournal(long ID, string CurrentDate)
        {
            return Properties.Resources.UserDataDirPath + "\\" + ID + "\\" + CurrentDate + Properties.Resources.SaveFormatter;
        }


        public static void UpdateDayJournal(IMessage message, long ID)
        {


            DayMessageJournalSerializable dayMessageJournalSerializable;
            if (File.Exists(CreatePathToJournal(ID, DateTime.Now.ToShortDateString())))
            {

                dayMessageJournalSerializable = GetDayJournal(ID, DateTime.Now.ToShortDateString());
                File.Delete(CreatePathToJournal(ID, DateTime.Now.ToShortDateString()));
            }

            else
                dayMessageJournalSerializable = new DayMessageJournalSerializable();


            dayMessageJournalSerializable.Messages.Add(new Message(message));


            XmlSerializer formatter = new XmlSerializer(typeof(DayMessageJournalSerializable));


            using (FileStream fs = new FileStream(CreatePathToJournal(ID, dayMessageJournalSerializable.CurrentDate), FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, dayMessageJournalSerializable);



            }

            GC.Collect();

        }


    }
}
