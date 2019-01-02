
#region base .net namespace imports
using System;
using System.Collections.Generic;
#endregion

#region custom namespace imports
using Formulatrix.OOT.Common;
using Formulatrix.OOT.Common.Enums;
using Formulatrix.OOT.Common.Entities;
#endregion


namespace Formulatrix.OOT.Repository
{
    public sealed class ItemRepository : IRepository
    {
        #region constructor
        private ItemRepository()
        {
            _cache = new Dictionary<string, Item>();
        }
        #endregion

        #region properties
        private static Dictionary<string, Item> _cache;
        private static readonly object _lock = new object();
        private static ItemRepository _instance;

        public static ItemRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ItemRepository();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region implement IRepository
        public void Register(string itemName, string itemContent, int itemType)
        {
            try
            {
                ResultStatus resultStatus = new ResultStatus();
                resultStatus = CheckIsValidToRegisterItem(itemName, itemContent, itemType);

                if (resultStatus.IsSuccess)
                {
                    Item itemToRegister = new Item()
                    {
                        ItemName = itemName,
                        ItemType = itemType,
                        ItemContent = itemContent
                    };

                    _cache.Add(itemToRegister.ItemName, itemToRegister);
                    resultStatus.SetSuccessStatus(string.Format(CommonFunction.messageSuccessRegisterItem, itemName));
                }

                //to do : save resultStatus to log
            }
            catch (Exception)
            {
                //to do : write exception to log
            }
        }

        public string Retrieve(string itemName)
        {
            string result = string.Empty;

            try
            {
                if (!_cache.ContainsKey(itemName))
                    throw new ApplicationException(String.Format(CommonFunction.messageItemDoesNotExists, itemName));

                Item registeredItem = _cache[itemName];
                result = registeredItem.ItemContent;
            }
            catch (Exception)
            {
                //to do : write exception to log
            }

            return result;
        }

        public int GetType(string itemName)
        {
            int result = (int)ItemType.NA;

            try
            {
                if (!_cache.ContainsKey(itemName))
                    throw new ApplicationException(String.Format(CommonFunction.messageItemDoesNotExists, itemName));

                Item registeredItem = _cache[itemName];
                result = registeredItem.ItemType;
            }
            catch (Exception)
            {
                //to do : write exception to log
            }

            return result;
        }

        public void Deregister(string itemName)
        {
            try
            {
                ResultStatus resultStatus = new ResultStatus();

                if (!_cache.ContainsKey(itemName))
                    throw new ApplicationException(String.Format(CommonFunction.messageItemDoesNotExists, itemName));
                
                 _cache.Remove(itemName);
                resultStatus.SetSuccessStatus(string.Format(CommonFunction.messageSuccessRegisterItem, itemName));

                //to do : remove resultStatus to log
            }
            catch (Exception)
            {
                //to do : write exception to log
            }
        }

        public void Initialize() { }
        #endregion

        #region private method
        private ResultStatus CheckIsValidToRegisterItem(string itemName, string itemContent, int itemType)
        {
            ResultStatus resultStatus = new ResultStatus();
            resultStatus.SetSuccessStatus(); 

            if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(itemContent))
                return new ResultStatus() { MessageText = CommonFunction.messageEmptyItemNameOrContent };

            if (itemType != (int)ItemType.JSON && itemType != (int)ItemType.XML)
                return new ResultStatus() { MessageText = CommonFunction.messageUndefinedItemType };

            bool isValidItemContent = itemType == (int)ItemType.JSON ? 
                CommonFunction.IsValidJsonFormat(itemContent) : CommonFunction.IsValidXmlFormat(itemContent);

            if (!isValidItemContent)
                return new ResultStatus() { MessageText = CommonFunction.messageInvalidItemContent };

            if (_cache.ContainsKey(itemName))
                resultStatus = new ResultStatus() { MessageText = string.Format(CommonFunction.messageItemExists, itemName) };
               
            return resultStatus;
        }
        #endregion
    }
}
