using System;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Mine.Models;

using Xamarin.Forms;
using System.Collections.Generic;

namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Write new item to the table in the Database and return the ID of what was written 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            if (item == null)
            {
                return false;
            }

            // Add a new item to the table in the Database and get ID of the insertion
            var result = await Database.InsertAsync(item);
            if (result == 0 )
            {
                return false;
            }

            return true;
        }

        public Task<bool> UpdateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// From the Database, get the corresponding item to the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            // Call the Database to read the ID
            // Using Linq syntax, find the first record that has the ID that matches
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));

            return result;
        }

        /// <summary>
        /// Get the list of items from the Database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            // Call to the ToListAsync method on Database with the Table called ItemModel
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}