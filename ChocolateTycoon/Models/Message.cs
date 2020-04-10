using ChocolateTycoon.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Message
    {
        public static string ErrorMessage { get; private set; }
        public static string Notification { get; private set; }
        public static string MainStorageInfo { get; private set; }



        public static void SetErrorMessage(MessageEnum? received)
        {
            ErrorMessage = string.Empty;
            if (received != null)
                ErrorMessage = PosisionEnumHelper.GetDisplayName(received);
        }

        public static void SetNotification(MessageEnum? received)
        {
            if (received != null)
                Notification = PosisionEnumHelper.GetDisplayName(received);
        }

        public static void SetMainStorageInfo(int succeded, int failed)
        {
            MainStorageInfo = $"{succeded} {PosisionEnumHelper.GetDisplayName(MessageEnum.ProducedInfo)}" +
                              $" {failed} { PosisionEnumHelper.GetDisplayName(MessageEnum.CharityInfo)}";
        }
    }
}