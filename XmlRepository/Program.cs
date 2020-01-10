using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace XmlRepository
{
    class Program
    {
        static UnityContainer _container;

        static void Main(string[] args)
        {
            var settings = new Settings
            {
                DataProvider = DataProviderType.XmlFileStore,
                DataFileLocation = @"C:\projects\XmlRepository\"
            };

            Register(settings);

            var repository = _container.Resolve<IRepository<Employee>>();

            var employee = new Employee
            {
                FirstName = "David",
                LastName = "Lee"
            };
            repository.Insert(employee);

            employee.LastName = "Boo";
            repository.Update(employee);

            repository.Delete(employee);            
        }

        public static void Register(Settings settings)
        {
            _container = new UnityContainer();

            _container.RegisterInstance(settings);

            _container.RegisterType<XmlContext>();
            _container.RegisterType(typeof(IRepository<>), typeof(XmlRepository<>));
        }
    }
}
