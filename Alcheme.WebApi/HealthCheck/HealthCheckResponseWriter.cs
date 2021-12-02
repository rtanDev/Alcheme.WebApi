using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alcheme.WebApi.HealthCheck
{
    public class HealthCheckResponseWriter
    {
        [System.Obsolete]
        public static Task WriteResponse(HttpContext context, HealthReport healthresult)
        {
            if (healthresult == null)
                return context.Response.WriteAsync("{}");

            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyCompanyAttribute[] companyAttributes = (AssemblyCompanyAttribute[])assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            AssemblyProductAttribute[] productAttributes = (AssemblyProductAttribute[])assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            AssemblyInformationalVersionAttribute[] versionAttributes = (AssemblyInformationalVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true);
            AssemblyFileVersionAttribute[] fileVersionAttributes = (AssemblyFileVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);

            dynamic result = new ExpandoObject();
            result.Company = companyAttributes.First()?.Company;
            result.Product = productAttributes.First()?.Product;
            result.DotNetCoreVersion = System.Environment.Version.ToString();
            result.PackageVersion = versionAttributes?.First()?.InformationalVersion;
            result.AssemblyFileVersion = fileVersionAttributes?.First()?.Version;
            result.BuildDate = new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString();
            result.CurrentDate = System.DateTime.Now.ToString();
            result.TimeZone = System.TimeZone.CurrentTimeZone;
            result.Guid = System.Guid.NewGuid().ToString();

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("status", healthresult.Status.ToString());
                    writer.WriteStartObject("results");
                    foreach (var entry in healthresult.Entries)
                    {
                        writer.WriteStartObject(entry.Key);
                        writer.WriteString("status", entry.Value.Status.ToString());
                        writer.WriteString("description", entry.Value.Description);
                        writer.WriteStartObject("data");
                        foreach (var item in entry.Value.Data)
                        {
                            writer.WritePropertyName(item.Key);
                            JsonSerializer.Serialize(
                                writer, item.Value, item.Value?.GetType() ??
                                typeof(object));
                        }
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();

                    writer.WriteString("Company", result.Company);
                    writer.WriteString("Product", result.Product);
                    writer.WriteString("CoreVersion", result.DotNetCoreVersion);
                    writer.WriteString("PackageVersion", result.PackageVersion);
                    writer.WriteString("AssemblyFileVersion", result.AssemblyFileVersion);
                    writer.WriteString("BuildDate", result.BuildDate);
                    writer.WriteString("CurrentDate", result.CurrentDate);
                    writer.WritePropertyName("TimeZone");
                    JsonSerializer.Serialize(writer, result.TimeZone, result.TimeZone?.GetType() ?? typeof(System.TimeZone));
                    writer.WriteEndObject();
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                return context.Response.WriteAsync(json);
            }
        }
    }
}
