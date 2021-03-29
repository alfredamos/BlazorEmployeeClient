using BlazorEmployeeClient.Client.Contracts;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Servicess
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/addresses";

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Address> AddEntity(Address newEntity)
        {
            return await _httpClient.PostJsonAsync<Address>(_baseUrl, newEntity);
        }

        public async Task DeleteEntity(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Address[]>(_baseUrl);
        }

        public async Task<Address> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Address>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Address>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Address[]>($"{_baseUrl}/search/{searchKey}");
        }

        public async Task<Address> UpdateEntity(Address updatedEntity)
        {
            return await _httpClient.PutJsonAsync<Address>($"{_baseUrl}/{updatedEntity.AddressID}", updatedEntity);
        }
    }
}
