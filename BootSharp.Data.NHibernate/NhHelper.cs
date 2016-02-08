using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace BootSharp.Data.NHibernate
{
    /// <summary>
    /// Helper for managing all the session factory available in the application.
    /// </summary>
    public static class NhHelper
    {
        private static ConcurrentDictionary<Type, ISessionFactory> _factories;

        static NhHelper()
        {
            _factories = new ConcurrentDictionary<Type, ISessionFactory>();
        }

        public static ISessionFactory GetSessionFactory(NhDataContext context, IPersistenceConfigurer dbPersister, AutoPersistenceModel autoPersistanceModel = null)
        {
            var contextType = context.GetType();
            var contextAssembly = Assembly.GetAssembly(contextType);

            return _factories.GetOrAdd(contextType, CreateSessionFactory(contextAssembly, dbPersister, autoPersistanceModel));
        }
        public static ISessionFactory GetSessionFactory(NhDataContext context, FluentConfiguration factoryConfig, AutoPersistenceModel autoPersistanceModel = null)
        {
            var contextType = context.GetType();
            var contextAssembly = Assembly.GetAssembly(contextType);

            return _factories.GetOrAdd(contextType, CreateSessionFactory(contextAssembly, factoryConfig, autoPersistanceModel));
        }

        private static ISessionFactory CreateSessionFactory(Assembly contextAssembly, IPersistenceConfigurer dbPersister, AutoPersistenceModel autoPersistanceModel = null)
        {
            // Create config
            var factoryConfig = Fluently.Configure();
            factoryConfig.Database(dbPersister);
            return CreateSessionFactory(contextAssembly, factoryConfig, autoPersistanceModel);
        }
        private static ISessionFactory CreateSessionFactory(Assembly contextAssembly, FluentConfiguration factoryConfig, AutoPersistenceModel autoPersistanceModel = null)
        {
            // Create mapping config
            factoryConfig.Mappings(m =>
            {
                m.HbmMappings.AddFromAssembly(contextAssembly);

                m.FluentMappings.AddFromAssembly(contextAssembly);

                if (autoPersistanceModel != null)
                {
                    m.AutoMappings.Add(autoPersistanceModel);
                }
            });

            // Exemple of schemaExport and create
            // factoryConfig.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            // Create factory
            var sessionFactory = factoryConfig.BuildSessionFactory();
            return sessionFactory;
        }
    }
}
