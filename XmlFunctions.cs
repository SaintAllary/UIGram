using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace RuslanMessager
{
    internal static class XmlFunctions
    {
        public static UserPreviewSerializableList GetPreviewListInfo() {
            UserPreviewSerializableList userPreviewSerializableList = new UserPreviewSerializableList();

            XmlSerializer formatter = new XmlSerializer(typeof(UserPreviewSerializableList));

            using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate)) {
                userPreviewSerializableList = (UserPreviewSerializableList)formatter.Deserialize(fs);
            }

            return userPreviewSerializableList;
        }

        public static long GetBiggestId() {
            long current = 0;

            foreach (var item in GetPreviewListInfo().userPreviewSerializables) {
                if (item.ID >= current)
                    current = item.ID;
            }

            return current;
        }

        public static DayMessageJournalSerializable GetDayJournal(long chatID, string dateToFind) {
            //if (dateToFind == null)
            //    return null;

            DayMessageJournalSerializable userPreviewSerializableList = null;

            if (File.Exists(CreatePathToJournal(chatID, dateToFind))) {
                XmlSerializer formatter = new XmlSerializer(typeof(DayMessageJournalSerializable));

                using (FileStream fs = new FileStream(CreatePathToJournal(chatID, dateToFind), FileMode.OpenOrCreate)) {
                    userPreviewSerializableList = (DayMessageJournalSerializable)formatter.Deserialize(fs);
                    userPreviewSerializableList.Messages.Sort((x, y) => DateTime.Parse((x.SendDateTime)).CompareTo(DateTime.Parse((y.SendDateTime))));
                }
            }

            return userPreviewSerializableList;
        }

        public static string CreatePathToJournal(long ID, string CurrentDate) {
            return Properties.Resources.UserDataDirPath + "\\" + ID + "\\" + CurrentDate + Properties.Resources.SaveFormatter;
        }

        public static void UpdateDayJournal(IMessage message, long ID) {
            DayMessageJournalSerializable dayMessageJournalSerializable;
            if (File.Exists(CreatePathToJournal(ID, DateTime.Now.ToShortDateString()))) {
                dayMessageJournalSerializable = GetDayJournal(ID, DateTime.Now.ToShortDateString());
                File.Delete(CreatePathToJournal(ID, DateTime.Now.ToShortDateString()));
            }
            else
                dayMessageJournalSerializable = new DayMessageJournalSerializable();

            dayMessageJournalSerializable.Messages.Add(new Message(message));

            XmlSerializer formatter = new XmlSerializer(typeof(DayMessageJournalSerializable));

            using (FileStream fs = new FileStream(CreatePathToJournal(ID, dayMessageJournalSerializable.CurrentDate), FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, dayMessageJournalSerializable);
            }

            GC.Collect();
        }

        public static void WriteDayJournal(Message message, long ID, string OldMsgSendTime) {
            try {
                string local_tmp_path = Properties.Resources.UserDataDirPath + "\\" + ID + "\\" + DateTime.Parse(message.SendDateTime).ToShortDateString() + Properties.Resources.SaveFormatter;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(DayMessageJournalSerializable));

                //MessageBox.Show($"OLD SEND DATE TIME {OldMsgSendTime}\n NEW DATE TIME {message.SendDateTime}");
                string path_to_existing_file_remove = Properties.Resources.UserDataDirPath+"\\"+ID+"\\" +DateTime.Parse(OldMsgSendTime).ToShortDateString() + Properties.Resources.SaveFormatter;
                if (File.Exists(local_tmp_path)) {
                    #region Creating and editing new journal
                    DayMessageJournalSerializable new_chat_journal = XmlFunctions.GetDayJournal(ID, DateTime.Parse(message.SendDateTime).ToShortDateString());
                    File.Delete(local_tmp_path);
                    new_chat_journal.Messages.Add(message);
                    new_chat_journal.Messages.Sort((x, y) => DateTime.Parse((x.SendDateTime)).CompareTo(DateTime.Parse((y.SendDateTime))));
                    //new_chat_journal.Messages.Reverse();
                 
                    using (FileStream fs = new FileStream(CreatePathToJournal(ID, new_chat_journal.CurrentDate), FileMode.OpenOrCreate)) {
                        xmlSerializer.Serialize(fs, new_chat_journal);
                    }
                    //MessageBox.Show(message.MyTurn.ToString());
                    #endregion

                }
                else {
                    DayMessageJournalSerializable new_chat_journal = new DayMessageJournalSerializable();
                    new_chat_journal.Messages.Add(new Message(message));
                    new_chat_journal.CurrentDate = DateTime.Parse(message.SendDateTime).ToShortDateString();

                    XmlSerializer formatter = new XmlSerializer(typeof(DayMessageJournalSerializable));

                    using (FileStream fs = new FileStream(CreatePathToJournal(ID, DateTime.Parse(message.SendDateTime).ToShortDateString()), FileMode.OpenOrCreate)) {
                        formatter.Serialize(fs, new_chat_journal);
                    }
                }

                #region Editing old journal
                if (File.Exists(path_to_existing_file_remove))
                {
                    var oldChat = GetDayJournal(ID, DateTime.Parse(OldMsgSendTime).ToShortDateString());

                    oldChat.Messages.Remove(oldChat.Messages.Find(x => x.SendDateTime == OldMsgSendTime));

                    if (File.Exists(path_to_existing_file_remove))
                        File.Delete(path_to_existing_file_remove);

                    if(oldChat.Messages.Count!=0)
                    using (FileStream fs = new FileStream(CreatePathToJournal(ID, oldChat.CurrentDate), FileMode.OpenOrCreate))
                    {
                        xmlSerializer.Serialize(fs, oldChat);
                    }
                }

                #endregion
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


            GC.Collect();
        }

        public static void UpdatePreviewByMsg(UserPreviewSerializableList userPreviewSerializableList) {
            UserPreviewSerializableList uPSL = userPreviewSerializableList;

            if (File.Exists(Properties.Resources.PreviewSavePath))
                File.Delete(Properties.Resources.PreviewSavePath);

            XmlSerializer formatter = new XmlSerializer(typeof(UserPreviewSerializableList));

            using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, uPSL);
            }
        }


        //public static UserPreviewSerializable GetPreviewListInfo(long ID)
        //{
        //    UserPreviewSerializableList userPreviewSerializableList = new UserPreviewSerializableList();

        //    XmlSerializer formatter = new XmlSerializer(typeof(UserPreviewSerializableList));

        //    using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate))
        //    {
        //        userPreviewSerializableList = (UserPreviewSerializableList)formatter.Deserialize(fs);

        //    }

        //    return userPreviewSerializableList.userPreviewSerializables;
        //}
    }
}