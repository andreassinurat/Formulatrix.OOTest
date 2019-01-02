
#region base .net namespace imports
#endregion

#region custom namespace imports
#endregion

namespace Formulatrix.OOT.Common
{
    public static class CommonFunction
    {
        #region constant values
        public const string messageEmptyItemNameOrContent = "Register data is failed. Item name and item content can't be empty.";
        public const string messageUndefinedItemType = "Register data is failed. Undefined item type.";
        public const string messageInvalidItemContent = "Register data is failed. Invalid item content.";
        public const string messageItemExists = "Register data is failed. Invalid item {0} already exists.";
        public const string messageItemDoesNotExists = "Item {0} does not exists.";
        public const string messageSuccessRegisterItem = "Register Item {0} is success.";
        public const string messageSuccessDeregisterItem = "Deregister Item {0} is success.";
        #endregion

        #region public method
        public static bool IsValidJsonFormat(string jsonFormatString)
        {
            bool result = true;
            //to do : validate json format
            return result;
        }

        public static bool IsValidXmlFormat(string xmlFormatString)
        {
            bool result = true;
            //to do : validate json format

            return result;
        }
        #endregion
    }
}
