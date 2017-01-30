using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDoWebApp.Tests
{
    public class TestLoggerFactory : ILoggerFactory
    {
        void ILoggerFactory.AddProvider(ILoggerProvider provider)
        {

        }

        ILogger ILoggerFactory.CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
    public class ServiceProvider : IServiceProvider
    {
        object IServiceProvider.GetService(Type serviceType)
        {
            return this;
        }
    }

    public class TestApplicationBuilder : IApplicationBuilder
    {
        IServiceProvider IApplicationBuilder.ApplicationServices
        {
            get
            {
                return new ServiceProvider();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IDictionary<string, object> IApplicationBuilder.Properties
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IFeatureCollection IApplicationBuilder.ServerFeatures
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        RequestDelegate IApplicationBuilder.Build()
        {
            throw new NotImplementedException();
        }

        IApplicationBuilder IApplicationBuilder.New()
        {
            throw new NotImplementedException();
        }

        IApplicationBuilder IApplicationBuilder.Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            return this;
        }
    }
    public class TestHostingEnviroment : IHostingEnvironment
    {
        string IHostingEnvironment.ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IFileProvider IHostingEnvironment.ContentRootFileProvider
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IHostingEnvironment.ContentRootPath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IHostingEnvironment.EnvironmentName
        {
            get
            {
                return "UnitTest";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IFileProvider IHostingEnvironment.WebRootFileProvider
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IHostingEnvironment.WebRootPath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public class TestServiceCollection : IServiceCollection
    {
        ServiceDescriptor IList<ServiceDescriptor>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        int ICollection<ServiceDescriptor>.Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollection<ServiceDescriptor>.IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            List<ServiceDescriptor> mylist = new List<ServiceDescriptor>();
            mylist.Add(item);
        }

        void ICollection<ServiceDescriptor>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<ServiceDescriptor>.Contains(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        void ICollection<ServiceDescriptor>.CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<ServiceDescriptor> IEnumerable<ServiceDescriptor>.GetEnumerator()
        {

            List<ServiceDescriptor> mylist = new List<ServiceDescriptor>();
            return mylist.GetEnumerator();
            //throw new NotImplementedException();
        }


        int IList<ServiceDescriptor>.IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        void IList<ServiceDescriptor>.Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<ServiceDescriptor>.Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        void IList<ServiceDescriptor>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }

    public class TestConfigurationRoot : IConfigurationRoot
    {
        string IConfiguration.this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
        {
            throw new NotImplementedException();
        }

        IChangeToken IConfiguration.GetReloadToken()
        {
            throw new NotImplementedException();
        }

        IConfigurationSection IConfiguration.GetSection(string key)
        {
            return new TestConfigurationSection();
        }

        void IConfigurationRoot.Reload()
        {
            throw new NotImplementedException();
        }
    }

    public class TestConfigurationSection : IConfigurationSection
    {
        string IConfiguration.this[string key]
        {
            get
            {
                return "";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IConfigurationSection.Key
        {
            get
            {
                return "";
            }
        }

        string IConfigurationSection.Path
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IConfigurationSection.Value
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
        {
            throw new NotImplementedException();
        }

        IChangeToken IConfiguration.GetReloadToken()
        {
            throw new NotImplementedException();
        }

        IConfigurationSection IConfiguration.GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
