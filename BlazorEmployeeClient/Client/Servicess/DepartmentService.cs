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
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/departments";

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Department> AddEntity(Department newEntity)
        {
            return await _httpClient.PostJsonAsync<Department>(_baseUrl, newEntity);
        }

        public async Task DeleteEntity(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Department[]>(_baseUrl);
        }

        public async Task<Department> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Department>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Department>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Department[]>($"{_baseUrl}/search/{searchKey}");
        }

        public async Task<Department> UpdateEntity(Department updatedEntity)
        {
            return await _httpClient.PutJsonAsync<Department>($"{_baseUrl}/{updatedEntity.DepartmentID}", updatedEntity);
        }
    }
}
