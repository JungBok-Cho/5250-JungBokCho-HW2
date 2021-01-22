using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Liar's Ring", Description="Increase speed but decrease wisdom", Value=9 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Angel's Sword", Description="Inflict more damage to evils", Value=5 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Clairvoyant Necklace", Description="Get a great vision like an eagle", Value=3 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Spear of Zeus", Description="Attack monsters with electric power", Value=7 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Shining Shield", Description="Can use for SOS", Value=1 }
            };
        }

        public async Task<bool> CreateAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}