/* --------------------------------------------
 * Author       : Andreas
 * Create date  : 28th Dec 2018
 * File Name    : IItemRepository.cs
 * Purpose      : Repository Interface
 * --------------------------------------------*/

#region base .net namespace imports
#endregion

#region custom namespace imports
#endregion

namespace Formulatrix.OOT.Repository
{
    interface IRepository
    {
        /// <summary>
        /// store an item to the repository
        /// </summary>
        /// <param name="itemName">unique item name to be stored</param>
        /// <param name="itemContent">item content in json or xml format string</param>
        /// <param name="itemType">item type 1: json, 2: xml</param>
        void Register(string itemName, string itemContent, int itemType);

        /// <summary>
        /// retrieve an item from repository
        /// </summary>
        /// <param name="itemName">unique item name</param>
        /// <returns>item content in json or xml format string</returns>
        string Retrieve(string itemName);
        
        /// <summary>
        /// retrieve the type of the item
        /// </summary>
        /// <param name="itemName">unique item name</param>
        /// <returns>item type 1: json, 2: xml</returns>
        int GetType(string itemName);
        
        /// <summary>
        /// remove an item from the repository
        /// </summary>
        /// <param name="itemName">unique item name</param>
        void Deregister(string itemName);
        
        /// <summary>
        /// initialize the repository
        /// </summary>
        void Initialize();
    }
}
