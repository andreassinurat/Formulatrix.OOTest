
#region base .net namespace imports
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region custom namespace imports
using Formulatrix.OOT.UnitTest.Model;
using Formulatrix.OOT.Repository;
#endregion

namespace Formulatrix.OOT.UnitTest
{
    [TestClass]
    public class ItemRepositoryTest
    {
        const int itemTypeNA = 0;
        const int itemTypeJson = 1;
        const int itemTypeXml = 2;
        readonly Employee employee1 = new Employee() { ID = 1, Name = "John Doe", NIK = "EMP001", Tittle = "Programmer" };
        readonly Employee employee2 = new Employee() { ID = 2, Name = "Emmy Chan", NIK = "EMP002", Tittle = "Business Analyst" };
        /// <summary>
        /// register new data employee1.
        /// </summary>
        [TestMethod]
        public void RegisterData()
        {
            #region arrange
            string itemName = employee1.NIK;
            string itemContent = JsonConvert.SerializeObject(employee1);
            #endregion

            #region act
            ItemRepository.Instance.Register(itemName, itemContent, itemTypeJson);
            #endregion

            #region assert
            string result = ItemRepository.Instance.Retrieve(itemName);
            Assert.AreEqual(itemContent, result);
            #endregion
        }
        
        /// <summary>
        /// register new data employee2, with undefined item type
        /// </summary>
        [TestMethod]
        public void RegisterUndefinedItemTypeData()
        {
            #region arrange
            string itemName = employee2.NIK;
            string itemContent = JsonConvert.SerializeObject(employee2);
            #endregion

            #region act
            ItemRepository.Instance.Register(itemName, itemContent, itemTypeNA);
            #endregion

            #region assert
            string result = ItemRepository.Instance.Retrieve(itemName);
            Assert.IsNull(result);
            #endregion
        }

        /// <summary>
        /// retrieve registered data employee1.
        /// must be executed after RegisterData employee1.
        /// </summary>
        [TestMethod]
        public void RetrieveRegisteredData()
        {
            #region arrange
            string itemName = employee1.NIK;
            string itemContent = JsonConvert.SerializeObject(employee1);
            #endregion

            #region act
            string result = ItemRepository.Instance.Retrieve(itemName);
            #endregion

            #region assert
            Assert.AreEqual(itemContent, result);
            #endregion
        }

        /// <summary>
        /// retrieve unregistered data employee2.
        /// </summary>
        [TestMethod]
        public void RetrieveUnregisteredData()
        {
            #region arrange
            string itemName = employee2.NIK;
            #endregion

            #region act
            string result = ItemRepository.Instance.Retrieve(itemName);
            #endregion

            #region assert
            Assert.IsNull(result);
            #endregion
        }

        /// <summary>
        /// retrieve item type for registered data employee1.
        /// </summary>
        [TestMethod]
        public void RetrieveItemTypeRegisteredData()
        {
            #region arrange
            string itemName = employee1.NIK;
            #endregion

            #region act
            int result = ItemRepository.Instance.GetType(itemName);
            #endregion

            #region assert
            Assert.AreEqual(itemTypeJson, result);
            #endregion
        }

        /// <summary>
        /// retrieve item type for unregistered data employee2.
        /// </summary>
        [TestMethod]
        public void RetrieveItemTypeUnregisteredData()
        {
            #region arrange
            string itemName = employee2.NIK;
            #endregion

            #region act
            int result = ItemRepository.Instance.GetType(itemName);
            #endregion

            #region assert
            Assert.AreEqual(0, result);
            #endregion
        }

        /// <summary>
        /// deregister data employee1.
        /// </summary>
        [TestMethod]
        public void DeregisterData()
        {
            #region arrange
            string itemName = employee1.NIK;
            #endregion

            #region act
            ItemRepository.Instance.Deregister(itemName);
            #endregion

            #region assert
            string result = ItemRepository.Instance.Retrieve(itemName);
            Assert.IsNull(result);
            #endregion
        }

    }
}
