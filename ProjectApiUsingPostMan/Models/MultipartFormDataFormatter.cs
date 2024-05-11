using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ProjectApiUsingPostMan.Models
{
    public class MultipartFormDataFormatter: MediaTypeFormatter
    {
        public MultipartFormDataFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(EmployeeRequest);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var multipartData = await content.ReadAsMultipartAsync();
            var empData = new EmployeeRequest();

            foreach (var contentPart in multipartData.Contents)
            {
                var fieldName = contentPart.Headers.ContentDisposition.Name.Trim('\"');

                if (fieldName == "Employee")
                {
                    var empContent = await contentPart.ReadAsStringAsync();
                    empData.Employee = JsonConvert.DeserializeObject<Employee>(empContent);
                }
                else if (fieldName == "ImageFile")
                {
                    empData.ImageFile = await contentPart.ReadAsByteArrayAsync();
                    empData.ImageFileName = contentPart.Headers.ContentDisposition.FileName;
                }
            }

            return empData;
        }
    }
}