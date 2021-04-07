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
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/employees";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Employee> AddEntity(Employee newEntity)
        {
            return await _httpClient.PostJsonAsync<Employee>(_baseUrl, newEntity);
        }

        public async Task DeleteEntity(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Employee[]>(_baseUrl);
        }

        public async Task<Employee> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Employee>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Employee>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Employee[]>($"{_baseUrl}/search/{searchKey}");
        }

        public async Task<IEnumerable<HeadCounter>> Searcher(Dept searchKey)
        {            
            return await _httpClient.GetJsonAsync<HeadCounter[]>($"{_baseUrl}/department/{searchKey}");
        }

        public async Task<IEnumerable<HeadCounter>> Searcher()
        {
            return await _httpClient.GetJsonAsync<HeadCounter[]>($"{_baseUrl}/dello");
        }

        public async Task<Employee> UpdateEntity(Employee updatedEntity)
        {
            return await _httpClient.PutJsonAsync<Employee>($"{_baseUrl}/{updatedEntity.EmployeeID}", updatedEntity);
        }
    }
}
