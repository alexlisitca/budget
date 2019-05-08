using Budget.Domain.Interfaces;
using Budget.Infrastructure.Business;
using Budget.Infrastructure.Data;
using Budget.Infrastructure.Data.Implementation;
using Budget.Services.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace Budget.Web.Mvc.DependencyInjection
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterType<BudgetDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<ScoreRepository>().As<IScoreRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<RepositoryContainer>().As<IRepositoryContainer>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}