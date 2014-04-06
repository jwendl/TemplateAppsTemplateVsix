using $safeprojectname$.Interfaces;
using $saferootprojectname$.Common;
using SharpRepository.Repository;
using System.Collections.Generic;

namespace $safeprojectname$
{
    /// <summary>
    /// A base business model abstract class to support business objects that 
    /// use SharpRepository or some other repository implementation.
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object.</typeparam>
    /// <typeparam name="TBusinessObject">The type of the business object.</typeparam>
    public abstract class BaseBusinessModel<TDataObject, TBusinessObject>
        where TBusinessObject : IBusinessObject, new()
        where TDataObject : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusinessModel{TDataObject, TBusinessObject}"/> class.
        /// </summary>
        protected BaseBusinessModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusinessModel{TDataObject, TBusinessObject}" /> class.
        /// </summary>
        /// <param name="dataRepository">The data repository.</param>
        protected BaseBusinessModel(IRepository<TDataObject> dataRepository)
        {
            this.DataRepository = dataRepository;
        }

        private void AddBusinessObject(TBusinessObject businessObject)
        {
            BeforeAdd(businessObject);
            BeforeSave(businessObject);

            var dataObject = new TDataObject();
            MapBusinessToDataObject(businessObject, dataObject);
            DataRepository.Add(dataObject);
            AfterAdd(businessObject);
            AfterSave(dataObject, businessObject);

            MapDataToBusinessObject(dataObject, businessObject);
        }

        private void UpdateBusinessObject(TBusinessObject businessObject)
        {
            BeforeUpdate(businessObject);
            BeforeSave(businessObject);

            var dataObject = DataRepository.Get(businessObject.Key);
            MapBusinessToDataObject(businessObject, dataObject);
            DataRepository.Update(dataObject);
            AfterUpdate(businessObject);
            AfterSave(dataObject, businessObject);

            MapDataToBusinessObject(dataObject, businessObject);
        }

        /// <summary>
        /// Happens after reading from the database.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <param name="businessObject">The business object.</param>
        protected virtual void AfterRead(TDataObject dataObject, TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Sets the data repository.
        /// </summary>
        /// <param name="dataRepository">The data repository.</param>
        public void SetDataRepository(IRepository<TDataObject> dataRepository)
        {
            this.DataRepository = dataRepository;
        }

        /// <summary>
        /// Maps from database object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <param name="businessObject">The business object.</param>
        /// <returns></returns>
        protected virtual TBusinessObject MapDataToBusinessObject(TDataObject dataObject, TBusinessObject businessObject)
        {
            if (DataRepository != null)
            {
                AfterRead(dataObject, businessObject);
            }

            return businessObject;
        }

        /// <summary>
        /// Maps from business object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        /// <param name="dataObject">The data object.</param>
        /// <returns></returns>
        protected virtual TDataObject MapBusinessToDataObject(TBusinessObject businessObject, TDataObject dataObject)
        {
            return dataObject;
        }

        /// <summary>
        /// Happens before adding an item.
        /// </summary>
        protected virtual void BeforeAdd(TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Adds the specified data object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        public void Add(TBusinessObject businessObject)
        {
            AddBusinessObject(businessObject);
        }

        /// <summary>
        /// Adds many TBusinessObject items.
        /// </summary>
        /// <param name="businessObjects">The list of TBusinessObject items to add.</param>
        public void AddMany(IEnumerable<TBusinessObject> businessObjects)
        {
            Arg.IsNotNull(() => businessObjects);

            using (var batch = DataRepository.BeginBatch())
            {
                foreach (var businessObject in businessObjects)
                {
                    AddBusinessObject(businessObject);
                }

                batch.Commit();
            }
        }

        /// <summary>
        /// Happens after adding an item.
        /// </summary>
        protected virtual void AfterAdd(TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Happens before updating
        /// </summary>
        protected virtual void BeforeUpdate(TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Updates the specified data object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        public void Update(TBusinessObject businessObject)
        {
            UpdateBusinessObject(businessObject);
        }

        /// <summary>
        /// Updates multiple TBusinessObject items.
        /// </summary>
        /// <param name="businessObjects">The list of business objects to delete.</param>
        public void UpdateMany(IEnumerable<TBusinessObject> businessObjects)
        {
            Arg.IsNotNull(() => businessObjects);

            using (var batch = DataRepository.BeginBatch())
            {
                foreach (var businessObject in businessObjects)
                {
                    UpdateBusinessObject(businessObject);
                }
            }
        }

        /// <summary>
        /// Happens after updating
        /// </summary>
        protected virtual void AfterUpdate(TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Happens before saving the business object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        protected virtual void BeforeSave(TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Happens after saving
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <param name="businessObject">The business object.</param>
        protected virtual void AfterSave(TDataObject dataObject, TBusinessObject businessObject)
        {

        }

        /// <summary>
        /// Deletes the specified data object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        public void Delete(TBusinessObject businessObject)
        {
            DataRepository.Delete(businessObject.Key);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            DataRepository.Delete(id);
        }

        /// <summary>
        /// Deletes many items by primary key.
        /// </summary>
        /// <param name="ids">The list of primary keys.</param>
        public void DeleteMany(IEnumerable<int> ids)
        {
            Arg.IsNotNull(() => ids);

            using (var batch = DataRepository.BeginBatch())
            {
                foreach (var id in ids)
                {
                    DataRepository.Delete(id);
                }

                batch.Commit();
            }
        }

        /// <summary>
        /// Converts from a data object to a business object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <returns>The business object.</returns>
        public TBusinessObject ConvertFrom(TDataObject dataObject)
        {
            return MapDataToBusinessObject(dataObject, new TBusinessObject());
        }

        /// <summary>
        /// Converts from a business object to a data object.
        /// </summary>
        /// <param name="businessObject">The business object.</param>
        /// <returns>The data object.</returns>
        public TDataObject ConvertFrom(TBusinessObject businessObject)
        {
            return MapBusinessToDataObject(businessObject, new TDataObject());
        }

        /// <summary>
        /// Gets or sets the data repository.
        /// </summary>
        protected IRepository<TDataObject> DataRepository { get; set; }
    }
}
