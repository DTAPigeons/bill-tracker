using BillTrackerAPI.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.MongoDB
{
    public abstract class MongoService<TModel> where TModel:BaseEntity
    {
        private IMongoCollection<TModel> collection;

        public MongoService(IMongoDBSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<TModel>(collectionName);

        }

        public async Task<List<TModel>> GetAll()
        {
            return await GetAll(item => true);
        }

        public async Task<List<TModel>> GetAll(Expression<Func<TModel, bool>> filterExpression)
        {
            var data = await collection.Find(filterExpression).ToListAsync();
            return data;
        }

        public async Task<TModel> GetById(string id)
        {
            var data = await collection.Find(item => item.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<TModel> Create(TModel item)
        {
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<TModel> Update(string id, TModel itemIn)
        {
            await collection.ReplaceOneAsync(item => item.Id == id, itemIn);
            return itemIn;
        }

        public async Task<TModel> Delete(string id)
        {
            var userToDelete = await GetById(id);
            await collection.DeleteOneAsync(item => item.Id == id);

            return userToDelete;
        }

        protected void SetCollection(IMongoDBSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<TModel>(collectionName);
        }
    }
}
