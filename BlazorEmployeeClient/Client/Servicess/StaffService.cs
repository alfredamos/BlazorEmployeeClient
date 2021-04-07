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
    public class StaffService : IStaffService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/staffs";

        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Staff> AddEntity(Staff newEntity)
        {
            return await _httpClient.PostJsonAsync<Staff>(_baseUrl, newEntity);
        }

        public async Task DeleteEntity(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Staff[]>(_baseUrl);
        }

        public async Task<Staff> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Staff>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Staff>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Staff[]>($"{_baseUrl}/search/{searchKey}");
        }

        public async Task<IEnumerable<HeadCount>> Searcher(string searchKey)
        {
            return await _httpClient.GetJsonAsync<HeadCount[]>($"{_baseUrl}/department/{searchKey}");
        }

        public async Task<IEnumerable<HeadCount>> Searcher()
        {
            return await _httpClient.GetJsonAsync<HeadCount[]>($"{_baseUrl}/dello");
        }

        public async Task<Staff> UpdateEntity(Staff updatedEntity)
        {
            return await _httpClient.PutJsonAsync<Staff>($"{_baseUrl}/{updatedEntity.StaffID}", updatedEntity);
        }
    }
}
