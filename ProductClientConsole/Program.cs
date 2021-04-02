//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using System.Net.Http.Formatting.dll;
///using System.Threading.Tasks;

namespace ProductClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {



            Product product = new Product();
            GetAllProducts().Wait();
            Console.WriteLine("Enter the Id");
            int id = Convert.ToInt32(Console.ReadLine());
            GetProductById(id).Wait();
            Console.WriteLine("Enter the Product Id");
            product.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Product Name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter the QuntyInStock");
            product.QualityStock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Description");
            product.Description = Console.ReadLine();
            Console.WriteLine("Enter the Supplier");
            product.Supplier = Console.ReadLine();

            Insert(product).Wait();
            GetAllProducts().Wait();

            Put().Wait();
            GetAllProducts().Wait();

            Delete().Wait();
            GetAllProducts().Wait();

        }


        static async Task GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Productss");
                if (response.IsSuccessStatusCode)
                {

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    var productList = JsonConvert.DeserializeObject<List<Product>>(jsonString.Result);

                    foreach (var temp in productList)
                    {
                        Console.WriteLine("Id:{0}\tName:{1}", temp.Id, temp.Name);





                    }

                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine("Internal server Error");
                }

            }
        }

        static async Task GetProductById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    Console.WriteLine("Id:{0}\tName:{1}", product.Id, product.Name);
                    //  Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);

                }


            }
        }
        static async Task Insert(Product product)
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                client.BaseAddress = new Uri("https://localhost:44381/");


                HttpResponseMessage response = await client.PostAsJsonAsync("api/Products", product);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    Console.WriteLine(response.StatusCode);
                }
            }
        }
        static async Task Put()
        {

            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                client.BaseAddress = new Uri("https://localhost:44381/");

                //PUT Method  
                var department = new Product() { Id = 9, Name = "Updated Department" };
                int id = 1;
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Products/" + id, department);
                if (response.IsSuccessStatusCode)

                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
        }

        static async Task Delete()
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                client.BaseAddress = new Uri("https://localhost:44381/");


                int id = 1;
                HttpResponseMessage response = await client.DeleteAsync("api/Students/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                    Console.WriteLine(response.StatusCode);
            }
        }
    }
}